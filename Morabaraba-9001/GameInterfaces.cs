using System;
using System.Collections.Generic;

namespace Morabaraba_9001
{
    public enum Color { dark, light }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

    public enum GameEnd { NoEnd, CantMove, KilledOff }

    public enum MoveError { NoCow, InValid, Valid }

    public interface ICow
    {
        //Symbol symbol {get;}
        Color Color { get; }
   

    }
    public interface ITile
    {
        ICow Cow { get; set; }
        IEnumerable<(char, int)> PossibleMoves { get; }
    }

    public interface IBoard
    {
        ICow Occupant((char, int) pos);
        IEnumerable<ITile> Cows(Color c);
        IEnumerable<ITile> Mills(IPlayer player);
        MoveError MoveCow(IPlayer player, (char, int) fromPos, (char, int) toPos);
        MoveError PlaceCow(IPlayer player, (char, int) pos);
        MoveError FlyCow(IPlayer player, (char, int) fromPos, (char, int) toPos);
        MoveError KillCow((char, int) killPos, IPlayer player);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> AllTiles { get; }
        IEnumerable<IEnumerable<ITile>> AllBoardMills { get; }

        //    Dictionary<(char, int), ICow> allCows { get; }
    }

    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }

        int CowsUnPlaced { get; }
        int CowsOnBoard { get; }
        PlayerState State { get; }

        List<ICow> Cows { get; }
        (char, int) GetMove(string what);
        bool DecrementCowsOnBoard();
        bool DecrementCowsPlaced();
        bool AddCowToBoard();
    }


    public interface IReferee
    {
        IPlayer Winner();
        MoveError Play(IPlayer player, PlayerState state);
        MoveError KillCow();
        GameEnd EndGame();
        bool PlayerCanMove();
        void ChangePlayerTurn();
        void StartGame();
        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get; }


    }




}
