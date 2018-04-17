using System;
using System.Collections.Generic;
using System.Linq;
namespace Morabaraba_9001
{
    public class Player : IPlayer
    {
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            State = PlayerState.Placing;
            Cows = new List<ICow>();
            for (int i = 0; i < 12; i++)
            {
                Cows.Add(new Cow(Color));
            }
        }

        public Player(PlayerState state, Cow cow)//for testing purposes, instantiate a player with a given cow in it's cow list
        {
            State = state;
            Cows = new List<ICow>();
            Cows.Add(cow);
            for (int i = 1; i < 12; i++)
            {
                Cows.Add(new Cow(Color));
            }
        }


        public List<ICow> Cows { get; private set; }
        public string Name { get; }

        public Color Color { get; }

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

        public MoveError placeCow((char, int) toPos, IReferee referee)
        {
            MoveError error = referee.Place(this, toPos);
            if (error != MoveError.Valid)
            {
                return error;
            }
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].status == cowStatus.Unplaced)
                {
                    Cows[i].pos = toPos;
                    Cows[i].status = cowStatus.Placed;
                    break;
                }
            }
            return MoveError.Valid;
        }

        public MoveError moveCow((char, int) fromPos, (char, int) toPos, IReferee referee)
        {

            if (State == PlayerState.Moving)
            {
                MoveError error = referee.Move(this, fromPos, toPos);
                if (error != MoveError.Valid)
                {
                    return error;
                }
            }
            if(State == PlayerState.Flying){
                MoveError error = referee.Fly(this, fromPos, toPos);
                if(error != MoveError.Valid){
                    return error;
                }
            }
           
            for (int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(fromPos))
                {
                    Cows[i].pos = toPos;
                    break;
                }
            }
            return MoveError.Valid;
        }

        public bool hasCowAtPos((char, int) pos)
        {
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(pos) && Cows[i].status.Equals(cowStatus.Placed))
                {
                    return true;
                }
            }
            return false;
        }
        
        public int numCowsOnBoard()
        {
            int count = 0;
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].status.Equals(cowStatus.Placed)) count++;
            }
            return count;
        }

        public MoveError killCow((char, int) pos,IReferee referee)
        {
            MoveError error = referee.KillCow(this, pos);
            if (error != MoveError.Valid)
                return error;
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(pos))
                {
                    Cows[i].pos = ('Z', 0);
                    Cows[i].status = cowStatus.Dead;
                    break;
                }
            }
            return MoveError.Valid;
        }

        public PlayerState changePlayerState()
        {
            if (this.Cows.All(cow => cow.status == cowStatus.Placed)
            && this.State == PlayerState.Placing){
                return PlayerState.Moving;
            }
            else if (this.State == PlayerState.Moving &&
            this.Cows.Count == 3){
                return PlayerState.Flying;
            }
            else {
                return this.State;
            }
        }
    }
    
}

