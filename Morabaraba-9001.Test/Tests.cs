﻿using System;
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
            new object[] { ('A', 1), true },
            new object[] { ('A', 4), true },
            new object[] { ('A', 7), true }
        };
        [Test]
        [TestCaseSource(nameof(legalPlacementOfCowsOnOccupiedAndUnoccupiedTiles))]
        public void BoardIsEmptyAtStart((char, int) pos, bool expected)//Louise
        {
            IPlayer player = Substitute.For<IPlayer>();
            IBoard board = Substitute.For<IBoard>();
            Referee referee = new Referee(player, player, board);
            bool result = referee.emptyTile(pos);
            Assert.That(result = expected);
        }

        [Test]
        public void ThePlayerWithDarkCowsMovesFirst()
        {
            //IPlayer darkPlayer = Substitute.For<IPlayer>();
            //IPlayer lightPlayer = Substitute.For<IPlayer>();

            Player rick = new Player("theRick", Color.dark);
            Player peter = new Player("peter Pan", Color.light);
            IBoard b = Substitute.For<IBoard>();
            Referee louise = new Referee(rick, peter, b);
            Referee martin = new Referee(peter, rick, b);
            Assert.That(louise.CurrentPlayer.Color == Color.dark);
            Assert.That(martin.CurrentPlayer.Color == Color.dark);
        }

        static object[] legalPlacementOfCowsOnOccupiedAndUnoccupiedTiles =
        {
            new object[] { ('A', 1), true, MoveError.Valid }
        };
        [Test]
        [TestCaseSource(nameof(legalPlacementOfCowsOnOccupiedAndUnoccupiedTiles))]
        public void CowsCanOnlyBePlacedOnEmptyTiles((char, int) pos, bool isOpenBoardSpace, MoveError expected)//Louise
        {
            IPlayer player = Substitute.For<IPlayer>();
            IBoard board = new Board(); //Substitute.For<IBoard>();
            if (isOpenBoardSpace == true)
            {
                //Dictionary<(char, int), ITile> mocked = new Dictionary<(char, int), ITile>();
                //ITile tileMock = Substitute.For<ITile>();
                //tileMock.Cow.Returns((ICow)null);
                //mocked[pos] = tileMock;
                //board.AllTiles.Returns(mocked);
                //board.AllTiles[pos].Cow.Returns((ICow)null);
            }
            else
            {
               // board.AllTiles[pos].Cow.Returns(Arg.Any<ICow>());
            }
            //MoveError result = board.PlaceCow(player, pos);
            //Assert.That(result == expected);
        }

        [Test]
        public void AMaximumOf12PlacementsPerPlayer() 
        {

        }

        [Test]
        public void CowsCannotBeMovedDuringPlacement()
        {

        }
        
        //TESTS FOR DURING MOVING
        // Incomplete 
        [Test]
        public void ACowCanOnlyMoveToAnotherConnectedSpace(Color c, (char,int) pos) // matt 
        {
            ICow cow = new Cow(c);
            IBoard b = Substitute.For<IBoard>();
            IPlayer p = new Player("player", c);

            (char,int)[] posMoves = b.AllTiles[pos].PossibleMoves.ToArray();
            foreach ((char,int) tile in posMoves){

            }
//            b.Cows(c).Returns(new ITile[] {  });  

        }

        [Test]
        public void ACowCanOnlyMoveToAnEmptySpace() // matt 
        {

        }

        [Test]
        public void MovingDoesNotDecreaseOrIncreaseTheNumberOfTilesOnTheBoard() // matt 
        {

        }

        //TESTS FOR DURING FLYING
        [Test]
        public void CowsCanFlyAnywhereIfOnly3CowsRemainOnTheBoard()
        {

        }

        //GENERAL TESTING
        [Test]
        public void AMillFormsWhen3CowsOfTheSameColorAreInARow()
        {

        }

        [Test]
        public void AMillIsNotFormedWhen3CowsInALineAreDifferentColors()
        {

        }

        [Test]
        public void AMillIsNotFormedWhenConnectedSpacesDoNotFormALine()
        {

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
        public void APlayerCannotShootTheirOwnCows()
        {

        }

        [Test]
        public void APlayerCannotShootAnEmptySpace()
        {

        }

        [Test]
        public void ShotCowsAreRemovedFromTheBoard()
        {

        }

        [Test]
        public void AWinOccursIfAnOpponentCannotMove()
        {

        }

        [Test]
        public void AWinOccursIfAPlayerIsNotInPlacingAndHasLessThan3Cows()
        {

        }
    }
}
