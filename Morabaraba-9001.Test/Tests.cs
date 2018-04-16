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
        [Test]
        public void BoardIsEmptyAtStart()
        {
            Board b = new Board();
            Assert.That(b.AllTiles.Values.Where(tile => tile.Cow != null).Count() == 0);
        }

        [Test]
        public void ThePlayerWithDarkCowsMovesFirst()
        {
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
        public void CowsCanOnlyBePlacedOnEmptyTiles((char, int) pos, bool isOpenBoardSpace, MoveError expected)
        {
            IPlayer player = Substitute.For<IPlayer>();
            //Player player = new Player("Morty", Color.dark);
            IBoard board = Substitute.For<IBoard>();
            MoveError result = MoveError.InValid;
            if (isOpenBoardSpace == true)
            {
                board.AllTiles[pos].Returns((ITile)null);
            }
            else
            {
                board.AllTiles[pos].Returns(Arg.Any<ITile>());
            }
            result = board.PlaceCow(player, pos);
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
        public void ACowCanOnlyMoveToAnotherConnectedSpace(Color c, (char,int) pos)
        {
            ICow cow = new Cow(c, pos);
            IBoard b = Substitute.For<IBoard>();
            IPlayer p = new Player("player", c);

            (char,int)[] posMoves = b.AllTiles[pos].PossibleMoves.ToArray();
            foreach ((char,int) tile in posMoves){

            }
            b.Cows(c).Returns(new ITile[] {  });  

        }

        [Test]
        public void ACowCanOnlyMoveToAnEmptySpace()
        {

        }

        [Test]
        public void MovingDoesNotDecreaseOrIncreaseTheNumberOfTilesOnTheBoard()
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
