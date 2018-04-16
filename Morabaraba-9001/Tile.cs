using System;
using System.Collections.Generic;
namespace Morabaraba9001
{
    public class Tile : ITile
    {

        public ICow Cow { get; set; }
        public IEnumerable<(char, int)> PossibleMoves { get; private set; }
        public Tile(ICow cow, IEnumerable<(char, int)> possibleMoves)
        {
            Cow = cow;
            PossibleMoves = possibleMoves;
        }


    }
}
