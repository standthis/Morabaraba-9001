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
            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();
            IReferee referee = Substitute.For<IReferee>();
            IBoard board = new Board();
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
                board.isOccupied(pos).Returns(false);
                game.CurrentPlayer.State.ReturnsForAnyArgs(PlayerState.Placing);
                game.CurrentPlayer.UnplacedCows.ReturnsForAnyArgs(12);
                game.CurrentPlayer.Color.Returns(Color.dark);
                result = game.Place(pos);
                Assert.AreEqual(expected,result);
                board.Received().Place(game.CurrentPlayer, pos);
                game.CurrentPlayer.Received().placeCow(pos);
               // game.CurrentPlayer.Received().placeCow(pos);    
            }
            else
            {
                //if isOpenBoardSpace is false, create a player with a cow in the given position
                //simulaate cow being at position, pos
                board.isOccupied(pos).Returns(true);
             
                result = referee.Place(game.CurrentPlayer, pos);
                Assert.AreEqual(result, expected);
                board.DidNotReceive().Place(player_1, pos);
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

            //player_1.State.Returns(PlayerState.Placing);

            //mock a board where every position is always free
            board.isOccupied(Arg.Any<(char, int)>()).Returns(false); 
            game.CurrentPlayer.UnplacedCows.Returns(12);
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

            board.DidNotReceive().Move(game.CurrentPlayer, fromPos, toPos);
            game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);
        

        }




        [Test]

        [TestCaseSource(nameof(legalMoves))]
        public void ACowCanOnlyMoveToAnotherConnectedSpace((char, int) fromPos,List<(char,int)> possibleMoves) // not passing 
        {

            IBoard board = new Board(); //used allboardTiles
            IBoard mockBoard= Substitute.For<IBoard>();
            
            IPlayer player_1 =  Substitute.For<IPlayer>();
            IPlayer player_2 =  Substitute.For<IPlayer>();

            IReferee referee = new Referee(mockBoard);
            IGame game = new Game(mockBoard,referee,player_1,player_2);

            //mock a player that is moving and has 1 cow at fromPos
            game.CurrentPlayer.hasCowAtPos(fromPos).Returns(true);
            game.CurrentPlayer.State.Returns(PlayerState.Moving);
            game.CurrentPlayer.Color.Returns(Color.dark);

            mockBoard.AllTiles.Returns(board.AllTiles);
            //mock a board where only 1 tile is occupied
            mockBoard.isOccupied(fromPos).Returns(true);
                
            //try moving to possible places
            foreach ((char, int) toPos in possibleMoves)
            {
          
                MoveError error = game.Move(fromPos,toPos);
                Assert.That(error == MoveError.Valid);
                mockBoard.Received().Move(game.CurrentPlayer,fromPos,toPos);
                game.CurrentPlayer.Received().moveCow(fromPos,toPos);
                
                


            }
            mockBoard.ClearReceivedCalls();
            game.CurrentPlayer.ClearReceivedCalls();
            //try moving anywhere except from the possible places
            foreach ((char, int) toPos in board.AllTiles.Values.Select(t => t.Pos).Except(possibleMoves))
            {
                MoveError error = game.Move(fromPos, toPos);
                Assert.That(error == MoveError.InValid);
                mockBoard.DidNotReceive().Move(game.CurrentPlayer, fromPos, toPos);
                game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);
            
               

            }

        }

        [Test]
        [TestCaseSource(nameof(legalMoves))]
        public void ACowCanOnlyMoveToAnEmptySpace((char, int) fromPos,List<(char, int)> possibleMoves) // matt 
        {
            //what's the difference between this test and moving to connected spaces

            IBoard board = new Board(); //used allboardTiles
            IBoard mockBoard = Substitute.For<IBoard>();

            IPlayer player_1 = Substitute.For<IPlayer>();
            IPlayer player_2 = Substitute.For<IPlayer>();

            IReferee referee = new Referee(mockBoard);
            IGame game = new Game(mockBoard, referee, player_1, player_2);


            game.CurrentPlayer.hasCowAtPos(fromPos).Returns(true);
            game.CurrentPlayer.State.Returns(PlayerState.Moving);
            game.CurrentPlayer.Color.Returns(Color.dark);
            //mock a completly empty board except for the fromPos
           
            mockBoard.AllTiles.Returns(board.AllTiles);
            //mock a board where only 1 tile is occupied
            mockBoard.isOccupied(fromPos).Returns(true);

            //try moving to possible places when they are empty
            foreach ((char, int) toPos in possibleMoves)
            {
                MoveError error = game.Move(fromPos, toPos);
                Assert.That(error == MoveError.Valid);
                mockBoard.Received().Move(game.CurrentPlayer, fromPos, toPos);
                game.CurrentPlayer.Received().moveCow(fromPos, toPos);
            }
            //mock a completly full board
            mockBoard.isOccupied(Arg.Any<(char,int)>()).Returns(true);

            mockBoard.ClearReceivedCalls();
            game.CurrentPlayer.ClearReceivedCalls();

            //try moving to possible places when they are not empty

            foreach ((char, int) toPos in possibleMoves)
            {
                MoveError error = game.Move(fromPos, toPos);
                Assert.That(error == MoveError.InValid);
                mockBoard.DidNotReceive().Move(game.CurrentPlayer, fromPos, toPos);
                game.CurrentPlayer.DidNotReceive().moveCow(fromPos, toPos);

            }



        }




        [Test]
        [TestCaseSource(nameof(toAndFromPositions))]
        public void MovingDoesNotDecreaseOrIncreaseTheNumberOfCowsOnTheBoard((char, int) fromPos, (char, int) toPos) //Louise
        {



            List<(char, int)> posList = new List<(char, int)>();

            //ensure player has a minimum of 4 pieces so that they are not flying
            posList.Add(('A', 1));
            posList.Add(('A', 4));
            posList.Add(('B', 2));
            posList.Add(('B', 4));
            posList.Add(('B', 6));
          

         /*   posList=posList.Where(pos => !pos.Equals(toPos) && !pos.Equals(fromPos)).ToList();
            //ensure player has fromPos and not toPos
            posList.Add(fromPos);
            int numberOfPieces = posList.Count();
            Player player = new Player("test player 1 ", Color.dark, PlayerState.Moving, posList);
            IReferee mockReferee = Substitute.For<IReferee>();
            mockReferee.Move(player, fromPos, toPos).Returns(MoveError.Valid);
            Assert.AreEqual(player.numCowsOnBoard(), numberOfPieces);//check that we're starting with just one player on the board
            player.moveCow(fromPos, toPos, mockReferee, PlayerState.Moving);
            Assert.AreEqual(player.numCowsOnBoard(), numberOfPieces);*/
        }

        //TESTS FOR DURING FLYING
        [Test]
        public void CowsCanFlyAnywhereIfOnly3CowsRemainOnTheBoard()
        {
            //Player player = new Player("test")
            //player.moveCow()
        }

        //GENERAL TESTING
        [Test]
        public void AMillFormsWhen3CowsOfTheSameColorAreInARow()
        {
            IBoard b = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            p1.Color.Returns(Color.dark);
         
            //p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('A', 7)) });
            p1.hasCowAtPos(('A', 1)).Returns(true);
            p1.hasCowAtPos(('A', 4)).Returns(true);
            p1.hasCowAtPos(('A', 7)).Returns(true);
            Assert.That(b.MillFormed(p1, ('A', 4)) == true);
            //  A1, A4, A7
        }

        [Test]
        public void AMillIsNotFormedWhen3CowsInALineAreDifferentColors()
        {
            IBoard b = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();

            p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 7)) });
            p1.hasCowAtPos(('A', 1)).Returns(true);
            p1.hasCowAtPos(('A', 7)).Returns(true);

            p2.Cows.Returns(new List<ICow> { new Cow(Color.light, ('A', 4)) });
            p2.hasCowAtPos(('A', 4)).Returns(true);
            Assert.That(b.MillFormed(p1, ('A', 4)) == false);

        }

        [Test]
        public void AMillIsNotFormedWhenConnectedSpacesDoNotFormALine()
        {
            IBoard b = new Board();
            IPlayer p1 = Substitute.For<IPlayer>();
            IPlayer p2 = Substitute.For<IPlayer>();
            p1.Color.Returns(Color.dark);
            p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('D', 1)) });
            p1.hasCowAtPos(('A', 1)).Returns(true);
            p1.hasCowAtPos(('A', 4)).Returns(true);
            p1.hasCowAtPos(('D', 1)).Returns(true);
            Assert.That(b.MillFormed(p1, ('A', 4)) == false);
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

            MoveError result = player2.killCow(pos, mockReferee);//request for enemy player's cow to be killed at the position where player1's cow actually is
            Assert.That(result != MoveError.Valid);
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
            MoveError result = player.killCow(pos, mockReferee);//request for enemy player's cow to be killed at the position where no cow has been created for
            Assert.That(result != MoveError.Valid);

        }

        [Test]
        [TestCaseSource(nameof(allBoardPositions))]
        public void ShotCowsAreRemovedFromTheBoard((char, int) pos)
        {
            IReferee mockReferee = Substitute.For<IReferee>();
            List<(char, int)> posList = new List<(char, int)>();
            posList.Add(pos);
            Player player = new Player("test player_1", Color.dark, PlayerState.Placing, posList);////create an enemy player (the player being shot) with a cow at the given position
            mockReferee.KillCow(player, pos).Returns(MoveError.Valid);
            MoveError result = player.killCow(pos, mockReferee);//kill enemy player's cow at position given 
            Assert.That(result == MoveError.Valid);
           // Assert.That(!player.Cows.Any(cow => cow.Pos.Equals(pos))); //player should not cow
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
            p1.Color.Returns(Color.dark);
            p2.Color.Returns(Color.light);
            p1.State.Returns(PlayerState.Moving);
            p2.State.Returns(PlayerState.Moving);
            b.AllTiles.Returns(newDic);
            p1.Cows.Returns(posToCows(new List<(char,int)>(board.AllTiles.Keys).Except(new List<(char,int)> {pos} ).ToList(), Color.dark));
            p2.Cows.Returns(posToCows(new List<(char,int)> {pos}, Color.light));

            bool result =  myRef.PlayerCanMove(p2);
            p2.CanMove(myRef).Returns(result);
            IGame G = new Game(b, myRef, p1, p2);
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
            IReferee mockRef = Substitute.For<IReferee>();
            p1.State.Returns(PlayerState.Moving);
            p2.State.Returns(PlayerState.Flying);
            p1.Color.Returns(Color.dark);
            p2.Color.Returns(Color.light);
            p1.Cows.Returns(new List<ICow> { new Cow(Color.dark, ('A', 1)), new Cow(Color.dark, ('A', 4)), new Cow(Color.dark, ('D', 1)) });
            p2.Cows.Returns(new List<ICow> { new Cow(Color.light, ('G', 1)), new Cow(Color.light, ('D', 2))});
            IGame G = new Game(b, mockRef, p1, p2);
            G.OtherPlayer.numCowsOnBoard().Returns(2);
            Assert.AreEqual(G.EndGame(), GameEnd.KilledOff);
        }
    }
}
