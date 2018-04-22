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
        public MoveError Place(IPlayer player,IPlayer otherPlayer, (char, int) toPos)
        {
            
            if (player.State != PlayerState.Placing || player.UnplacedCows<=0)
            {
                return MoveError.InValid;
            }

            if (!isTileOccupied(player, otherPlayer, toPos))
            {
                return MoveError.Valid;
            }
            else
            {
                return MoveError.InValid;
            }
        }
        public MoveError Move(IPlayer player,IPlayer otherPlayer, (char, int) fromPos, (char, int) toPos)
        {
            if (player.State != PlayerState.Moving)
            {
                return MoveError.InValid;
            }
            if (player.hasCowAtPos(fromPos))
            {
                if (!isTileOccupied(player, otherPlayer, toPos) && GameBoard.AllTiles[fromPos].PossibleMoves.Any(tile => tile.Equals(toPos)))
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

       
        public MoveError Fly(IPlayer player,IPlayer otherPlayer,(char, int) fromPos, (char, int) toPos)
        {
            if (player.State != PlayerState.Flying || player.numCowsOnBoard()!=3){
                return MoveError.InValid;
            }
       
         
            if (player.hasCowAtPos(fromPos))
            {
                if (!isTileOccupied(player,otherPlayer,toPos))
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
            int CowsInMills = 0;
            foreach (ICow cow in player.Cows)
            {
                if(playerMills.Any(mill => IsInMill(mill.ToList(), cow.Pos))){
                    CowsInMills++;
                }
      
                
            }

            if (player.hasCowAtPos(killPos))
            {
                if (playerMills.Any(mill => IsInMill(mill.ToList(),killPos)) ) //check if killPos in any of players mills
                {
                    if (CowsInMills == player.numCowsOnBoard())
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

        private bool isInMill(IEnumerable<ITile> mill, (char, int) pos)
        {
            return mill.Any(tile => tile.Pos.Equals(pos));
        }

        public bool MillFormed(IPlayer player, (char, int) pos)
        {
            List<IEnumerable<ITile>> playersMills = GameBoard.Mills(player);

            foreach (IEnumerable<ITile> mill in playersMills)
            {
                if (isInMill(mill, pos))
                {
                    return true;
                }
            }
            return false;
               
        }
    
        private bool PlayerCanMove(IPlayer player,IPlayer otherPlayer) {
           // player.Cows.Where(cow=> cow)
            foreach(ICow cow in player.Cows){
                IEnumerable<(char,int)> posMoves=GameBoard.PossibleMoves(cow.Pos);
                foreach((char,int) position in posMoves){
                    if(!isTileOccupied(player,otherPlayer,position)){
                        return true;
                    }
                }
            }
            return false;
        }
        private bool isTileOccupied(IPlayer player,IPlayer otherPlayer,(char,int) pos){
            return player.hasCowAtPos(pos) || otherPlayer.hasCowAtPos(pos);
            
        }
        public GameEnd EndGame(IPlayer enemyPlayer,IPlayer currentPlayer)
        {
            if (enemyPlayer.State == PlayerState.Moving)
                if (!PlayerCanMove(enemyPlayer,currentPlayer))
                    return GameEnd.CantMove;
            if (enemyPlayer.numCowsOnBoard() == 2 && enemyPlayer.State == PlayerState.Flying)
                return GameEnd.KilledOff;
            return GameEnd.NoEnd;
        }
       
          
    }
}
