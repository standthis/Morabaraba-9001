using System;
using System.Collections.Generic;

namespace Morabaraba9001
{
    public enum Color { dark, light }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

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
        void MoveCow(ICow cow, (char, int) fromPos, (char, int) toPos);
        void PlaceCow(ICow cow, (char, int) pos);
        void FlyCow(ICow cow, (char, int) fromPos, (char, int) toPos);
        void KillCow((char, int) pos);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> AllTiles { get; }
        IEnumerable<IEnumerable<ITile>> AllBoardMills { get; }
        //    Dictionary<(char, int), ICow> allCows { get; }
    }

    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
        (char, int) GetMove(string what);
        int CowsUnPlaced { get; }
        int CowsOnBoard { get; }
        PlayerState State { get; }
        bool DecrementCowsOnBoard();
        bool DecrementCowsPlaced();
        bool AddCow();
    }


    public interface IReferee
    {
        IPlayer Winner();
        // IPlayer noMove();
        MoveError Play(IPlayer player, PlayerState state);
        void changePlayerTurn();
        void StartGame();
        // IPlayer player_1 { get; set; }
        //IPlayer player_2 { get; set; }

        IBoard GameBoard { get; }
        IPlayer EnemyPlayer { get; }
        IPlayer CurrentPlayer { get; }


    }




}
