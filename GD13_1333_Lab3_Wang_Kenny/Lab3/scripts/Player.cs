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
        public List<Items> Inventory = new List<Items> { new Revolver(), new PipeBomb() };

        public bool isBot = false;
        public int Score;
        int PlayerResult, CPUResult;
        DieRoller dieRoller = new DieRoller();

        private void RemoveItem(string itemToRemove)
        {
            bool removed = Inventory.Remove(Revolver());
        }

        public void GenerateItem()
        {

        }

        public int Turn()
        {
            if (isBot)
            {
                Random selection = new Random();

                //picks a random possible die from the "inventory"
                int randomIndex = selection.Next(Inventory.Count);

                Items choice = Inventory[randomIndex];

                //Use dieroller to get the result
                int result = dieRoller.Roll(choice.ReturnDamage());

                //store the result
                CPUResult = result;

                Console.WriteLine("CPU has picked their dice. They picked " + choice);
                return result;
            }
            else
            {
                
                //Get player input and display all available choices
                Console.WriteLine("Select a dice to play: " + string.Join(", ", Inventory));
                string? playersPick = Console.ReadLine();
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
