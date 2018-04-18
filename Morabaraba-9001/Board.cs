using System;
using System.Collections.Generic;
using System.Linq;
namespace Morabaraba_9001
{
    public class Board : IBoard
    {

        public Dictionary<(char, int), ITile> AllTiles { get; }
        public IEnumerable<IEnumerable<ITile>> AllBoardMills { get; }

        public Board()
        {
            // All tiles on the board
            AllTiles = new Dictionary<(char, int), ITile>();
            AllBoardMills = new List<IEnumerable<ITile>>();

            ITile A1 = AllTiles[('A', 1)] = new Tile(('A', 1), new List<(char, int)> { ('A', 4), ('B', 2), ('D', 1) });
            ITile A4 = AllTiles[('A', 4)] = new Tile(('A', 4), new List<(char, int)> { ('A', 1), ('A', 7), ('B', 4) });
            ITile A7 = AllTiles[('A', 7)] = new Tile(('A', 7), new List<(char, int)> { ('A', 4), ('B', 6), ('D', 7) });

            ITile B2 = AllTiles[('B', 2)] = new Tile(('B', 2), new List<(char, int)> { ('A', 1), ('B', 4), ('C', 3), ('D', 2) });
            ITile B4 = AllTiles[('B', 4)] = new Tile(('B', 4), new List<(char, int)> { ('A', 4), ('B', 2), ('B', 6), ('C', 4) });
            ITile B6 = AllTiles[('B', 6)] = new Tile(('B', 6), new List<(char, int)> { ('A', 7), ('B', 4), ('D', 6), ('C', 5) });

            ITile C3 = AllTiles[('C', 3)] = new Tile(('C', 3), new List<(char, int)> { ('B', 2), ('C', 4), ('D', 3) });
            ITile C4 = AllTiles[('C', 4)] = new Tile(('C', 4), new List<(char, int)> { ('B', 4), ('C', 3), ('C', 5) });
            ITile C5 = AllTiles[('C', 5)] = new Tile(('C', 5), new List<(char, int)> { ('B', 6), ('C', 4), ('D', 5) });

            ITile D1 = AllTiles[('D', 1)] = new Tile(('D', 1), new List<(char, int)> { ('A', 1), ('D', 2), ('G', 1) });
            ITile D2 = AllTiles[('D', 2)] = new Tile(('D', 2), new List<(char, int)> { ('B', 2), ('D', 1), ('D', 3), ('F', 2) });
            ITile D3 = AllTiles[('D', 3)] = new Tile(('D', 3), new List<(char, int)> { ('C', 3), ('D', 2), ('E', 3) });

            ITile D5 = AllTiles[('D', 5)] = new Tile(('D', 5), new List<(char, int)> { ('C', 5), ('D', 6), ('E', 5) });
            ITile D6 = AllTiles[('D', 6)] = new Tile(('D', 6), new List<(char, int)> { ('B', 6), ('D', 5), ('D', 7), ('F', 6) });
            ITile D7 = AllTiles[('D', 7)] = new Tile(('D', 7), new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });

            ITile E3 = AllTiles[('E', 3)] = new Tile(('E', 3), new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });
            ITile E4 = AllTiles[('E', 4)] = new Tile(('E', 4), new List<(char, int)> { ('E', 3), ('F', 4), ('E', 5) });
            ITile E5 = AllTiles[('E', 5)] = new Tile(('E', 5), new List<(char, int)> { ('D', 5), ('E', 4), ('F', 6) });

            ITile F2 = AllTiles[('F', 2)] = new Tile(('F', 2), new List<(char, int)> { ('D', 2), ('E', 3), ('F', 4), ('G', 1) });
            ITile F4 = AllTiles[('F', 4)] = new Tile(('F', 4), new List<(char, int)> { ('E', 4), ('F', 2), ('F', 6), ('G', 4) });
            ITile F6 = AllTiles[('F', 6)] = new Tile(('F', 6), new List<(char, int)> { ('D', 6), ('E', 5), ('F', 4), ('G', 7) });

            ITile G1 = AllTiles[('G', 1)] = new Tile(('G', 1), new List<(char, int)> { ('D', 1), ('F', 2), ('G', 4) });
            ITile G4 = AllTiles[('G', 4)] = new Tile(('G', 4), new List<(char, int)> { ('F', 4), ('G', 1), ('G', 7) });
            ITile G7 = AllTiles[('G', 7)] = new Tile(('G', 7), new List<(char, int)> { ('D', 7), ('F', 6), ('G', 4) });

            //All mills
            IEnumerable<ITile> AA17 = new List<ITile> { A1, A4, A7 };                                                                     //all coordinate combinations that can form a mill (if all are occupied by the same player)
            IEnumerable<ITile> BB26 = new List<ITile> { B2, B4, B6 };
            IEnumerable<ITile> CC35 = new List<ITile> { C3, C4, C5 };
            IEnumerable<ITile> DD13 = new List<ITile> { D1, D2, D3 };
            IEnumerable<ITile> DD57 = new List<ITile> { D5, D6, D7 };
            IEnumerable<ITile> EE35 = new List<ITile> { E3, E4, E5 };
            IEnumerable<ITile> FF26 = new List<ITile> { F2, F4, F6 };
            IEnumerable<ITile> GG17 = new List<ITile> { G1, G4, G7 };

            IEnumerable<ITile> AG11 = new List<ITile> { A1, D1, G1 };
            IEnumerable<ITile> BF22 = new List<ITile> { B2, D2, F2 };
            IEnumerable<ITile> CE33 = new List<ITile> { C3, D3, E3 };
            IEnumerable<ITile> AC44 = new List<ITile> { A4, B4, C4 };
            IEnumerable<ITile> EG44 = new List<ITile> { E4, F4, G4 };
            IEnumerable<ITile> CE55 = new List<ITile> { C5, D5, E5 };
            IEnumerable<ITile> BF66 = new List<ITile> { B6, D6, F6 };
            IEnumerable<ITile> AG77 = new List<ITile> { A7, D7, G7 };

            IEnumerable<ITile> AC13 = new List<ITile> { A1, B2, C3 };
            IEnumerable<ITile> CA57 = new List<ITile> { C5, B6, A7 };
            IEnumerable<ITile> GE13 = new List<ITile> { G1, F2, E3 };
            IEnumerable<ITile> EG57 = new List<ITile> { E5, F6, G7 };

            AllBoardMills = new List<IEnumerable<ITile>>{
                AA17, BB26, CC35, DD13, DD57, EE35, FF26, GG17, AG11, BF22,
                CE33, AC44, EG44, CE55, BF66, AG77, AC13, CA57, GE13, EG57}; //list of all possible mills    
            
        }
        

        public IEnumerable<ITile> Mills(IPlayer player)
        {
            IEnumerable<ITile> retMills = new List<ITile>();
            foreach (IEnumerable<ITile> mill in AllBoardMills)
            {
                for (int i = 0; i < mill.Count(); i++)
                {
                    ITile currentTile = mill.ElementAt(i);
                    if (player.hasCowAtPos(currentTile.Pos) && !retMills.Contains(currentTile))
                    {
                        retMills.Append(currentTile);
                    }
                }
            }
            return retMills;
        }
        public bool MillFormed(IPlayer player,(char,int ) pos){
            IEnumerable<ITile> playersMills = Mills(player);
            foreach(Tile tile in playersMills){
                if (tile.Pos.Equals(pos))
                    return true;
            }
            return false;
        }
       
        public IEnumerable<(char, int)> PossibleMoves((char, int) pos)
        {
            return AllTiles[pos].PossibleMoves;
        }
        
    }
    
}
