using System;
using System.Collections.Generic;
using System.Linq;
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
            CurrentPlayer = player_1;
            OtherPlayer = player_2;
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
            return Referee.EndGame(OtherPlayer);


        }

        public MoveError Fly((char, int) fromPos, (char, int) toPos)
        {
            MoveError error = Referee.Fly(CurrentPlayer, fromPos, toPos);
            if(error == MoveError.Valid){
                GameBoard.Move(CurrentPlayer, fromPos, toPos);
                CurrentPlayer.moveCow(fromPos, toPos);
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
            }

            return error;
           
        }

        public MoveError Place((char, int) pos){

            MoveError error = Referee.Place(CurrentPlayer,pos);
            if (error == MoveError.Valid)
            {
                GameBoard.Place(CurrentPlayer, pos);
                CurrentPlayer.placeCow(pos);
              

            }

            return error;
        }
        public void ChangePlayerTurn(){
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

        public MoveError GetMove()
        {
            (char, int) fromPos, toPos; 
            switch(CurrentPlayer.State){
              
                case PlayerState.Placing:
                    toPos = GetPlayerMove("Where do you want to place your cow?: ");
                    return Place(toPos);
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
        public MoveError KillCow((char,int) killPos){
            MoveError error = Referee.KillCow(OtherPlayer, killPos);
            if (error == MoveError.Valid)
            {
                OtherPlayer.killCow(killPos);
            }
            return error;
        }

        public MoveError getKillCowMove(){
            (char, int) killPos;
            killPos = GetPlayerMove("Which cow do you want to shoot");
            return KillCow(killPos);

        }
        public bool isMillFormed((char,int) pos){
            return Referee.MillFormed(CurrentPlayer, pos);
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
