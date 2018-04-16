using System;
namespace Morabaraba_9001
{

    public class Cow : ICow
    {
        public Cow(Color c)
        {
            Color = c;
       
        }

        public Color Color { get; set; }
   


    }
}
