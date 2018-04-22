using System;
using System.Collections.Generic;

namespace Morabaraba_9001
{
    public enum Color { dark, light,none }

    public enum Symbol { x, o }

    public enum PlayerState { Placing, Moving, Flying }

    public enum GameEnd { NoEnd, CantMove, KilledOff }

    public enum MoveError { NoCow, InValid, Valid }

    public enum cowStatus { Unplaced, Placed, Dead }

    public interface ICow
    {
        //Symbol symbol {get;}
        Color Color { get; }
        (char, int) Pos { get; set; }
       
    }

    public interface ITile
    {
        (char, int) Pos { get; }
        IEnumerable<(char, int)> PossibleMoves { get; }
        Color color { get; set; }
    }

    public interface IBoard
    {
        List<IEnumerable<ITile>> Mills(IPlayer player);
        IEnumerable<(char, int)> PossibleMoves((char, int) pos);
        Dictionary<(char, int), ITile> AllTiles { get; }
        IEnumerable<IEnumerable<ITile>> AllBoardMills { get;}
  


        //    Dictionary<(char, int), ICow> allCows { get; }
    }

    public interface IPlayer
    {
        string Name { get; }
        Color Color { get; }
        PlayerState State { get; }
        List<ICow> Cows { get; }
        int UnplacedCows { get; }
        void placeCow((char, int) toPos);
        void moveCow((char, int) fromPos, (char, int) toPos);
        bool hasCowAtPos((char, int) pos);
        int numCowsOnBoard();
        MoveError killCow((char, int) pos);
        void changePlayerState();
    }


    public interface IReferee
    {
        
       
        MoveError Place(IPlayer player,IPlayer otherPlayer, (char, int) toPos);
        MoveError Move(IPlayer player,IPlayer otherPlayer, (char, int) fromPos, (char, int) toPos);
        MoveError Fly(IPlayer player,IPlayer otherPlayer, (char, int) fromPos, (char, int) toPos);
        MoveError KillCow(IPlayer player, (char, int) killPos);
        GameEnd EndGame(IPlayer enemyPlayer,IPlayer otherPlayer);
        bool MillFormed(IPlayer player, (char, int) pos);
        IBoard GameBoard { get; }

      
    }
    public interface IGame
    {
     
        MoveError Place((char,int) pos);
        MoveError Move((char, int) fromPos,(char,int) toPos);
        MoveError Fly((char, int) fromPos,(char,int) toPos);
        IBoard GameBoard { get; }
        IReferee Referee { get; }
        IPlayer Player_1 { get; }
        IPlayer Player_2 { get; }
        IPlayer Winner { get; }

        void StartGame();
        GameEnd EndGame();
        bool IsTileOccupied((char, int) pos);
        bool isMillFormed((char, int) pos);

        void ChangePlayerTurn();
        IPlayer CurrentPlayer { get; }
        IPlayer OtherPlayer { get; }
        MoveError KillCow((char, int) killPos);



    }
}
