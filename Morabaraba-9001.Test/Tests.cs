using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using NSubstitute;
using Morabaraba_9001;
/****
 * NOTE:
 * FOR SOME OF THE TEST THE GAME OBJECT WAS ASSERTED BECAUSE WE NEEDED TO CHECK IF THE MOCKED PLAYER CALLED
 * IT'S APPROPRIATE METHOD AFTER THE REFEREE HAD VALIDATED SOMETHING. HOWEVER INSTEAD OF STUBBING METHODS
 * FOR THE PLAYER(SAY PLAYER_1) THAT WAS PASSED THROUGH AS A PARAMETER TO THE GAME OBJECT'S CONSTRUCTOR.
 * WE STUBBED METHODS FOR GAME.CURRENTPLAYER WHICH WAS SET TO EQUAL PLAYER_1 IN THE CONSTRUCTOR. FOR SOME REASON
 * STUBBING PLAYER_1'S METHODS DIDN'T WORK BECAUSE GAME.CURRENTPLAYER WAS NOT RECOGNIZING THOSE STUBBED 
 * METHODS. SO WE HAD TO STUB GAME.CURRENTPLAYER'S METHODS WHICH MADE THE TESTS PASS. PLAYER_1 IS EQUAL TO 
 * GAME.CURRENTPLAYER SO STUBBING THE GAME.CURRENTPLAYER SHOULD BE EQUIVALENT TO STUBBING PLAYER_1. GAME.CURRENTPLAYER
 * REFERENCES PLAYER_1 SO ANY METHOD CALLED ON GAME.CURRENTPLAYER IS ALSO CALLED ON PLAYER_1. SO THIS IS WHY WE
 * DIDN'T SEE THIS TO BE TO BIG OF AN ISSUE. SAME CAN APPLY FOR PLAYER_2 AND GAME.OTHERPLAYER
 * 
 * 
 * 
 * 
 * 
 * */



namespace Morabaraba_9001.Test
{
    [TestFixture]
    public class Tests
    {

        //TESTS FOR DURING PLACEMENT


        static object[] allBoardPositions =
        {
            new object[] { ('A', 1) },
            new object[] { ('A', 4) },
            new object[] { ('A', 7) },
            new object[] { ('B', 2) },
            new object[] { ('B', 4) },
            new object[] { ('B', 6) },
            new object[] { ('C', 3) },
            new object[] { ('C', 4) },
            new object[] { ('C', 5) },
            new object[] { ('D', 1) },
            new object[] { ('D', 2) },
            new object[] { ('D', 3) },
            new object[] { ('D', 5) },
            new object[] { ('D', 6) },
            new object[] { ('D', 7) },
            new object[] { ('E', 3) },
            new object[] { ('E', 4) },
            new object[] { ('E', 5) },
            new object[] { ('F', 2) },
            new object[] { ('F', 4) },
            new object[] { ('F', 6) },
            new object[] { ('G', 1) },
            new object[] { ('G', 4) },
            new object[] { ('G', 7) }

        };
        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void BoardIsEmptyAtStart((char, int) pos)//Louise
        {

            IPlayer player_1 = new Player("Player 1", Color.dark);
            IPlayer player_2 = new Player("Player 2", Color.light);
            IBoard board = Substitute.For<IBoard>();
            IReferee referee = new Referee(board);
            IGame game = new Game(board, referee, player_1, player_2);

            bool result=game.IsTileOccupied(pos);
            Assert.That(result == false);
        }

        [Test]
        public void ThePlayerWithDarkCowsMovesFirst()
        {
            IPlayer darkPlayer = new Player("Player 1", Color.dark);
            IPlayer lightPlayer = new Player("Player 2", Color.light);
            IReferee referee = Substitute.For<IReferee>();
            IBoard board = Substitute.For<IBoard>();
            IGame game_1 = new Game(board, referee, darkPlayer, lightPlayer);
            IGame game_2 = new Game(board, referee, lightPlayer,darkPlayer);
            Assert.That(game_1.CurrentPlayer.Color == Color.dark);//check that for both games, the CurrentPlayer (starting player as no moves have been made) is the dark one
            Assert.That(game_2.CurrentPlayer.Color == Color.dark);
        }

        static object[] legalPlacementOfCowsOnOccupiedAndUnoccupiedTiles =
        {
            new object[] { ('A', 1), true, MoveError.Valid },
            new object[] { ('A', 4), true, MoveError.Valid },
            new object[] { ('A', 7), true, MoveError.Valid },

            new object[] { ('B', 2), true, MoveError.Valid },
            new object[] { ('B', 4), true, MoveError.Valid },
            new object[] { ('B', 6), true, MoveError.Valid },

            new object[] { ('C', 3), true, MoveError.Valid },
            new object[] { ('C', 4), true, MoveError.Valid },
            new object[] { ('C', 5), true, MoveError.Valid },

            new object[] { ('D', 1), true, MoveError.Valid },
            new object[] { ('D', 2), true, MoveError.Valid },
            new object[] { ('D', 3), true, MoveError.Valid },
            new object[] { ('D', 5), true, MoveError.Valid },
            new object[] { ('D', 6), true, MoveError.Valid },
            new object[] { ('D', 7), true, MoveError.Valid },

