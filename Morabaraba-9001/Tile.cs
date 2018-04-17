using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Tile : ITile
    {
        
        public IEnumerable<(char, int)> PossibleMoves { get; private set; }
        public Tile(IEnumerable<(char, int)> possibleMoves)
        {
            PossibleMoves = possibleMoves;
        }


    }
}
