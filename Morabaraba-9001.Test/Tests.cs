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
        public void ABoardHas24EmptyTiles()
        {
            Board b = new Board();
            Assert.That(b.AllTiles.Values.Where(tile => tile.Cow != null).Count() == 0);
        }

        //merge testing is a go!

        [Test]
        public void ThePlayerWithDarkCowsMovesFirst()
        {

        }

        [Test]
        public void CowsCanOnlyBePlacesOnEmptySpaces()
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

        //public void mergeTest
        // Second test

    }
}
