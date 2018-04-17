using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{
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
            if (player.hasCowAtPos(fromPos))
            {
                toPos = player.GetMove("Where do you want to move to?: ");
                if (emptyTile(toPos) && GameBoard.AllTiles[fromPos].PossibleMoves.Any(tile => tile.Equals(toPos)))
                {
                    player.moveCow(fromPos, toPos);
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
            (char, int) toPos = player.GetMove("Where do you want to place your cow?: ");
            if (emptyTile(toPos))
            {
                player.placeCow(toPos); 
                return MoveError.Valid;
            }
            else
            {
                return MoveError.InValid;
            }
        }

        public bool emptyTile((char, int) pos)
        {
            if (CurrentPlayer.hasCowAtPos(pos) || EnemyPlayer.hasCowAtPos(pos))
            {
                return false;
            }
            return true;
        }

        private MoveError Fly(IPlayer player)
        {
            //if (player.State != PlayerState.Flying)
            //  throw new IncorrectStateException();
            (char, int) toPos, fromPos;
            fromPos = player.GetMove("Where do you want to fly from?: ");
            if (player.hasCowAtPos(fromPos))
            {
                toPos = player.GetMove("Where do you want to fly to?: ");
                if (emptyTile(toPos))
                {
                    player.moveCow(fromPos, toPos);
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


        public void ChangePlayerTurn()
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
                ChangePlayerTurn();
            }
        }

        public IPlayer Winner()
        {
            throw new NotImplementedException();
        }


        public MoveError KillCow()
        {
            (char,int) killPos = CurrentPlayer.GetMove("Which cow do you want to kill?");
            IEnumerable<ITile> enemyPlayerMills = GameBoard.Mills(EnemyPlayer);

            if (EnemyPlayer.hasCowAtPos(killPos))
            {
                if (enemyPlayerMills.Contains(GameBoard.AllTiles[killPos]))
                {
                    if (enemyPlayerMills.Count() == EnemyPlayer.numCowsOnBoard())
                    {
                        EnemyPlayer.killCow(killPos);
                        return MoveError.Valid;
                    }
                    else
                    {
                        //can't kill cow in mill
                        return MoveError.InValid;
                    }
                }
                else
                {
                    EnemyPlayer.killCow(killPos);
                    return MoveError.Valid;
                }
            }
            else
            {
                return MoveError.NoCow;
            }
        }

        public bool PlayerCanMove(IPlayer player) {
            for (int i = 0; i < 12; i++)
            {
                if (player.Cows[i].status == cowStatus.Placed)
                {
                    IEnumerable<(char, int)> posMoves = GameBoard.PossibleMoves(player.Cows[i].pos);
                    for (int j = 0; j < posMoves.Count(); j++)
                    {
                        if (emptyTile(posMoves.ElementAt<(char, int)>(i)))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public GameEnd EndGame()
        {
            if (EnemyPlayer.State == PlayerState.Moving)
            {
                if (!PlayerCanMove(EnemyPlayer))
                    return GameEnd.CantMove;
               
                if (EnemyPlayer.numCowsOnBoard() == 2)
                    return GameEnd.KilledOff;
            }
                return GameEnd.NoEnd;
        }
          
    }
}
