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
        void MoveCow(IPlayer player, (char, int) fromPos, (char, int) toPos);
        void PlaceCow(IPlayer player, (char, int) pos);
        void FlyCow(IPlayer player, (char, int) fromPos, (char, int) toPos);
        void KillCow((char, int) killPos);
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
        void placeCow((char, int) toPos);
        void moveCow((char, int) fromPos, (char, int) toPos);
        bool hasCowAtPos((char, int) pos);

    }


    public interface IReferee
    {
        IPlayer Winner();
        MoveError Play(IPlayer player, PlayerState state);
        MoveError KillCow(IPlayer player);
        GameEnd EndGame();
        bool PlayerCanMove();
        void ChangePlayerTurn();
        void StartGame();
        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get; }


    }




}
