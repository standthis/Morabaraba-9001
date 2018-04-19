using System;
using System.Collections.Generic;
using System.Linq;
namespace Morabaraba_9001
{
    public class Player : IPlayer
    {
        public Player(string name, Color color,PlayerState state=PlayerState.Placing,List<(char,int)> positions= null)
        {
            Name = name;
            Color = color;
            State = state;
            Cows = new List<ICow>();
            if(positions!=null){
                foreach ((char,int) pos in positions)
                {
                    Cows.Add(new Cow(this.Color, pos));
                        
                }
                
            }
            switch (State)
            {
                case PlayerState.Placing:
                    UnplacedCows = 12 - Cows.Count();
                    break;
                case PlayerState.Moving:
                    UnplacedCows = 0;
                    break;
                case PlayerState.Flying:
                    UnplacedCows = 0;
                    if (numCowsOnBoard() > 3)
                    {
                        State = PlayerState.Moving;
                    }

                    break;
            }

            changePlayerState();
        }
     

        public List<ICow> Cows { get; private set; }
        public string Name { get; }

        public Color Color { get; }

        public PlayerState State { get; private set; }
        public int UnplacedCows { get; private set; }

    
       

        public void placeCow((char, int) toPos)
        {
         
            Cows.Add(new Cow(Color, toPos));
            UnplacedCows--;
            changePlayerState();

        }


        public void moveCow((char, int) fromPos, (char, int) toPos)
        {


            //get rid of the from positions
            Cows = Cows.Where(cow => !cow.Pos.Equals(fromPos)).ToList();
            Cows.Add(new Cow(Color, toPos));
            changePlayerState();

        }


        public bool hasCowAtPos((char, int) pos)
        {
            return Cows.Any(cow => cow.Pos.Equals(pos));
        }
        
        public int numCowsOnBoard()
        {
            return Cows.Count();
        }


        public MoveError killCow((char, int) pos, IReferee referee)
        {
            MoveError error = referee.KillCow(this, pos);
            if (error != MoveError.Valid)
                return error;
            //Cows.Remove(new Cow(Color, pos));
            Cows = Cows.Where(cow => !(cow.Pos.Equals(pos))).ToList();
          
            return MoveError.Valid;
        }

        public bool CanMove(IReferee referee){
            return referee.PlayerCanMove(this);
        }
        public void changePlayerState()
        {
            if(State == PlayerState.Placing && UnplacedCows==0){
                State = PlayerState.Moving;
            }
            else if (State == PlayerState.Moving && numCowsOnBoard()==3 ){
                State = PlayerState.Flying;
            }

        }
    }
    
}

