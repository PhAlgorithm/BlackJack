using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public struct Card
    {
        public Suit Suit;
        public Face Face;

        public override String ToString()
        {
            return String.Format("{0,-6} of {1,5}", Face, Suit);
        }

    }
}
