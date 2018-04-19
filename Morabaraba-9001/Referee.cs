using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{
    public class Referee : IReferee
    {
        
        public IBoard GameBoard { get; }


        public Referee(IBoard board)
        {
            
            GameBoard = board;

        }
        public MoveError Place(IPlayer player, (char, int) toPos)
        {
            if (player.State != PlayerState.Placing)
            {
                return MoveError.InValid;
            }

            if (!GameBoard.isOccupied(toPos))
            {
                return MoveError.Valid;
            }
            else
            {
                return MoveError.InValid;
            }
        }
        public MoveError Move(IPlayer player, (char, int) fromPos, (char, int) toPos)
        {
            if (player.State != PlayerState.Moving)
            {
                return MoveError.InValid;
            }
            if (player.hasCowAtPos(fromPos))
            {
                if (!GameBoard.isOccupied(toPos) && GameBoard.AllTiles[fromPos].PossibleMoves.Any(tile => tile.Equals(toPos)))
                {

                    return MoveError.Valid;
                }
                else{
                    return MoveError.InValid;
                }
            }
            else
            {
                return MoveError.NoCow;
            }
        }

       
        public MoveError Fly(IPlayer player,(char, int) fromPos, (char, int) toPos)
        {
            if (player.State != PlayerState.Flying){
                return MoveError.InValid;
            }
       
         
            if (player.hasCowAtPos(fromPos))
            {
                if (!GameBoard.isOccupied(toPos))
                {

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
        

    

        private bool IsInMill(List<ITile> mill, (char,int) pos){
            return mill.Any(tile => tile.Pos.Equals(pos));
        }
        public MoveError KillCow(IPlayer player, (char,int) killPos)
        {
          
            List<IEnumerable<ITile>> playerMills = GameBoard.Mills(player);

            if (player.hasCowAtPos(killPos))
            {
                if (playerMills.Any(mill => IsInMill(mill.ToList(),killPos)) ) //check if killPos in any of players mills
                {
                    if (playerMills.Count() == player.numCowsOnBoard())
                    {
                        
                        return MoveError.Valid;
                    }
                    else
                    {
                        //can't kill cow in mill
                        return MoveError.InValid;
                    }
                }
                else
                    return MoveError.Valid;
             }

            else
            {
                return MoveError.NoCow;
            }
        }

        public bool PlayerCanMove(IPlayer player) {
           // player.Cows.Where(cow=> cow)
            foreach(ICow cow in player.Cows){
                IEnumerable<(char,int)> posMoves=GameBoard.PossibleMoves(cow.Pos);
                foreach((char,int) position in posMoves){
                    if(!GameBoard.isOccupied(position)){
                        return true;
                    }
                }
            }
            return false;
        }

       
          
    }
}