            new object[] { ('E', 3), true, MoveError.Valid },
            new object[] { ('E', 4), true, MoveError.Valid },
            new object[] { ('E', 5), true, MoveError.Valid },

            new object[] { ('F', 2), true, MoveError.Valid },
            new object[] { ('F', 4), true, MoveError.Valid },
            new object[] { ('F', 6), true, MoveError.Valid },

            new object[] { ('G', 1), true, MoveError.Valid },
            new object[] { ('G', 4), true, MoveError.Valid },
            new object[] { ('G', 7), true, MoveError.Valid },


            new object[] { ('A', 1), false, MoveError.InValid },
            new object[] { ('A', 4), false, MoveError.InValid },
            new object[] { ('A', 7), false, MoveError.InValid },

            new object[] { ('B', 2), false, MoveError.InValid },
            new object[] { ('B', 4), false, MoveError.InValid },
            new object[] { ('B', 6), false, MoveError.InValid },
            new object[] { ('A', 1), false, MoveError.InValid },
            new object[] { ('A', 4), false, MoveError.InValid },
            new object[] { ('A', 7), false, MoveError.InValid },

            new object[] { ('B', 2), false, MoveError.InValid },
            new object[] { ('B', 4), false, MoveError.InValid },
            new object[] { ('B', 6), false, MoveError.InValid },

            new object[] { ('C', 3), false, MoveError.InValid },
            new object[] { ('C', 4), false, MoveError.InValid },
            new object[] { ('C', 5), false, MoveError.InValid },

            new object[] { ('D', 1), false, MoveError.InValid },
            new object[] { ('D', 2), false, MoveError.InValid },
            new object[] { ('D', 3), false, MoveError.InValid },
            new object[] { ('D', 5), false, MoveError.InValid },
            new object[] { ('D', 6), false, MoveError.InValid },
            new object[] { ('D', 7), false, MoveError.InValid },

            new object[] { ('E', 3), false, MoveError.InValid },
            new object[] { ('E', 4), false, MoveError.InValid },
            new object[] { ('E', 5), false, MoveError.InValid },

            new object[] { ('F', 2), false, MoveError.InValid },
            new object[] { ('F', 4), false, MoveError.InValid },
            new object[] { ('F', 6), false, MoveError.InValid },

            new object[] { ('G', 1), false, MoveError.InValid },
            new object[] { ('G', 4), false, MoveError.InValid },
            new object[] { ('G', 7), false, MoveError.InValid },
            new object[] { ('C', 3), false, MoveError.InValid },
            new object[] { ('C', 4), false, MoveError.InValid },
            new object[] { ('C', 5), false, MoveError.InValid },

            new object[] { ('D', 1), false, MoveError.InValid },
            new object[] { ('D', 2), false, MoveError.InValid },
            new object[] { ('D', 3), false, MoveError.InValid },
            new object[] { ('D', 5), false, MoveError.InValid },
            new object[] { ('D', 6), false, MoveError.InValid },
            new object[] { ('D', 7), false, MoveError.InValid },

            new object[] { ('E', 3), false, MoveError.InValid },
            new object[] { ('E', 4), false, MoveError.InValid },
            new object[] { ('E', 5), false, MoveError.InValid },

            new object[] { ('F', 2), false, MoveError.InValid },
            new object[] { ('F', 4), false, MoveError.InValid },
            new object[] { ('F', 6), false, MoveError.InValid },

            new object[] { ('G', 1), false, MoveError.InValid },
            new object[] { ('G', 4), false, MoveError.InValid },
            new object[] { ('G', 7), false, MoveError.InValid },

          //  new object[] { ('H', 7), false, MoveError.InValid }


        };
        [Test]
        [TestCaseSource(nameof(legalPlacementOfCowsOnOccupiedAndUnoccupiedTiles))]
        public void CowsCanOnlyBePlacedOnEmptyTiles((char, int) pos, bool isOpenBoardSpace, MoveError expected)//Louise
        {
            IPlayer player_1 =Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IBoard board = new Board();

            IReferee referee = new Referee(board);

           
            IGame game = new Game(board, referee, player_1, player_2);

            MoveError result;
            if (isOpenBoardSpace)
            {
               
                game.CurrentPlayer.State.ReturnsForAnyArgs(PlayerState.Placing);
                game.CurrentPlayer.UnplacedCows.ReturnsForAnyArgs(12);
                game.CurrentPlayer.Color.Returns(Color.dark);
                result = game.Place(pos);
                Assert.AreEqual(expected,result);
              
                game.CurrentPlayer.Received().placeCow(pos);
            }
            else
            {
                //if isOpenBoardSpace is false, create a player with a cow in the given position
                //simulaate cow being at position, pos

                result = game.Place(pos);
                  
                Assert.AreEqual(result, expected);
              
                game.CurrentPlayer.DidNotReceive().placeCow(pos);

            }



        }

        [Test]

