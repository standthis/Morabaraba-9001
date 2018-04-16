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


        private MoveError placeMove(IPlayer player)
        {
            (char, int) toPos;

            toPos = player.GetMove("Where do you want to place your cow?: ");

            return GameBoard.PlaceCow(player, toPos);
        }


        private MoveError moveMove(IPlayer player)
        {
            (char, int) toPos, fromPos;

            fromPos = player.GetMove("Where do you want to move from?: ");
            toPos = player.GetMove("Where do you want to move to?: ");

            return GameBoard.MoveCow(player, toPos, fromPos);
        } 


        private MoveError moveFly(IPlayer player)
        {
            (char, int) toPos, fromPos;

            fromPos = player.GetMove("Where do you want to fly from?: ");
            toPos = player.GetMove("Where do you want to fly to?: ");

            return GameBoard.FlyCow(player, toPos, fromPos);
        }


        public MoveError Play(IPlayer player, PlayerState state)
        {
            switch (state)
            {
                case PlayerState.Placing:
                    return placeMove(player);

                case PlayerState.Moving:
                    return moveMove(player);

                case PlayerState.Flying:
                    return moveFly(player);

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

    /*    let pos = getPos "Which cow do you want to kill?"
    let playerMill = getPlayerMills player 
    match (isValidMove pos player.Positions), (isInMill pos playerMill), (canKillCowInMill playerMill player) with
    | true, true, true | true, false, _ -> removePiece player (getCoords pos)
    | true, true, _ -> 
        printfn "Can't kill cow in mill unless all cows are in mills" 
        killCow player
    | _ -> 
        printfn "No valid cow was in pos %A" pos
        killCow player 
        public MoveError KillCow()
        {
            (char,int) killPos=player.GetMove("Which cow do you want to kill?");
            GameBoard.Mills(player)

        }*/

        public bool EndGame()
        {
            throw new NotImplementedException();
        }


        public MoveError KillCow(IPlayer player)
        {
            throw new NotImplementedException();
        }


        public MoveError KillCow()
        {
            throw new NotImplementedException();
        }
    }
}
