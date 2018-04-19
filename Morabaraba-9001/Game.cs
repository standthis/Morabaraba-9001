using System;
namespace Morabaraba_9001
{
    public class Game :IGame
    {
        public Game(IBoard board, IReferee referee, IPlayer player_1, IPlayer player_2) 
        {
            GameBoard = board;
            Referee = referee;
            Player_1 = player_1;
            Player_2 = player_2;
            CurrentPlayer = Player_1;
            OtherPlayer = Player_2;
          /*  if(Player_1.Color==Player_2.Color){ //if wrong information for starting a game is wrong 
                Player_1 = new Player(Player_1.Name,Color.dark);
                Player_2 = new Player(Player_2.Name,Color.light);
            }*/
            if(Player_2.Color==Color.dark){
                CurrentPlayer = Player_2;
                OtherPlayer = Player_1;
            }
        }

        public IBoard GameBoard { get; private set; }

        public IReferee Referee { get; private set; }

        public IPlayer Player_1 {get; private set; }

        public IPlayer Player_2 { get; private set; }

        public IPlayer CurrentPlayer { get; private set; }
        public IPlayer OtherPlayer { get; private set; }

        public GameEnd EndGame()
        {
            if (OtherPlayer.State == PlayerState.Moving)
            {
                if (!OtherPlayer.CanMove(Referee))
                    return GameEnd.CantMove;

                if (OtherPlayer.numCowsOnBoard() == 2)
                    return GameEnd.KilledOff;
            }
            return GameEnd.NoEnd;
        }

        public MoveError Fly((char, int) fromPos, (char, int) toPos)
        {
            MoveError error = Referee.Fly(CurrentPlayer, fromPos, toPos);
            if(error == MoveError.Valid){
                GameBoard.Move(CurrentPlayer, fromPos, toPos);
                CurrentPlayer.moveCow(fromPos, toPos);
                changePlayerTurn();
            }

            return error;


        }

        public bool IsTileOccupied((char, int) pos)
        {
            return GameBoard.isOccupied(pos);
        }

        public MoveError Move((char, int) fromPos, (char, int) toPos)
        {
            MoveError error = Referee.Move(CurrentPlayer,fromPos,toPos);
            if (error == MoveError.Valid)
            {
                GameBoard.Move(CurrentPlayer, fromPos, toPos);
                CurrentPlayer.moveCow(fromPos, toPos);
                changePlayerTurn();
            }

            return error;
           
        }

        public MoveError Place((char, int) pos){

            MoveError error = Referee.Place(CurrentPlayer,pos);
            if (error == MoveError.Valid)
            {
                GameBoard.Place(CurrentPlayer, pos);
                CurrentPlayer.placeCow(pos);
                changePlayerTurn();

            }

            return error;
        }
        private void changePlayerTurn(){
            IPlayer tmpPlayer = CurrentPlayer;
            CurrentPlayer = OtherPlayer;
            OtherPlayer = tmpPlayer;
        }
        private (char, int) GetPlayerMove(string what)
        {
            Console.WriteLine(CurrentPlayer.Name + "'s turn!");
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
                return GetPlayerMove(what);
            }
        }

        public MoveError GetMove((char, int) pos)
        {
            (char, int) fromPos, toPos; 
            switch(CurrentPlayer.State){
              
                case PlayerState.Placing:
                    toPos = GetPlayerMove("Where do you want to place your cow?: ");
                    return Place(pos);
                case PlayerState.Moving:
                    fromPos = GetPlayerMove("Where do you want to move from?: ");
                    toPos = GetPlayerMove("Where do you want to move to?: ");
                    return Move(fromPos, toPos);

                case PlayerState.Flying:
                    fromPos = GetPlayerMove("Where do you want to fly from?: ");
                    toPos = GetPlayerMove("Where do you want to fly to?: ");
                    return Fly(fromPos, toPos);
                default:
                    return MoveError.InValid;

            }
               
               
           
        }

        public void StartGame()
        {
            throw new NotImplementedException();
        }

        public IPlayer Winner()
        {
            throw new NotImplementedException();
        }
    }
}
