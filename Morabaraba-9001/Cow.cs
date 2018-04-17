using System;
namespace Morabaraba_9001
{

    public class Cow : ICow
    {
        public Cow(Color c)
        {
            Color = c;
            pos = (' ', 0); //placeholder
            status = cowStatus.Unplaced;
        }

        public Cow((char, int) posi)//used for testing: creates a cow already placed at a given position
        {
            pos = posi;
            status = cowStatus.Placed;
        }

        public Color Color { get; set; }
        public (char, int) pos { get; set; }
        public cowStatus status { get; set; }
        
    }
}
