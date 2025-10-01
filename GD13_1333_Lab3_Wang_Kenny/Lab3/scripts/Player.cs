using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal class Player
    {
        public string UserName = "";
        //Replace with List
        List<string> Inventory = new List<string> { "d6", "d8", "d12", "d20", "d50" };
        public bool isBot = false;
        public int Score;
        int PlayerResult, CPUResult;
        DieRoller dieRoller = new DieRoller();

        public int Turn()
        {
            if (isBot)
            {
                Random selection = new Random();

                //picks a random possible die from the "inventory"
                int randomIndex = selection.Next(Inventory.Count);

                string choice = Inventory[randomIndex];

                //Use dieroller to get the result
                int result = dieRoller.Roll(choice);

                //store the result
                CPUResult = result;

                Console.WriteLine("CPU has picked their dice. They picked " + choice);
                return result;
            }
            else
            {
                
                //Get player input and display all available choices
                Console.WriteLine("Select a dice to play: " + string.Join(", ", Inventory));
                string playersPick = Console.ReadLine();
                int result = 0;
                //Check validity of player's choice
                if (playersPick != null && Inventory.Contains(playersPick))
                {
                    result = dieRoller.Roll(playersPick);
                    PlayerResult = result;
                    Console.WriteLine(UserName + " has picked their dice: " + playersPick);
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Try again.");
                    Turn();
                }
                return result;
            }
        }
    }
}
