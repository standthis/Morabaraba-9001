using System;
namespace Morabaraba9001
{
    public class Player : IPlayer
    {
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            CowsUnPlaced = 12;
            CowsOnBoard = 0;
            State = PlayerState.Placing;
        }

        public string Name { get; }

        public Color Color { get; }

        public int CowsUnPlaced { get; private set; }
        public int CowsOnBoard { get; private set; }

        public PlayerState State { get; private set; }

        public (char, int) GetMove(string what)
        {
            Console.WriteLine(what);
            Console.Write("Row: ");
            char rowInput = Console.ReadKey().KeyChar;
            bool is_char = Char.TryParse(rowInput + "", out rowInput);
            Console.WriteLine();
            Console.Write("Column: ");
            char getCol = Console.ReadKey().KeyChar;
            Console.WriteLine();
            int col;
            bool is_numeric = System.Int32.TryParse(getCol + "", out col);
            if (is_numeric && is_char)
            {
                return (Char.ToUpper(rowInput), col);

            }
            else
            {
                Console.WriteLine("Row requires a character and Col requires a number.Please enter valid input ");
                return GetMove(what);
            }


        }
        public bool addCow()
        {
            CowsOnBoard += 1;
            if (CowsOnBoard > 12)
            { //can't have more than 12 cows
                return false;
            }
            return true;


        }
        public bool DecrementCowsOnBoard()
        {
            CowsOnBoard -= 1;
            if (State == PlayerState.Moving)
            {
                if (CowsOnBoard == 3)
                {
                    State = PlayerState.Flying;
                }

            }
            else if (State == PlayerState.Flying)
            {

                if (CowsOnBoard < 2)
                {

                    return false;
                }
            }
            return true;
        }
        public bool DecrementCowsPlaced()
        {
            CowsUnPlaced -= 1;

            if (CowsUnPlaced == 0)
            {
                State = PlayerState.Moving;
            }
            else if (CowsUnPlaced < 0)
            {
                return false;
            }
            return true;
        }

    }





}

