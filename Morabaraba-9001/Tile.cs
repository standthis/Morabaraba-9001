using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Tile : ITile
    {
        
        public IEnumerable<(char, int)> PossibleMoves { get; private set; }
        public (char, int) Pos { get; }
        public bool Occupied { get; set;}
        public Tile((char, int) pos,bool occupied, IEnumerable<(char, int)> possibleMoves)
        {
            Pos = pos;
            PossibleMoves = possibleMoves;
            Occupied = occupied;
        }


    }
}
