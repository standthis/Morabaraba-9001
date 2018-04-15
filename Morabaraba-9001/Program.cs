using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{
    class Program
    {
        public interface Pos
        {
            char Row { get; }
            int Col { get; }
        }
        public interface IBoard
        {
            ICow Occupant(Pos pos);
            IEnumerable<ICow> Cows(Symbol symbol);
            IEnumerable<ICow> Mills(Symbol symbol);
            void MoveCow(ICow cow, Pos pos);
            void PlaceCow(Pos pos);
            void FlyCow(Pos pos);
            void KillCow(Pos pos, Symbol symbol);
        }


        public enum Color { dark, light }

        public enum Symbol { x, o }

        public enum PlayerState { Placing, Moving, Flying }

        public interface IPlayer
        {
            string Name { get; }
            (char, int) GetMove();
            int Unplayed { get; }
            PlayerState State { get; }
        }

        public interface ICow
        {
            IEnumerable<Pos> PossibleMoves { get; }
            //Symbol symbol {get;}
            Color Color { get; }
            Pos Pos { get; }
            IEnumerable<Pos> AvailableMoves(IBoard board);
        }


        public interface IReferee
        {
            IPlayer Winner();
            IPlayer noMove();
            void Play();
        }

        public class IlligalMoveException : ApplicationException { }

        public class Board : IBoard
        {
            ICow[] light, dark;

            public Board()
            {
            }
            public IEnumerable<ICow> Cows(Symbol symbol)
            {
                throw new NotImplementedException();
            }

            public void FlyCow(Pos pos)
            {
                throw new NotImplementedException();
            }

            public void KillCow(Pos pos, Symbol symbol)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ICow> Mills(Symbol symbol)
            {
                throw new NotImplementedException();
            }

            public void MoveCow(ICow cow, Pos pos)
            {
                throw new NotImplementedException();
            }

            public ICow Occupant(Pos pos)
            {
                throw new NotImplementedException();
            }

            public void PlaceCow(Pos pos)
            {
                throw new NotImplementedException();
            }
        }

        public class Cow : ICow
        {
            public Cow(Color c, Pos pos, Pos[] possibleMoves)
            {
                Color = c;
                Pos = pos;
                PossibleMoves = possibleMoves;
            }

            public IEnumerable<Pos> PossibleMoves { get; }
            public Color Color { get; set; }

            public Pos Pos { get; private set; }

            public IEnumerable<Pos> AvailableMoves(IBoard board)
            {
                throw new NotImplementedException();
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Morabaraba!");
        }
    }
}
