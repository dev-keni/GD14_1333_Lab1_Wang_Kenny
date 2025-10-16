using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal class DieRoller
    {
        public int Roll(string input)
        {

            int num;
            //new random
            Random random = new Random();

            if (input == "Revolver")
            {
                input = "d6";
            }
            if (input == "MP5")
            {
                input = "d30";
            }
            if (input == "Shotgun")
            {
                input = "d20";
            }
            if (input == "Pipebomb")
            {
                input = "d" + random.Next(1, 50);
            }

            //remove "d" from the input string so it can be converted to int
            string cleanedInput = input.Replace("d", "");
            bool successfullyParsed = int.TryParse(cleanedInput, out num);

            //roll selected die
            int result = random.Next(1,num+1);
            
            return result;
        }

    }
}
