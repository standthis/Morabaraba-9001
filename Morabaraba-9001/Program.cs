using System;
using System.Collections.Generic;
using System.Linq;

namespace Morabaraba_9001
{
    class Program
    {
        public interface IPos
        {
            char getRow();
            int getCol();
        }
        public interface IBoard
        {
            ICow Occupant(IPos pos);
            IEnumerable<ICow> Cows(IPlayer player);
            IEnumerable<ICow> Mills(IPlayer player);  
            void MoveCow(ICow cow, IPos fromPos, IPos toPos);
            void PlaceCow(ICow cow, IPos pos);
            void FlyCow(ICow cow, IPos fromPos, IPos toPos);
            void KillCow(IPos pos, IPlayer player);
            //Dictionary<IPos, IEnumerable<IPos>> PossibleMoves { get; }
            IEnumerable<IPos> getPossibleMoves(IPos pos);
        }


        public enum Color { dark, light }

        public enum Symbol { x, o }

        public enum PlayerState { Placing, Moving, Flying }

        public interface IPlayer
        {
            string Name { get; }
            Color c { get; }
            (char, int) GetMove();
            int Unplayed { get; }
            PlayerState State { get; }
        }

        public interface ICow
        {
            //Symbol symbol {get;}
            Color Color { get; }
            IPos Pos { get; }
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
            public IEnumerable<ICow> Cows(IPlayer player)
            {
                throw new NotImplementedException();
            }

            public void FlyCow(ICow cow, IPos fromPos, IPos toPos)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<IPos> getPossibleMoves(IPos pos)
            {
                throw new NotImplementedException();
            }

            public void KillCow(IPos pos, IPlayer player)
            {
                throw new NotImplementedException();
            }

            public IEnumerable<ICow> Mills(IPlayer player)
            {
                throw new NotImplementedException();
            }

            public void MoveCow(ICow cow, IPos fromPos, IPos toPos)
            {
                throw new NotImplementedException();
            }

            public ICow Occupant(IPos pos)
            {
                throw new NotImplementedException();
            }

            public void PlaceCow(ICow cow, IPos pos)
            {
                throw new NotImplementedException();
            }
        }

        public class Cow : ICow
        {
            public Cow(Color c, IPos pos)
            {
                Color = c;
                Pos = pos;
            }

            public Color Color { get; set; }

            public IPos Pos { get; private set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Morabaraba!");
        }
    }
}
