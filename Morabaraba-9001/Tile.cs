using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Tile : ITile
    {
        
        public IEnumerable<(char, int)> PossibleMoves { get; private set; }
        public (char, int) Pos { get; }

        public Tile((char, int) pos, IEnumerable<(char, int)> possibleMoves)
        {
            Pos = pos;
            PossibleMoves = possibleMoves;
        }


    }
}
