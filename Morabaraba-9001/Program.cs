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
        void MoveCow(ICow cow, (char,int) fromPos, (char,int) toPos);
        void PlaceCow(ICow cow, (char,int) pos);
        void FlyCow(ICow cow, (char,int) fromPos, (char,int) toPos);
        void KillCow((char,int) pos);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> allTiles { get; }
        IEnumerable<IEnumerable<ITile>> allBoardMills { get; }
        //    Dictionary<(char, int), ICow> allCows { get; }
    }


    public enum Color { dark, light }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
        (char, int) GetMove(string what);
        int Unplayed { get; }
        PlayerState State { get; }
    }

    public class Player : IPlayer
    {
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            Unplayed = 12;
        }

        public string Name { get; }

        public Color Color { get; }

        public int Unplayed { get; private set; }

        public PlayerState State { get; }

        public (char, int) GetMove(string what)
        {
            throw new NotImplementedException();
        }
    }


















    public interface ICow
    {
        //Symbol symbol {get;}
        Color Color { get; }
        IPos Pos { get; }

    }
    public interface ITile
    {
        ICow Cow { get; set; }
        IEnumerable<(char,int)> PossibleMoves { get; }

    }
    public class Tile : ITile
    {

        private ICow cow;
        private IEnumerable<(char, int)> possibleMoves;
        public Tile(ICow cow, IEnumerable<(char, int)> possibleMoves)
        {
            this.cow = cow;
            this.possibleMoves = possibleMoves;
        }

        public ICow Cow { 
            get 
            {

                return cow;
            }

            set{
                cow = value;   
            }
        }



        public IEnumerable<(char, int)> PossibleMoves
        {
            get{
                return possibleMoves;
                }
        }

           
    }

    public interface IReferee
    {
        IPlayer Winner();
       // IPlayer noMove();
        void Play(IPlayer player);
        void StartGame();
        // IPlayer player_1 { get; set; }
        //IPlayer player_2 { get; set; }

        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get;}
       
    }

    public class Referee : IReferee
    {
        public IPlayer CurrentPlayer { get; private set; }
        public IPlayer EnemyPlayer { get; private set; }
        public IBoard GameBoard { get; }

        public Referee(IPlayer p1, IPlayer p2, IBoard board ){
            CurrentPlayer = p1;
            if (p2.Color == Color.dark)
            {
                CurrentPlayer = p2;
            }
            GameBoard = board;

        }
       
       
        private void Move(IPlayer player){
          //  if (player.State != PlayerState.Moving)
             //   throw new IncorrectStateException();
           (char, int) toPos, fromPos;
            bool valid = false;
            while (!valid)
            {
                fromPos = player.GetMove("Where do you want to move from?: ");
                if (GameBoard.a)

                toPos = player.GetMove("Where do you want to move to?: ");

            }
            
        }
        private void Place(IPlayer player){
         //   if (player.State != PlayerState.Placing)
           //     throw new IncorrectStateException();
            (char, int) toPos, fromPos;
            bool valid = false;
            while (!valid)
            {
                toPos = player.GetMove("Where do you want to place your cow?: ");

            }

        }
        private void Fly(IPlayer player)
        {
            //if (player.State != PlayerState.Flying)
              //  throw new IncorrectStateException();
            (char, int) toPos, fromPos;
            bool valid = false;
            while (!valid)
            {
                fromPos = player.GetMove("Where do you want to fly from?: ");

                toPos=player.GetMove("Where do you want to fly to?: ");

            }
            
        }
            
        public void Play(IPlayer player,PlayerState state)
        {

            switch(state){
                case PlayerState.Placing:
                    Place(player);
                    break;
                case PlayerState.Moving:
                    Move(player);
                    break;
                case PlayerState.Flying:
                    Fly(player);
                    break;
                   

            }
            (char,int) pos = player.GetMove();


        }

        private changePlayerTurn(){
            IPlayer temp_player = currentPlayer;
            currentPlayer = EnemyPlayer;
        }
        public void StartGame(){
            while(true){
               // printBoard()
                Play(currentPlayer, currentPlayer.State);
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

        Dictionary<(char, int), ITile> allTiles;
        IEnumerable<IEnumerable<ITile>> allBoardMills;
        public Board()
        {
            allTiles = new Dictionary<(char, int), ITile>();
            allBoardMills = new List<IEnumerable<ITile>>();
            ITile A1 = allTiles[('A', 1)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 2), ('D', 1) });
            ITile A4 = allTiles[('A', 4)] = new Tile(null, new List<(char, int)> { ('A', 1), ('A', 7), ('B', 4) });
            ITile A7 = allTiles[('A', 7)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 6), ('D', 7) });

            ITile B2 = allTiles[('B', 2)] = new Tile(null, new List<(char, int)> { ('A', 1), ('B', 4), ('C', 3), ('D', 2) });
            ITile B4 = allTiles[('B', 4)] = new Tile(null, new List<(char, int)> { ('A', 4), ('B', 2), ('B', 6), ('C', 4) });
            ITile B6 = allTiles[('B', 6)] = new Tile(null, new List<(char, int)> { ('A', 7), ('B', 4), ('D', 6), ('C', 5) });

            ITile C3 = allTiles[('C', 3)] = new Tile(null, new List<(char, int)> { ('B', 2), ('C', 4), ('D', 3) });
            ITile C4 = allTiles[('C', 4)] = new Tile(null, new List<(char, int)> { ('B', 4), ('C', 3), ('C', 5) });
            ITile C5 = allTiles[('C', 5)] = new Tile(null, new List<(char, int)> { ('B', 6), ('C', 4), ('D', 5) });

            ITile D1 = allTiles[('D', 1)] = new Tile(null, new List<(char, int)> { ('A', 1), ('D', 2), ('G', 1) });
            ITile D2 = allTiles[('D', 2)] = new Tile(null, new List<(char, int)> { ('B', 2), ('D', 1), ('D', 3), ('F', 2) });
            ITile D3 = allTiles[('D', 3)] = new Tile(null, new List<(char, int)> { ('C', 3), ('D', 2), ('E', 3) });

            ITile D5 = allTiles[('D', 5)] = new Tile(null, new List<(char, int)> { ('C', 5), ('D', 6), ('E', 5) });
            ITile D6 = allTiles[('D', 6)] = new Tile(null, new List<(char, int)> { ('B', 6), ('D', 5), ('D', 7), ('F', 6) });
            ITile D7 = allTiles[('D', 7)] = new Tile(null, new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });

            ITile E3 = allTiles[('E', 3)] = new Tile(null, new List<(char, int)> { ('A', 7), ('D', 6), ('G', 7) });
            ITile E4 = allTiles[('E', 4)] = new Tile(null, new List<(char, int)> { ('E', 3), ('F', 4), ('E', 5) });
            ITile E5 = allTiles[('E', 5)] = new Tile(null, new List<(char, int)> { ('D', 5), ('E', 4), ('F', 6) });

            ITile F2 = allTiles[('F', 2)] = new Tile(null, new List<(char, int)> { ('D', 2), ('E', 3), ('F', 4), ('G', 1) });
            ITile F4 = allTiles[('F', 4)] = new Tile(null, new List<(char, int)> { ('E', 4), ('F', 2), ('F', 6), ('G', 4) });
            ITile F6 = allTiles[('F', 6)] = new Tile(null, new List<(char, int)> { ('D', 6), ('E', 5), ('F', 4), ('G', 7) });

            ITile G1 = allTiles[('G', 1)] = new Tile(null, new List<(char, int)> { ('D', 1), ('F', 2), ('G', 4) });
            ITile G4 = allTiles[('G', 4)] = new Tile(null, new List<(char, int)> { ('F', 4), ('G', 1), ('G', 7) });
            ITile G7 = allTiles[('G', 7)] = new Tile(null, new List<(char, int)> { ('D', 7), ('F', 6), ('G', 4) });


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
            allBoardMills = new List<IEnumerable<ITile>>{
                AA17, BB26, CC35, DD13, DD57, EE35, FF26, GG17, AG11, BF22,
                CE33, AC44, EG44, CE55, BF66, AG77, AC13, CA57, GE13, EG57}; //list of all possible mills    


        }
        public ICow Occupant((char, int) pos)
        {
        return allTiles[pos].Cow;
               
        }


        public IEnumerable<ITile> Cows(Color c)
        {
            
            return allTiles.Values.Where(tile => tile.Cow.Color == c ).ToArray();
               

        }
           
        public IEnumerable<ITile> Mills(IPlayer player)
        {
            IEnumerable<ITile> retMills= new List<ITile>();
            foreach(IEnumerable<ITile> mill in allBoardMills){
                for (int i = 0; i < mill.Count();i++){
                    ITile currentTile = mill.ElementAt(i);
                    if(currentTile.Cow.Color == player.Color && !retMills.Contains(currentTile)){
                        retMills.Append(currentTile);
                    }
                }
            }
        return retMills;
                        
        }

        public void FlyCow(ICow cow, (char,int) fromPos, (char,int) toPos)
        {
            allTiles[fromPos].Cow = null;
            allTiles[toPos].Cow = cow;
        }

        public IEnumerable<(char,int)> PossibleMoves((char,int) pos)
        {
            return allTiles[pos].PossibleMoves; 
        }

        public void KillCow((char,int) pos)
        {
            allTiles[pos].Cow = null;
        }

         

        public void MoveCow(ICow cow, (char,int) fromPos, (char,int) toPos)
        {
            allTiles[fromPos].Cow = null;
            allTiles[toPos].Cow = cow;
        }

          

        public void PlaceCow(ICow cow, (char,int) pos)
        {
            allTiles[pos].Cow = cow;
        }
    }

    public class Cow : ICow
    {
        public Cow(Color c, IPos pos)
        {
            Color = c;
            Pos = pos;
        }

        public Color Color { get; set; }

        public IPos Pos { get; set; }
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
