using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{

        public class Program
        {
            static void Main(string[] args)
            {
            Board board = new Board();
            Console.WriteLine(board.isOccupied(('B', 2)));
                Console.WriteLine("Morabaraba!");
               // Board b = new Board();
                //Console.WriteLine(b.allTiles.Values.Where(tile => tile.Cow == null).Count());
                //Console.ReadKey();
            }

        }

    }
