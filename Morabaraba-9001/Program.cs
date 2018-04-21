using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{

        public class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Morabaraba!");
            List<(char, int)> posList = new List<(char, int)>();
            posList.Add(('A', 1));
            posList.Add(('A', 4));
            posList.Add(('A', 7));
            Player player = new Player("h", Color.dark, PlayerState.Flying, posList);
            Board board = new Board();
            //Console.WriteLine(board.MillFormed(player, ('A', 7)));
            foreach(List<ITile> mills in board.Mills(player)){
                foreach(ITile mill in mills){
                    Console.WriteLine(mill.Pos); 
                }
            }
           
               // Board b = new Board();
                //Console.WriteLine(b.allTiles.Values.Where(tile => tile.Cow == null).Count());
                //Console.ReadKey();
            }

        }

    }
