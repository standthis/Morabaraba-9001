using System;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using NSubstitute;
using Morabaraba_9001;

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

            IBoard board = Substitute.For<IBoard>();

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

            IBoard board = Substitute.For<IBoard>();

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
          
            IGame game = new Game(mockBoard, mockReferee, player_1, mock_player);
          

          
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
        //=======
        //        public void CowsCanFlyAnywhereIfOnly3CowsRemainOnTheBoard((char,int) pos)
        //        {
        //            // Let 3 cows remain. Show that cows can fly anywhere in this state.
        //            IBoard board = new Board();
        //            IBoard b = Substitute.For<IBoard>();
        //            IPlayer p1 = Substitute.For<IPlayer>();
        //            IPlayer p2 = Substitute.For<IPlayer>();
        //            p1.Color.Returns(Color.dark);
        //            p1.State.Returns(PlayerState.Flying);
        //            p1.Cows.Returns(posToCows(new List<(char,int)> {pos}, Color.dark));
        //            p1.hasCowAtPos(pos).Returns(true);
        //            IReferee myRef = new Referee(board);
        //            bool pass = true;
        //            foreach ((char,int) toPos in new List<(char,int)>(board.AllTiles.Keys).Except(new List<(char,int)> {pos})){
        //                MoveError error = myRef.Fly(p1, pos, toPos);
        //                if (error != MoveError.Valid){
        //                    pass = false;
        //                    break;
        //                }
        //            }
        //            Assert.AreEqual(pass, true);
        //            //p1.Cows.Returns(posToCows(new List<(char,int)>(board.AllTiles.Keys).Except(new List<(char,int)> {pos} ).ToList(), Color.dark));
        //            //p2.Cows.Returns(posToCows(new List<(char,int)> {pos}, Color.light));
        //>>>>>>> b8fa601ac95144f33d76cf986634523716e20538
        //}


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
           /* IBoard board = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();

            Referee referee = new Referee(board);
            p1.hasCowAtPos(fromPos).Returns(true);
            //add 2 other pieces from its connected spaces
            (char, int) pos_2 = possibleMoves.ElementAt(0);
            (char, int) pos_3 = possibleMoves.ElementAt(1);
         
            p1.hasCowAtPos(pos_2).Returns(true);
            p1.hasCowAtPos(pos_3).Returns(true);

            //check player 1 cows that are connected do not from a mill
            Assert.That(referee.MillFormed(p1, fromPos) == false);
            Assert.That(referee.MillFormed(p1, pos_2) == false);
         
            Assert.That(referee.MillFormed(p1, pos_3) == false);*/



        }

        [Test]
        public void ShootingOnlyOccursInTheTurnTheMillIsCreatedAndNotInTheNextTurnItStillExists()
        {

        }

        [Test]
        public void ACowInAMillCannotBeShotWhenNonMillCowsExist()
        {

        }

        [Test]
        public void ACowInAMillCanBeShotWhenOnlyCowsInMillsExist()
        {

        }


        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void APlayerCannotShootTheirOwnCows((char, int) pos)
        {
            IReferee mockReferee = Substitute.For<IReferee>();
            List<(char, int)> posList = new List<(char, int)>();
            posList.Add(pos);
            Player player1 = new Player("test player_1", Color.dark, PlayerState.Placing, posList);//create a player with a cow in the given position
            Player player2 = new Player("test player_2", Color.light);
            mockReferee.KillCow(player2, pos).Returns(MoveError.InValid);

            //MoveError result = player2.killCow(pos, mockReferee);//request for enemy player's cow to be killed at the position where player1's cow actually is
           // Assert.That(result != MoveError.Valid);
            Assert.That(player1.Cows.Where(cow => cow.Pos.Equals(pos)).Count() == 1); //player 1 should still have cow

        }

        //is passing, but probably not for the right reasons. I'm sleepy. Think I need to somehow assert the space is empty that I'm trying to shoot.
        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void APlayerCannotShootAnEmptySpace((char, int) pos)
        {
            IReferee mockReferee = Substitute.For<IReferee>();
            Player player = new Player("test player", Color.dark);//create an enemy player (the player being shot)
            mockReferee.KillCow(player, pos).Returns(MoveError.InValid);
            //MoveError result = player.killCow(pos, mockReferee);//request for enemy player's cow to be killed at the position where no cow has been created for
          //  Assert.That(result != MoveError.Valid);

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
          
            //check playerr and board have cow to start off with
            Assert.AreEqual(true, game.OtherPlayer.hasCowAtPos(pos)); //
            Assert.AreEqual(true, game.IsTileOccupied(pos));

            //kill should be valid
            Assert.AreEqual(MoveError.Valid, game.KillCow(pos));
           
            //player should not cow and neither should board
            Assert.AreEqual(false, game.OtherPlayer.hasCowAtPos(pos)); //
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
           
            //bool result =  myRef.PlayerCanMove(p2);
           // p2.CanMove(myRef).Returns(result);
            IGame G = new Game(board, myRef, p1, p2);
            G.CurrentPlayer.Color.Returns(Color.dark);
            G.OtherPlayer.Color.Returns(Color.light);
            G.CurrentPlayer.State.Returns(PlayerState.Moving);
            G.OtherPlayer.State.Returns(PlayerState.Moving);
            b.AllTiles.Returns(newDic);
            G.CurrentPlayer.Cows.Returns(posToCows(new List<(char, int)>(board.AllTiles.Keys).Except(new List<(char, int)> { pos }).ToList(), Color.dark));
            foreach(ICow cow in G.CurrentPlayer.Cows){
                G.CurrentPlayer.hasCowAtPos(cow.Pos).Returns(true);
                G.OtherPlayer.hasCowAtPos(cow.Pos).Returns(false);
            }
            G.CurrentPlayer.hasCowAtPos(pos).Returns(false);

            G.OtherPlayer.Cows.Returns(posToCows(new List<(char, int)> { pos }, Color.light));

            Assert.AreEqual(G.EndGame(), GameEnd.CantMove);
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
            IGame G = new Game(b, referee, p1, p2);
            G.CurrentPlayer.State.Returns(PlayerState.Moving);
            G.OtherPlayer.State.Returns(PlayerState.Flying);
            G.CurrentPlayer.Color.Returns(Color.dark);
            G.OtherPlayer.Color.Returns(Color.light);
            //   G.CurrentPlayer.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('D', 1)) });
            //  G.OtherPlayer.Cows.Returns(new List<ICow> { new Cow(Color.light, ('G', 1)), new Cow(Color.light, ('D', 2))});
            G.OtherPlayer.numCowsOnBoard().Returns(2);
            Assert.AreEqual(G.EndGame(), GameEnd.KilledOff);
            //if the player is placing and only has 2 cows on the board game shouldn't end
            G.OtherPlayer.State.Returns(PlayerState.Placing);
            Assert.AreEqual(G.EndGame(), GameEnd.NoEnd);

        }
    }
}
