using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba9001
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
            else
            {
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
}
