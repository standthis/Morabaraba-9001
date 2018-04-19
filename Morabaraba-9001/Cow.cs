using System;
namespace Morabaraba_9001
{

    public class Cow : ICow
    {
        public Cow(Color color,(char,int)pos)
        {
            Color = color;
            Pos = pos;

        }

     

        public Color Color { get; set; }
        public (char, int) Pos { get; set; }
       
        
    }
}
