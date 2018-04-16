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
            Assert.That(b.AllTiles.Values.Where(tile => tile.Cow != null).Count() == 0);
        }


        [Test]
        public void ThePlayerWithDarkCowsMovesFirst()
        {
            Player rick = new Player("theRick", Color.dark);
            Player peter = new Player("peter Pan", Color.light);
            Board b = new Board();
            Referee louise = new Referee(rick, peter, b);
            Referee martin = new Referee(peter, rick, b);
            Assert.That(louise.CurrentPlayer == rick);
            Assert.That(martin.CurrentPlayer == rick);
        }

        [Test]
        public void CowsCanOnlyBePlacesOnEmptyTiles()
        {

        }

        [Test]
        public void AMaximumOf12PlacementsPerPlayer()
        {

        }

        [Test]
        public void CowsCannotBeMovedDuringPlacement()
        {

        }
    }
}
