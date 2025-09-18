using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal class DieRoller
    {
        public int Roll()
        { 
            //new random
            Random random = new Random();

            //roll each die
            int d6 = random.Next(1,6);
            int d8 = random.Next(1,8);
            int d12 = random.Next(1,12);
            int d20 = random.Next(1,20);

            //add every die
            int total = d6 + d8 + d12 + d20;

            return total;
        }
    }
}
