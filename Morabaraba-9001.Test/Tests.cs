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
        [Test]
        public void ABoardHas24EmptyTilesAtStart()
        {
            Board b = new Board();
            Assert.That(b.AllTiles.Values.Where(tile => tile.Cow == null).Count() == 24);
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
        
        // During moving 
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
    }
}
