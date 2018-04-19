using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Tile : ITile
    {
        
        public IEnumerable<(char, int)> PossibleMoves { get; private set; }
        public (char, int) Pos { get; }

        public Color color { get; set; }

        public Tile((char, int) pos,Color c, IEnumerable<(char, int)> possibleMoves)
        {
            Pos = pos;
            PossibleMoves = possibleMoves;
            color = c;

        }


    }
}
