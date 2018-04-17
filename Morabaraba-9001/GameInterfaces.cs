using System;
using System.Collections.Generic;

namespace Morabaraba_9001
{
    public enum Color { dark, light }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

    public enum GameEnd { NoEnd, CantMove, KilledOff }

    public enum MoveError { NoCow, InValid, Valid }

    public enum cowStatus { Unplaced, Placed, Dead }

    public interface ICow
    {
        //Symbol symbol {get;}
        Color Color { get; }
        (char, int) pos { get; set; }
        cowStatus status { get; set; }
    }

    public interface ITile
    {
        (char, int) Pos { get; }
        IEnumerable<(char, int)> PossibleMoves { get; }
    }

    public interface IBoard
    {
        IEnumerable<ITile> Mills(IPlayer player);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> AllTiles { get; }
        IEnumerable<IEnumerable<ITile>> AllBoardMills { get; }

        //    Dictionary<(char, int), ICow> allCows { get; }
    }

    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
        PlayerState State { get; }
        List<ICow> Cows { get; }
        (char, int) GetMove(string what);
        MoveError placeCow((char, int) toPos,IReferee referee);
        MoveError moveCow((char, int) fromPos, (char, int) toPos,IReferee referee);
        bool hasCowAtPos((char, int) pos);
        int numCowsOnBoard();
        MoveError killCow((char, int) pos,IReferee referee);
        void changePlayerState();
    }


    public interface IReferee
    {
        IPlayer Winner();
        //MoveError Play(IPlayer player, PlayerState state);
        MoveError Place(IPlayer player, (char, int) toPos);
        MoveError Move(IPlayer player, (char, int) fromPos, (char, int) toPos);
        MoveError Fly(IPlayer player, (char, int) fromPos, (char, int) toPos);
        MoveError KillCow(IPlayer player, (char, int) killPos);

        GameEnd EndGame();
        bool PlayerCanMove(IPlayer player);
        void UpdatePlayers(IPlayer currentPlayer, IPlayer enemyPlayer);
       // void StartGame();
        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get; }
        bool emptyTile((char, int) pos);
    }




}
