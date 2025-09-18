using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal class GameManager
    {
        DieRoller dieRoller = new DieRoller();

        private int score;

        public void Play()
        {
            //call Roll() and print to console
            Console.WriteLine(dieRoller.Roll().ToString());
        }

        private void OnPointAcquired()
        {
            score++;
            string gainedMessage = "You gained a point!";
            Console.WriteLine(gainedMessage);
        }

        public void PlayGame()
        {
            Console.WriteLine("Welcome to Die vs Die!");

            OnPointAcquired();
        }


    }
}
