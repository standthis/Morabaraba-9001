using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{

    public interface IPos
    {
        char getRow();
        int getCol();
    }
    public interface IBoard
    {
        ICow Occupant((char, int) pos);
        IEnumerable<ITile> Cows(Color c);
        IEnumerable<ITile> Mills(IPlayer player);
        void MoveCow(ICow cow, (char, int) fromPos, (char, int) toPos);
        void PlaceCow(ICow cow, (char, int) pos);
        void FlyCow(ICow cow, (char, int) fromPos, (char, int) toPos);
        void KillCow((char, int) pos);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> AllTiles { get; }
        IEnumerable<IEnumerable<ITile>> AllBoardMills { get; }
        //    Dictionary<(char, int), ICow> allCows { get; }
    }
    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
        (char, int) GetMove(string what);
        int CowsUnPlaced { get; }
        int CowsOnBoard { get; }
        PlayerState State { get; }
        bool DecrementCowsOnBoard();
        bool DecrementCowsPlaced();
        bool addCow();
    }
    public interface ICow
    {
        //Symbol symbol {get;}
        Color Color { get; }

    }
    public interface ITile
    {
        ICow Cow { get; set; }
        IEnumerable<(char, int)> PossibleMoves { get; }
    }
    public interface IReferee
    {
        IPlayer Winner();
        // IPlayer noMove();
        MoveError Play(IPlayer player, PlayerState state);
        void changePlayerTurn();
        void StartGame();
        // IPlayer player_1 { get; set; }
        //IPlayer player_2 { get; set; }

        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get; }


    }

    public enum Color { dark, light }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

    public enum MoveError { NoCow, InValid, Valid}

    public class Player : IPlayer
    {
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            CowsUnPlaced = 12;
            CowsOnBoard = 0;
            State = PlayerState.Placing;
        }

        public string Name { get; }

        public Color Color { get; }

        public int CowsUnPlaced { get; private set; }
        public int CowsOnBoard { get; private set; }

        public PlayerState State { get; private set; }

        public (char, int) GetMove(string what)
        {
            Console.WriteLine(what);
            Console.Write("Row: ");
            char rowInput = Console.ReadKey().KeyChar;
            bool is_char = Char.TryParse(rowInput + "", out rowInput);
            Console.WriteLine();
            Console.Write("Column: ");
            char getCol = Console.ReadKey().KeyChar;
            Console.WriteLine();
            int col;
            bool is_numeric = System.Int32.TryParse(getCol + "", out col);
            if (is_numeric && is_char)
            {
                return (Char.ToUpper(rowInput), col);

            }
            else
            {
                Console.WriteLine("Row requires a character and Col requires a number.Please enter valid input ");
                return GetMove(what);
            }

        
        }
        public bool addCow(){
            CowsOnBoard += 1;
            if(CowsOnBoard>12){ //can't have more than 12 cows
                return false;
            }
            return true;


        }
        public bool DecrementCowsOnBoard()
        {
            CowsOnBoard -= 1;
            if (State == PlayerState.Moving)
            {
                if (CowsOnBoard == 3)
                {
                    State = PlayerState.Flying;
                }

            }
            else if(State == PlayerState.Flying){

                if(CowsOnBoard <2){
                    
                    return false;
                }
            }
            return true;
        }
        public bool DecrementCowsPlaced(){
            CowsUnPlaced -= 1;

            if(CowsUnPlaced==0){
                State = PlayerState.Moving;
            }
            else if (CowsUnPlaced < 0)
            {
                return false;
            }
            return true;
        }

    }



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



        public class Referee : IReferee
        {
            public IPlayer CurrentPlayer { get; private set; }
            public IPlayer EnemyPlayer { get; private set; }
            public IBoard GameBoard { get; }


            public Referee(IPlayer p1, IPlayer p2, IBoard board)
            {
                CurrentPlayer = p1;
                if (p2.Color == Color.dark)
                {
                    CurrentPlayer = p2;
                }
                GameBoard = board;

            }


        private MoveError Move(IPlayer player)
            {
                //  if (player.State != PlayerState.Moving)
                //   throw new IncorrectStateException();
                (char, int) toPos, fromPos;


                fromPos = player.GetMove("Where do you want to move from?: ");
                if (GameBoard.AllTiles[fromPos] != null && GameBoard.AllTiles[fromPos].Cow.Color == player.Color)
                {

                    toPos = player.GetMove("Where do you want to move to?: ");
                    if (GameBoard.AllTiles[toPos].Cow == null && GameBoard.AllTiles[fromPos].PossibleMoves.Any(tile => tile.Equals(toPos)))
                    {
                        
                        GameBoard.MoveCow(new Cow(player.Color), fromPos, toPos);
                        return MoveError.Valid;
                    }
                    else
                    {

                        return MoveError.InValid;//can't move here
                    }

                }
                else
                {
                     return MoveError.NoCow;
                }

                

            }
        private MoveError Place(IPlayer player)
            {
                   (char, int) toPos;
                    toPos = player.GetMove("Where do you want to place your cow?: ");
                    if (GameBoard.AllTiles[toPos].Cow == null)
                    {
                        GameBoard.PlaceCow(new Cow(player.Color), toPos);
                return MoveError.Valid;
                    }
                else{
                return MoveError.InValid;
                }



            }
        private MoveError Fly(IPlayer player)
            {
                //if (player.State != PlayerState.Flying)
                //  throw new IncorrectStateException();
                (char, int) toPos, fromPos;
                fromPos = player.GetMove("Where do you want to fly from?: ");
                if (GameBoard.AllTiles[fromPos] != null && GameBoard.AllTiles[fromPos].Cow.Color == player.Color)
                {

                    toPos = player.GetMove("Where do you want to fly to?: ");
                    if (GameBoard.AllTiles[toPos].Cow == null)
                    {
                        GameBoard.FlyCow(new Cow(player.Color), fromPos, toPos);
                        return MoveError.Valid;
                    }
                    else
                    {
                        return MoveError.InValid;//cant move cow here
                    }

                }
                else
                {
                    return MoveError.NoCow;//have no cow here
                }

                }

            

        public MoveError Play(IPlayer player, PlayerState state)
            {

                switch (state)
                {
                    case PlayerState.Placing:
                        return Place(player);

                    case PlayerState.Moving:
                        return Move(player);
                    case PlayerState.Flying:
                        return Fly(player);

                    default:
                        throw new Exception("Invalid state");
                }


            }
             
          
            public void changePlayerTurn()
            {
                //swap who's turn it is
                IPlayer temp_player = CurrentPlayer;
                CurrentPlayer = EnemyPlayer;
                EnemyPlayer = temp_player;
            }
            public void StartGame()
            {
                while (true)
                {
                    // printBoard()
                    Play(CurrentPlayer, CurrentPlayer.State);
                    changePlayerTurn();
                }
            }
            public IPlayer Winner()
            {
                throw new NotImplementedException();
            }
        }

        public class IlligalMoveException : ApplicationException { }

        public class Board : IBoard
        {

            public Dictionary<(char, int), ITile> AllTiles { get; }
            public IEnumerable<IEnumerable<ITile>>    AllBoardMills { get; }
            public Board()
            {
                AllTiles = new Dictionary<(char, int), ITile>();
                AllBoardMills = new List<IEnumerable<ITile>>();
                ITile A1 = AllTiles[('A', 1)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 2), ('D', 1) });
                ITile A4 = AllTiles[('A', 4)] = new Tile(null, new List<(char, int)> { ('A', 1), ('A', 7), ('B', 4) });
                ITile A7 = AllTiles[('A', 7)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 6), ('D', 7) });

                ITile B2 = AllTiles[('B', 2)] = new Tile(null, new List<(char, int)> { ('A', 1), ('B', 4), ('C', 3), ('D', 2) });
                ITile B4 = AllTiles[('B', 4)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 2), ('B', 6), ('C', 4) });
                ITile B6 = AllTiles[('B', 6)] = new Tile(null, new List<(char, int)> { ('A', 7), ('B', 4), ('D', 6), ('C', 5) });

                ITile C3 = AllTiles[('C', 3)] = new Tile(null, new List<(char, int)> { ('B', 2), ('C', 4), ('D', 3) });
                ITile C4 = AllTiles[('C', 4)] = new Tile(null, new List<(char, int)> { ('B', 4), ('C', 3), ('C', 5) });
                ITile C5 = AllTiles[('C', 5)] = new Tile(null, new List<(char, int)> { ('B', 6), ('C', 4), ('D', 5) });

                ITile D1 = AllTiles[('D', 1)] = new Tile(null, new List<(char, int)> { ('A', 1), ('D', 2), ('G', 1) });
                ITile D2 = AllTiles[('D', 2)] = new Tile(null, new List<(char, int)> { ('B', 2), ('D', 1), ('D', 3), ('F', 2) });
                ITile D3 = AllTiles[('D', 3)] = new Tile(null, new List<(char, int)> { ('C', 3), ('D', 2), ('E', 3) });

                ITile D5 = AllTiles[('D', 5)] = new Tile(null, new List<(char, int)> { ('C', 5), ('D', 6), ('E', 5) });
                ITile D6 = AllTiles[('D', 6)] = new Tile(null, new List<(char, int)> { ('B', 6), ('D', 5), ('D', 7), ('F', 6) });
                ITile D7 = AllTiles[('D', 7)] = new Tile(null, new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });

                ITile E3 = AllTiles[('E', 3)] = new Tile(null, new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });
                ITile E4 = AllTiles[('E', 4)] = new Tile(null, new List<(char, int)> { ('E', 3), ('F', 4), ('E', 5) });
                ITile E5 = AllTiles[('E', 5)] = new Tile(null, new List<(char, int)> { ('D', 5), ('E', 4), ('F', 6) });

                ITile F2 = AllTiles[('F', 2)] = new Tile(null, new List<(char, int)> { ('D', 2), ('E', 3), ('F', 4), ('G', 1) });
                ITile F4 = AllTiles[('F', 4)] = new Tile(null, new List<(char, int)> { ('E', 4), ('F', 2), ('F', 6), ('G', 4) });
                ITile F6 = AllTiles[('F', 6)] = new Tile(null, new List<(char, int)> { ('D', 6), ('E', 5), ('F', 4), ('G', 7) });

                ITile G1 = AllTiles[('G', 1)] = new Tile(null, new List<(char, int)> { ('D', 1), ('F', 2), ('G', 4) });
                ITile G4 = AllTiles[('G', 4)] = new Tile(null, new List<(char, int)> { ('F', 4), ('G', 1), ('G', 7) });
                ITile G7 = AllTiles[('G', 7)] = new Tile(null, new List<(char, int)> { ('D', 7), ('F', 6), ('G', 4) });


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
            public ICow Occupant((char, int) pos)
            {
                return AllTiles[pos].Cow;

            }


            public IEnumerable<ITile> Cows(Color c)
            {

                return AllTiles.Values.Where(tile => tile.Cow.Color == c).ToArray();


            }

            public IEnumerable<ITile> Mills(IPlayer player)
            {
                IEnumerable<ITile> retMills = new List<ITile>();
                foreach (IEnumerable<ITile> mill in AllBoardMills)
                {
                    for (int i = 0; i < mill.Count(); i++)
                    {
                        ITile currentTile = mill.ElementAt(i);
                        if (currentTile.Cow.Color == player.Color && !retMills.Contains(currentTile))
                        {
                            retMills.Append(currentTile);
                        }
                    }
                }
                return retMills;

            }

            public void FlyCow(ICow cow, (char, int) fromPos, (char, int) toPos)
            {
                AllTiles[fromPos].Cow = null;
                AllTiles[toPos].Cow = cow;
            }

            public IEnumerable<(char, int)> PossibleMoves((char, int) pos)
            {
                return AllTiles[pos].PossibleMoves;
            }

            public void KillCow((char, int) pos)
            {
                AllTiles[pos].Cow = null;
            }



            public void MoveCow(ICow cow, (char, int) fromPos, (char, int) toPos)
            {
                AllTiles[fromPos].Cow = null;
                AllTiles[toPos].Cow = cow;
            }



            public void PlaceCow(ICow cow, (char, int) pos)
            {
                AllTiles[pos].Cow = cow;
            }
        }

        public class Cow : ICow
        {
            public Cow(Color c)
            {
                Color = c;
            }

            public Color Color { get; set; }


        }

        public class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Morabaraba!");
                //Board b = new Board();
                //Console.WriteLine(b.allTiles.Values.Where(tile => tile.Cow == null).Count());
                //Console.ReadKey();
            }

        }

    }