        public void AMaximumOf12PlacementsPerPlayer() // matt 
        {

            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IBoard board = new Board();

            IReferee referee = new Referee(board);

            IGame game = new Game(board, referee, player_1, player_2);

            game.CurrentPlayer.State.Returns(PlayerState.Placing);
            game.CurrentPlayer.UnplacedCows.Returns(12);

            //mock a board where every position is always free
            game.CurrentPlayer.hasCowAtPos(Arg.Any<(char, int)>()).Returns(false);
            game.OtherPlayer.hasCowAtPos(Arg.Any<(char, int)>()).Returns(false);

            //try placing a cow 12 times

            for (int i = 0; i < 12;i++){
              
                Assert.That(game.Place(('A', 1))== MoveError.Valid);
                game.CurrentPlayer.Received().placeCow(('A',1));
                game.CurrentPlayer.UnplacedCows.Returns(12 - (i+1)); //decrease the number of placed cows


            }
            // 13'th place should fail 
            Assert.That(game.Place(('A',1)) == MoveError.InValid);
           
        }
        static object[] legalMoves = {
            new object [] {('A', 1), new List<(char, int)> { ('A', 4), ('B', 2), ('D', 1) }},
            new object [] {('A', 4), new List<(char, int)> { ('A', 1), ('A', 7), ('B', 4) }},
            new object [] {('A', 7), new List<(char, int)> { ('A', 4), ('B', 6), ('D', 7) }},

            new object [] {('B', 2), new List<(char, int)> { ('A', 1), ('B', 4), ('C', 3), ('D', 2) }},
            new object [] {('B', 4), new List<(char, int)> { ('A', 4), ('B', 2), ('B', 6), ('C', 4) }},
            new object [] {('B', 6), new List<(char, int)> { ('A', 7), ('B', 4), ('D', 6), ('C', 5) }},

            new object [] {('C', 3), new List<(char, int)> { ('B', 2), ('C', 4), ('D', 3) }},
            new object [] {('C', 4), new List<(char, int)> { ('B', 4), ('C', 3), ('C', 5) }},
            new object [] {('C', 5), new List<(char, int)> { ('B', 6), ('C', 4), ('D', 5) }},

            new object [] {('D', 1), new List<(char, int)> { ('A', 1), ('D', 2), ('G', 1) }},
            new object [] {('D', 2), new List<(char, int)> { ('B', 2), ('D', 1), ('D', 3), ('F', 2) }},
            new object [] {('D', 3), new List<(char, int)> { ('C', 3), ('D', 2), ('E', 3) }},

            new object [] {('D', 5), new List<(char, int)> { ('C', 5), ('D', 6), ('E', 5) }},
            new object [] {('D', 6), new List<(char, int)> { ('B', 6), ('D', 5), ('D', 7), ('F', 6) }},
            new object [] {('D', 7), new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) }},

            new object [] {('E', 3), new List<(char, int)> { ('F', 2), ('D', 3), ('E', 4) }},
            new object [] {('E', 4), new List<(char, int)> { ('E', 3), ('F', 4), ('E', 5) }},
            new object [] {('E', 5), new List<(char, int)> { ('D', 5), ('E', 4), ('F', 6) }},

            new object [] {('F', 2), new List<(char, int)> { ('D', 2), ('E', 3), ('F', 4), ('G', 1) }},
            new object [] {('F', 4), new List<(char, int)> { ('E', 4), ('F', 2), ('F', 6), ('G', 4) }},
            new object [] {('F', 6), new List<(char, int)> { ('D', 6), ('E', 5), ('F', 4), ('G', 7) }},

