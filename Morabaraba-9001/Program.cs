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
            IEnumerable<ICow> Cows(Color c);
            IEnumerable<ICow> Mills(Color c);
            
            void MoveCow(ICow cow, Pos pos);
            void PlaceCow(Pos pos);
            void FlyCow(Pos pos);
            void KillCow(Pos pos, Color c);
            Dictionary<Pos, IEnumerable<Pos>> PossibleMoves { get; }
            IEnumerable<Pos> AvailableMoves(ICow cow);  
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
            //Symbol symbol {get;}
            Color Color { get; }
            Pos Pos { get; }
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

            public Dictionary<Pos, IEnumerable<Pos>> PossibleMoves => throw new NotImplementedException();

            public IEnumerable<Pos> AvailableMoves(ICow cow)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ICow> Cows(Color c)
            {
                throw new NotImplementedException();
            }

            public void FlyCow(Pos pos)
            {
                throw new NotImplementedException();
            }

            public void KillCow(Pos pos, Color c)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ICow> Mills(Color c)
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
            public Cow(Color c, Pos pos)
            {
                Color = c;
                Pos = pos;
            }

            public Color Color { get; set; }

            public Pos Pos { get; private set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Morabaraba!");
        }
    }
}
