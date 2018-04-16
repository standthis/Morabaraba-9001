using System;
namespace Morabaraba9001
{

    public class Cow : ICow
    {
        public Cow(Color c,(char,int) pos)
        {
            Color = c;
            Pos = pos;
        }

        public Color Color { get; set; }
        public (char, int) Pos { get; set; }


    }
}
