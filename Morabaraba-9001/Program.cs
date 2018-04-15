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
            ICow Occupant((char, int) pos);
            IEnumerable<ICow> Cows(Color c);
            IEnumerable<ICow> Mills(IPlayer player);
            void MoveCow(ICow cow, IPos fromPos, IPos toPos);
            void PlaceCow(ICow cow, IPos pos);
            void FlyCow(ICow cow, IPos fromPos, IPos toPos);
            void KillCow(IPos pos, IPlayer player);

            IEnumerable<IPos> getPossibleMoves(IPos pos);
            //    Dictionary<(char, int), ICow> allCows { get; }
        }


        public enum Color { dark, light }

        public enum Symbol { x, o }

        public enum PlayerState { Placing, Moving, Flying }

        public interface IPlayer
        {
            string Name { get; }
            Color Color { get; }
            (char, int) GetMove();
            int Unplayed { get; }
            PlayerState State { get; }
            IEnumerable<IPos> Cows { get; }
        }

        public interface ICow
        {
            //Symbol symbol {get;}
            Color Color { get; }
            IPos Pos { get; }

        }
        public interface ITile
        {
            ICow Cow { get; set; }
            IEnumerable<(char,int)> PossibleMoves { get; }

        }
        public class Tile : ITile
        {

            private ICow cow;
            private IEnumerable<(char, int)> possibleMoves;
            public Tile(ICow cow, IEnumerable<(char, int)> possibleMoves)
            {
                this.cow = cow;
                this.possibleMoves = possibleMoves;
            }

            public ICow Cow { 
                get 
                {

                    return cow;
                }

                set{
                    cow = value;   
                }
            }



            public IEnumerable<(char, int)> PossibleMoves
            {
                get{
                    return possibleMoves;
                   }
            }

           
        }

        public interface IReferee
        {
            IPlayer Winner();
            IPlayer noMove();
            IPlayer getCurrentPlayer();
            void Play();
        }

        public class IlligalMoveException : ApplicationException { }

        public class Board : IBoard
        {
         
         
            public ICow Occupant((char, int) pos)
            {
                //    return allCows[pos];
                return null;
            }

            public IEnumerable<ICow> Cows(Color c)
            {
                //  return allCows.Values.Where(cow => cow.Color == c).ToArray();
                return null;

            }
           
            public IEnumerable<ICow> Mills(IPlayer player)
            {
                //    return allMills.Where
                return null;
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

         

            public void MoveCow(ICow cow, IPos fromPos, IPos toPos)
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

            public IPos Pos { get; set; }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Morabaraba!");
        }
    }
}