            new object [] {('G', 1), new List<(char, int)> { ('D', 1), ('F', 2), ('G', 4) }},
            new object [] {('G', 4), new List<(char, int)> { ('F', 4), ('G', 1), ('G', 7) }},
            new object [] {('G', 7), new List<(char, int)> { ('D', 7), ('F', 6), ('G', 4) }},
        };


        static object[] toAndFromPositions =
      {
            new object[] { ('A', 1), ('A', 4) },
            new object[] { ('A', 1), ('B', 2) },
            new object[] { ('A', 1), ('D', 1) },
            new object[] { ('A', 4), ('A', 1) },
            new object[] { ('A', 4), ('A', 7) },
            new object[] { ('A', 4), ('B', 4) },
            new object[] { ('A', 7), ('A', 4) },
            new object[] { ('A', 7), ('B', 6) },
            new object[] { ('A', 7), ('D', 7) },
            new object[] { ('B', 2), ('A', 1) },
            new object[] { ('B', 2), ('B', 4) },
            new object[] { ('B', 2), ('C', 3) },
            new object[] { ('B', 2), ('D', 2) },
            new object[] { ('B', 4), ('A', 4) },
            new object[] { ('B', 4), ('B', 2) },
            new object[] { ('B', 4), ('B', 6) },
            new object[] { ('B', 4), ('C', 4) },
            new object[] { ('B', 6), ('A', 7) },
            new object[] { ('B', 6), ('B', 4) },
            new object[] { ('B', 6), ('D', 6) },
            new object[] { ('B', 6), ('C', 5) },
            new object[] { ('C', 3), ('B', 2) },
            new object[] { ('C', 3), ('C', 4) },
            new object[] { ('C', 3), ('D', 3) },
            new object[] { ('C', 4), ('B', 4) },
            new object[] { ('C', 4), ('C', 3) },
            new object[] { ('C', 4), ('C', 5) },
            new object[] { ('C', 5), ('B', 6) },
            new object[] { ('C', 5), ('C', 4) },
            new object[] { ('C', 5), ('D', 5) },
            new object[] { ('D', 1), ('A', 1) },
            new object[] { ('D', 1), ('D', 2) },
            new object[] { ('D', 1), ('G', 1) },
            new object[] { ('D', 2), ('B', 2) },
            new object[] { ('D', 2), ('D', 1) },
            new object[] { ('D', 2), ('D', 3) },
            new object[] { ('D', 2), ('F', 2) },
            new object[] { ('D', 3), ('C', 3) },
            new object[] { ('D', 3), ('D', 2) },
            new object[] { ('D', 3), ('E', 3) },
            new object[] { ('D', 5), ('C', 5) },
            new object[] { ('D', 5), ('D', 6) },
            new object[] { ('D', 5), ('E', 5) },
            new object[] { ('D', 6), ('B', 6) },
            new object[] { ('D', 6), ('D', 5) },
            new object[] { ('D', 6), ('D', 7) },
            new object[] { ('D', 6), ('F', 6) },
            new object[] { ('D', 7), ('A', 7) },
            new object[] { ('D', 7), ('D', 6) },
            new object[] { ('D', 7), ('G', 7) },
            new object[] { ('E', 3), ('F', 2) },
            new object[] { ('E', 3), ('D', 3) },
            new object[] { ('E', 3), ('E', 4) },
            new object[] { ('E', 4), ('E', 3) },
            new object[] { ('E', 4), ('F', 4) },
            new object[] { ('E', 4), ('E', 5) },
            new object[] { ('E', 5), ('D', 5) },
            new object[] { ('E', 5), ('E', 4) },
            new object[] { ('E', 5), ('F', 6) },
            new object[] { ('F', 2), ('D', 2) },
            new object[] { ('F', 2), ('E', 3) },
            new object[] { ('F', 2), ('F', 4) },
            new object[] { ('F', 2), ('G', 1) },
            new object[] { ('F', 4), ('E', 4) },
            new object[] { ('F', 4), ('F', 2) },
            new object[] { ('F', 4), ('F', 6) },
            new object[] { ('F', 4), ('G', 4) },
            new object[] { ('F', 6), ('D', 6) },
            new object[] { ('F', 6), ('E', 5) },
            new object[] { ('F', 6), ('F', 4) },
            new object[] { ('F', 6), ('G', 7) },
            new object[] { ('G', 1), ('D', 1) },
            new object[] { ('G', 1), ('F', 2) },
            new object[] { ('G', 1), ('G', 4) },
            new object[] { ('G', 4), ('F', 4) },
            new object[] { ('G', 4), ('G', 1) },
            new object[] { ('G', 4), ('G', 7) },
            new object[] { ('G', 7), ('D', 7) },
            new object[] { ('G', 7), ('F', 6) },
            new object[] { ('G', 7), ('G', 4) }
        };


        [Test]

        [TestCaseSource(nameof(toAndFromPositions))]
        public void CowsCannotBeMovedDuringPlacement((char,int)fromPos,(char,int)toPos) // matt 
        {
            IBoard board = Substitute.For<IBoard>();
            
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IReferee referee = new Referee(board);
           
            IGame game = new Game(board, referee, player_1, player_2); 
            game.CurrentPlayer.State.Returns(PlayerState.Placing);

            MoveError error= game.Move(fromPos, toPos);

            Assert.That(error == MoveError.InValid);
            game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);
        

        }




        [Test]

        [TestCaseSource(nameof(legalMoves))]
        public void ACowCanOnlyMoveToAnotherConnectedSpace((char, int) fromPos,List<(char,int)> possibleMoves) 
        {

            IBoard board = new Board(); //used allboardTiles
            IBoard mockBoard= Substitute.For<IBoard>();
            
            IPlayer player_1 =  Substitute.For<IPlayer>();
            IPlayer player_2 =  Substitute.For<IPlayer>();

            IReferee referee = new Referee(mockBoard);
            IGame game = new Game(mockBoard,referee,player_1,player_2);

            //mock a player that is moving and has 1 cow at fromPos
            game.CurrentPlayer.hasCowAtPos(fromPos).Returns(true);
            game.OtherPlayer.hasCowAtPos(fromPos).Returns(true);

            game.CurrentPlayer.State.Returns(PlayerState.Moving);
            game.CurrentPlayer.Color.Returns(Color.dark);

            mockBoard.AllTiles.Returns(board.AllTiles);
            //mock a board where only 1 tile is occupied
         
                
            //try moving to possible places
            foreach ((char, int) toPos in possibleMoves)
            {
          
                MoveError error = game.Move(fromPos,toPos);
                Assert.That(error == MoveError.Valid);
    
                game.CurrentPlayer.Received().moveCow(fromPos,toPos);
                
                


            }
            mockBoard.ClearReceivedCalls();
            game.CurrentPlayer.ClearReceivedCalls();
            //try moving anywhere except from the possible places
            foreach ((char, int) toPos in board.AllTiles.Values.Select(t => t.Pos).Except(possibleMoves))
            {
                MoveError error = game.Move(fromPos, toPos);
                Assert.That(error == MoveError.InValid);

                game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);

            }

        }

        [Test]
        [TestCaseSource(nameof(legalMoves))]
        public void ACowCanOnlyMoveToAnEmptySpace((char, int) fromPos,List<(char, int)> possibleMoves) // matt 
        {
            //what's the difference between this test and moving to connected spaces

            IBoard board = new Board(); //used allboardTiles
         

            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IReferee referee = new Referee(board);
            IGame game = new Game(board, referee, player_1, player_2);

            game.OtherPlayer.hasCowAtPos(fromPos).Returns(true);
            game.CurrentPlayer.hasCowAtPos(fromPos).Returns(true);


            game.CurrentPlayer.State.Returns(PlayerState.Moving);
            game.CurrentPlayer.Color.Returns(Color.dark);
            //mock a completly empty board except for the fromPos
           

          
           

            //try moving to possible places when they are empty
            foreach ((char, int) toPos in possibleMoves)
            {
                game.OtherPlayer.hasCowAtPos(toPos).Returns(false);
                game.CurrentPlayer.hasCowAtPos(toPos).Returns(false);
                MoveError error = game.Move(fromPos, toPos);
                Assert.AreEqual(MoveError.Valid,error);
              
                game.CurrentPlayer.Received().moveCow(fromPos, toPos);
            }
            //mock a completly full board
           
           
            game.CurrentPlayer.ClearReceivedCalls();

            //try moving to possible places when they are not empty

            foreach ((char, int) toPos in possibleMoves)
            {
                game.OtherPlayer.hasCowAtPos(toPos).Returns(true);
                game.CurrentPlayer.hasCowAtPos(toPos).Returns(true);
                MoveError error = game.Move(fromPos, toPos);
                Assert.That(error == MoveError.InValid);
               
                game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);

            }



        }

       


        [Test]
        [TestCaseSource(nameof(toAndFromPositions))]
        public void MovingDoesNotDecreaseOrIncreaseTheNumberOfCowsOnTheBoard((char, int) fromPos, (char, int) toPos) //Louise
        {



            IBoard mockBoard = Substitute.For<IBoard>();
            IReferee mockReferee = Substitute.For<IReferee>();
           
            mockReferee.Move(Arg.Any<IPlayer>(),Arg.Any<IPlayer>(), fromPos, toPos).Returns(MoveError.Valid);

            List<(char, int)> posList = new List<(char, int)>();

            //ensure player has a minimum of 4 pieces so that they are not flying
            posList.Add(('A', 1));
            posList.Add(('A', 4));
            posList.Add(('B', 2));
            posList.Add(('B', 4));
            posList.Add(('B', 6));

            //ensure player has fromPos and not toPos
            posList = posList.Where(pos => !pos.Equals(toPos) && !pos.Equals(fromPos)).ToList();
            posList.Add(fromPos);

            IPlayer player_1 = new Player("test player 1 ", Color.dark, PlayerState.Moving, posList);
            IPlayer mock_player = Substitute.For<IPlayer>();
          
       

          
            int numberOfPieces = posList.Count();
           
            Assert.AreEqual(player_1.numCowsOnBoard(), numberOfPieces);
            player_1.moveCow(fromPos, toPos);  //make move
            Assert.AreEqual(player_1.numCowsOnBoard(), numberOfPieces); //assert that num of cows are the same
            //check that move was actually successful
            Assert.That(!player_1.hasCowAtPos(fromPos)); 
            Assert.That(player_1.hasCowAtPos(toPos));
        }

        //TESTS FOR DURING FLYING
        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void CowsCanFlyAnywhereIfOnly3CowsRemainOnTheBoard((char, int) fromPos)
        {
            IBoard board = new Board(); //used allboardTiles
         

            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IReferee referee = new Referee(board);
            IGame game = new Game(board, referee, player_1, player_2);

            //make player have more than 3 cows

          

            List<(char, int)> fromPosList = new List<(char, int)>();
            fromPosList.Add(fromPos);

            //try move from fromPos to anywhere on the board except to fromPos ofcourse
            //try flying with 12 cows to 4 cows -- should fail (move to be invalid)
            game.CurrentPlayer.numCowsOnBoard().Returns(12);
            game.CurrentPlayer.State.Returns(PlayerState.Flying);
            game.CurrentPlayer.hasCowAtPos(fromPos).Returns(true);

            for (int i = 0; i < 12 - 3; i++) // try from 12 cows to 3 cows
            {

                foreach ((char, int) toPos in board.AllTiles.Values.Select(t => t.Pos).Except(fromPosList))
                {
                    MoveError error = game.Fly(fromPos, toPos);
                    Assert.That(error == MoveError.InValid);
                   
                    game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);

                }
                game.CurrentPlayer.numCowsOnBoard().Returns(12 - (i + 1));
            }


            //now try flying with 3 cows --should pass (move to be valid)

            foreach ((char, int) toPos in board.AllTiles.Values.Select(t => t.Pos).Except(fromPosList))
            {
                MoveError error = game.Fly(fromPos, toPos);
                Assert.That(error == MoveError.Valid);
               
                game.CurrentPlayer.Received().moveCow(fromPos, toPos);

            }

        }



        //GENERAL TESTING

        static object[] allPossibleMills = {

            new object []{('A', 1),('A', 4),('A', 7)}, 
            new object []{('B', 2),('B', 4),('B', 6)}, 
            new object []{('C', 3),('C', 4),('C', 5)}, 
            new object []{('D', 1),('D', 2),('D', 3)}, 
            new object []{('D', 5),('D', 6),('D', 7)}, 
            new object []{('E', 3),('E', 4),('E', 5)}, 
            new object []{('F', 2),('F', 4),('F', 6)}, 
            new object []{('G', 1),('G', 4),('G', 7)}, 
            new object []{('A', 1),('D', 1),('G', 1)}, 
            new object []{('B', 2),('D', 2),('F', 2)}, 
            new object []{('C', 3),('D', 3),('E', 3)}, 
            new object []{('A', 4),('B', 4),('C', 4)}, 
            new object []{('E', 4),('F', 4),('G', 4)}, 
            new object []{('C', 5),('D', 5),('E', 5)}, 
            new object []{('B', 6),('D', 6),('F', 6)}, 
            new object []{('A', 7),('D', 7),('G', 7)}, 
            new object []{('A', 1),('B', 2),('C', 3)}, 
            new object []{('C', 5),('B', 6),('A', 7)}, 
            new object []{('G', 1),('F', 2),('E', 3)}, 
            new object []{('E', 5),('F', 6),('G', 7)}

        };
        [Test]
        [TestCaseSource(nameof(allPossibleMills))]
        public void AMillFormsWhen3CowsOfTheSameColorAreInARow((char,int)pos_1,(char, int) pos_2,(char, int) pos_3)
        {
            IBoard board = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();

            Referee referee = new Referee(board);

         
           
            p1.hasCowAtPos(pos_1).Returns(true);
            p1.hasCowAtPos(pos_2).Returns(true);
            p1.hasCowAtPos(pos_3).Returns(true);

            //check if player 1 cows form a mill (they should)
            Assert.That(referee.MillFormed(p1,pos_1) == true);
            Assert.That(referee.MillFormed(p1,pos_2) == true);
            Assert.That(referee.MillFormed(p1,pos_3) == true);
            
         
           // Assert.That(b.MillFormed(p1, ('A', 4)) == true);
            //  A1, A4, A7
        }

        [Test]
        [TestCaseSource(nameof(allPossibleMills))]
        public void AMillIsNotFormedWhen3CowsInALineAreDifferentColors((char, int) pos_1, (char, int) pos_2, (char, int) pos_3)
        {
            IBoard board = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();

            Referee referee = new Referee(board);


            //p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('A', 7)) });
            p1.hasCowAtPos(pos_1).Returns(true);
            p1.hasCowAtPos(pos_2).Returns(true);
            p2.hasCowAtPos(pos_3).Returns(true);

            //check if player 1 cows form a mill (they shouldn't)
            Assert.That(referee.MillFormed(p1, pos_1) == false);
            Assert.That(referee.MillFormed(p1, pos_2) == false);
            //check if player 2 cows from a mill (they shouldn't)
            Assert.That(referee.MillFormed(p2, pos_3) == false);

        }

        [Test]
        [TestCaseSource(nameof(legalMoves))]
        public void AMillIsNotFormedWhenConnectedSpacesDoNotFormALine((char, int) fromPos,List<(char, int)> possibleMoves)
        {
            //A line is a max of 3 positions 
            IBoard board = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();

            Referee referee = new Referee(board);
            p1.hasCowAtPos(fromPos).Returns(true);
            //add 2 other pieces from its connected spaces
            (char, int) pos_2 = possibleMoves.ElementAt(0);
            (char, int) pos_3 = possibleMoves.ElementAt(1);

            //pos_2 and pos_3 are connected spaces to fromPos

            //MAKE SURE LINE IS NOT FORMED BY 3 SPACES BUT ENSURE THEY ARE CONNECTED
            //check if 3 poisitions form a line
            List<(char, int)> line = new List<(char, int)>() { fromPos, pos_2, pos_3 };
            bool lineFormed = false;
            //board.AllBoardMills holds all the possible lines
            //now check fromPos, pos_2,pos_3 form a line (if they are a purmatation of a line/mill)
            foreach(IEnumerable<ITile> mill in board.AllBoardMills){
                int count = 0;
                foreach(ITile tile in mill){
                    if (line.Any(pos => pos.Equals(tile.Pos)))
                    {
                        count++;
                    }
                }
                if(count==3){// is line is formed
                    lineFormed = true;
                    break;
                } 
            }
            if(lineFormed){
              
                pos_3 = possibleMoves.ElementAt(2); //change pos_3 so it is not a line but still a conncted_space to fromPos
            }
            //it would be impossible for fromPos,pos_2 and pos_3 to form a line now
            //because if pos_3 formed a line with fromPos and pos_2 originally.The new pos_3 cannot form a line because a line 
            // is unique with 2 connected spaces and if you change one of those spaces (pos_3) and keep the other 2
            // the same it cannot possibly be a line
           
            p1.hasCowAtPos(pos_2).Returns(true);
            p1.hasCowAtPos(pos_3).Returns(true);

            //check player 1 cows that are connected do not from a mill
            Assert.That(referee.MillFormed(p1, fromPos) == false);
            Assert.That(referee.MillFormed(p1, pos_2) == false);
         
            Assert.That(referee.MillFormed(p1, pos_3) == false);



        }

        [Test]
        [TestCaseSource(nameof(allPossibleMills))]
        public void ShootingOnlyOccursInTheTurnTheMillIsCreatedAndNotInTheNextTurnItStillExists((char, int) pos_1, (char, int) pos_2, (char, int) pos_3)
        {
            //shooting occurs when forming mills. So if no mill is formed
            //no shooting occurs
            IBoard board = new Board();
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IReferee referee = new Referee(board);

           
            List<(char, int)> mill = new List<(char, int)>() { pos_1, pos_2, pos_3 };
            List<(char, int)> restOfBoard = board.AllTiles.Keys.Select(x => x).Except(mill).ToList();

          
            //mock player with a mill already and then make then have an extra cow outside that mill
            player_1.hasCowAtPos(pos_1).Returns(true);
            player_1.hasCowAtPos(pos_2).Returns(true);
            player_1.hasCowAtPos(pos_3).Returns(true);

            //add cow not in mill
            (char, int) pos_4 = restOfBoard.ElementAt(0);
            player_1.hasCowAtPos(pos_4).Returns(false);

            mill.Add(pos_4);
            //and mock player with 4 cows 
            player_1.Cows.Returns(posToCows(mill, Color.dark));

            //check if mill is formed if player_1 has recently move to pos_4 and still has a mill with pos_1,pos_2,pos_3 (A mill should not be formed)
            Assert.AreEqual(false, referee.MillFormed(player_1,pos_4));

          

        }

        [Test]
        [TestCaseSource(nameof(allPossibleMills))]
        public void ACowInAMillCannotBeShotWhenNonMillCowsExist((char,int)pos_1,(char,int) pos_2,(char,int)pos_3)
        {
            IBoard board = new Board();
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IReferee referee = new Referee(board);
            //make target player have mill
            List<(char, int)> mill = new List<(char, int)>() { pos_1, pos_2, pos_3 };
            List<(char, int)> restOfBoard = board.AllTiles.Keys.Select(x => x).Except(mill).ToList();
            //mock player to have 3 cows all in mills and 1 non mill cow
            player_2.hasCowAtPos(pos_1).Returns(true);
            player_2.hasCowAtPos(pos_2).Returns(true);
            player_2.hasCowAtPos(pos_3).Returns(true);
            player_2.hasCowAtPos(restOfBoard.ElementAt(0)).Returns(true);
            
           
      
         
            //add a non-mill cow
         
            mill.Add(restOfBoard.ElementAt(0));

            player_2.Cows.Returns(posToCows(mill, Color.light));

            //try to kill cow in mills - should fail
            Assert.AreEqual(MoveError.InValid, referee.KillCow(player_2, pos_1));
            Assert.AreEqual(MoveError.InValid, referee.KillCow(player_2, pos_2));
            Assert.AreEqual(MoveError.InValid, referee.KillCow(player_2, pos_3));
       

        }

        [Test]
        [TestCaseSource(nameof(allPossibleMills))]
        public void ACowInAMillCanBeShotWhenOnlyCowsInMillsExist((char, int) pos_1, (char, int) pos_2, (char, int) pos_3)
        {
            IBoard board = new Board();
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IReferee referee = new Referee(board);
            //make target player have only cows in mill

            //mock player to have 3 cows all in mills
            player_2.hasCowAtPos(pos_1).Returns(true);
            player_2.hasCowAtPos(pos_2).Returns(true);
            player_2.hasCowAtPos(pos_3).Returns(true);

            List<(char, int)> mill = new List<(char, int)>() { pos_1, pos_2, pos_3 };
            player_2.Cows.Returns(posToCows(mill,Color.light));
            player_2.numCowsOnBoard().Returns(3);

        
            //try to kill cow in mills - should pass cause all cow are in mills
            Assert.AreEqual(MoveError.Valid, referee.KillCow(player_2, pos_1));
            Assert.AreEqual(MoveError.Valid, referee.KillCow(player_2, pos_2));
            Assert.AreEqual(MoveError.Valid, referee.KillCow(player_2, pos_3));
       

        }


        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void APlayerCannotShootTheirOwnCows((char, int) pos)
        {
            IBoard board = new Board();
            IReferee referee = new Referee(board);
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IGame game = new Game(board, referee, player_1, player_2);
            List<(char, int)> posList = new List<(char, int)>() { pos };
            //mock player to have 1 cow
            game.CurrentPlayer.hasCowAtPos(pos).Returns(true);
            game.CurrentPlayer.Cows.Returns(posToCows(posList, Color.dark));
            game.CurrentPlayer.numCowsOnBoard().Returns(1);

            game.OtherPlayer.hasCowAtPos(pos).Returns(false);
            game.OtherPlayer.Cows.Returns(new List<ICow>());
            game.OtherPlayer.numCowsOnBoard().Returns(0);

            //should not be a valid kill
            Assert.AreNotEqual(MoveError.Valid,game.KillCow(pos));
            //player should shot recieve a call to kill its cow
            game.CurrentPlayer.DidNotReceive().killCow(pos);


           

        }

        //is passing, but probably not for the right reasons. I'm sleepy. Think I need to somehow assert the space is empty that I'm trying to shoot.
        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void APlayerCannotShootAnEmptySpace((char, int) pos)
        {
            IBoard board = new Board();
            IReferee referee = new Referee(board);
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IGame game = new Game(board, referee, player_1, player_2);
            List<(char, int)> posList = new List<(char, int)>(){pos};
           

            List<(char, int)> restOfBoard = board.AllTiles.Keys.Select(x => x).Except(posList).ToList();

            //mock a completly empty board and try shoot cow at pos
            game.CurrentPlayer.hasCowAtPos(pos).Returns(false);
            game.CurrentPlayer.Cows.Returns(new List<ICow>());
            game.CurrentPlayer.numCowsOnBoard().Returns(0);

            game.OtherPlayer.hasCowAtPos(pos).Returns(false);
            game.OtherPlayer.Cows.Returns(new List<ICow>());
            game.OtherPlayer.numCowsOnBoard().Returns(0);

            //should not be a valid kill
            Assert.AreNotEqual(MoveError.Valid,game.KillCow(pos));
            //no players should recieve a call to shoot a cow
            game.CurrentPlayer.DidNotReceive().killCow(pos);
            game.OtherPlayer.DidNotReceive().killCow(pos);


        }

        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void ShotCowsAreRemovedFromTheBoard((char, int) pos)
        {

            IBoard board = new Board(); //used allboardTiles
          
            IPlayer player_1 = Substitute.For<IPlayer>();

            //make target player have 1 cow at shooting position
            IPlayer player_2 = new Player("testing player_2", Color.light, PlayerState.Placing, new List<(char, int)>() { pos });
           

            IReferee mockRef = Substitute.For<IReferee>();
            IGame game = new Game(board, mockRef, player_1, player_2);
            //have board occupied at position pos
            game.CurrentPlayer.hasCowAtPos(pos).Returns(false);
           

            mockRef.KillCow(Arg.Any<IPlayer>(), pos).Returns(MoveError.Valid);
          
            //board should tile
          //  Assert.AreEqual(true, game.OtherPlayer.hasCowAtPos(pos)); //
            Assert.AreEqual(true, game.IsTileOccupied(pos));


            //kill should be valid
            Assert.AreEqual(MoveError.Valid, game.KillCow(pos));

           
            //board should not have tile
         //   Assert.AreEqual(false, game.OtherPlayer.hasCowAtPos(pos)); //
            Assert.AreEqual(false, game.IsTileOccupied(pos));

         
          }

        
        public List<ICow> posToCows(List<(char, int)> possies, Color c){
            List<ICow> result = possies.Select(p => new Cow(c, p)).ToList<ICow>();
            return result; 
        }
        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void AWinOccursIfAnOpponentCannotMove((char, int) pos)
        {
            // think about references
            // Do we need to test every possible NoMove state?
            IBoard board = new Board();
            IBoard b = Substitute.For<IBoard>();
            IReferee myRef = new Referee(board);
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();
            p1.Color.Returns(Color.dark);
            p2.Color.Returns(Color.light);
            Dictionary<(char,int),ITile> newDic = board.AllTiles;
            foreach (var item in newDic){
                item.Value.color = Color.dark;
            }
            newDic[pos].color = Color.light;
           
            //mock players and board
           
            p1.Color.Returns(Color.dark);
            p2.Color.Returns(Color.light);
            p1.State.Returns(PlayerState.Moving);
            p2.State.Returns(PlayerState.Moving);
            b.AllTiles.Returns(newDic);
            p1.Cows.Returns(posToCows(new List<(char, int)>(board.AllTiles.Keys).Except(new List<(char, int)> { pos }).ToList(), Color.dark));
            foreach(ICow cow in p1.Cows){
                p1.hasCowAtPos(cow.Pos).Returns(true);
                p2.hasCowAtPos(cow.Pos).Returns(false);
            }
            p1.hasCowAtPos(pos).Returns(false);
            p2.hasCowAtPos(pos).Returns(true);
            p2.Cows.Returns(posToCows(new List<(char, int)> { pos }, Color.light));
            Assert.AreEqual(GameEnd.CantMove,myRef.EndGame(p2, p1));
            //p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('D', 1)) });
            //var list3 = list1.Except(list2).ToList();
        }
        [Test]
        public void AWinOccursIfAPlayerIsNotInPlacingAndHasLessThan3Cows()
        {
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();
            IBoard b = Substitute.For<IBoard>();
            IReferee referee = new Referee(b);
           
            //mock the players
            p1.State.Returns(PlayerState.Moving);
            p2.State.Returns(PlayerState.Flying);
            p1.Color.Returns(Color.dark);
            p2.Color.Returns(Color.light);

            p2.numCowsOnBoard().Returns(2);
            Assert.AreEqual(GameEnd.KilledOff,referee.EndGame(p2, p1));

            //if the player is placing and only has 2 cows on the board game shouldn't end
            p2.State.Returns(PlayerState.Placing);
            Assert.AreEqual(GameEnd.NoEnd,referee.EndGame(p2, p1));
        

        }
    }
}
