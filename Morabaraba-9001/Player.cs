using System;
using System.Collections.Generic;
namespace Morabaraba_9001
{
    public class Player : IPlayer
    {
        public Player(string name, Color color)
        {
            Name = name;
            Color = color;
            State = PlayerState.Placing;
            Cows = new List<ICow>();
            for (int i = 0; i < 12; i++)
            {
                Cows.Add(new Cow(Color));
            }
        }

        public List<ICow> Cows { get; private set; }
        public string Name { get; }

        public Color Color { get; }

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

        public void placeCow((char, int) toPos)
        {
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].status == cowStatus.Unplaced)
                {
                    Cows[i].pos = toPos;
                    Cows[i].status = cowStatus.Placed;
                    break;
                }
            }
        }

        public bool hasCowAtPos((char, int) pos)
        {
            for(int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(pos))
                {
                    return true;
                }
            }
            return false;
        }
        
        public void moveCow((char, int) fromPos, (char, int) toPos)
        {
            for (int i = 0; i < 12; i++)
            {
                if (Cows[i].pos.Equals(fromPos))
                {
                    Cows[i].pos = toPos;
                    break;
                }
            }
        }

    }
    
}

