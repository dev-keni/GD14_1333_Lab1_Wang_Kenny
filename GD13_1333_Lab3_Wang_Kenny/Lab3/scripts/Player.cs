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
        public Dictionary<string, Items> Inventory = new Dictionary<string,Items>();

        public bool isBot = false;
        public int Score;
        int PlayerResult, CPUResult;
        DieRoller dieRoller = new DieRoller();

        private void RemoveItem(string itemToRemove)
        {
            bool removed = Inventory.Remove(itemToRemove);
        }

        public void GenerateItem()
        {
            int itemResult = dieRoller.Roll(4);
            switch (itemResult)
            {
                //Generate a random weapon
                case 1:
                    Inventory.Add("Revolver", new Revolver { });
                    Console.WriteLine("A revolver!");
                    break;
                case 2:
                    Inventory.Add("Shotgun", new Revolver { });
                    Console.WriteLine("A shotgun!");
                    break;
                case 3:
                    Inventory.Add("MP5", new MP5 { });
                    Console.WriteLine("A MP5!");
                    break;
                case 4:
                    Inventory.Add("Pipe Bomb", new PipeBomb { });
                    Console.WriteLine("A pipe bomb!");
                    break;
            }   
        }

        public int Turn()
        {
            if (isBot)
            {
                Random selection = new Random();
                //Give the bot a new weapon
                GenerateItem();
                //picks a random possible die from the "inventory"
                int randomIndex = selection.Next(Inventory.Count);
                
                KeyValuePair<string, Items> choice = Inventory.ElementAt(randomIndex);
                Items choice2 = Inventory[choice.Key];
                //Use dieroller to get the result
                int result = dieRoller.Roll(choice2.ReturnDamage());

                //store the result
                CPUResult = result;

                Console.WriteLine("The enemy has revealed their weapon. They're using a " + choice.Key+".");
                return result;
            }
            else
            {
                
                //Get player input and display all available choices
                Console.WriteLine("Select a weapon to use: " + string.Join(", ", Inventory.Keys));
                string? playersPick = Console.ReadLine();
                int result = 0;
                //Check validity of player's choice
                if (playersPick != null && Inventory.ContainsKey(playersPick))
                {
                    Items playersPick2 = Inventory[playersPick];
                    result = dieRoller.Roll(playersPick2.ReturnDamage());
                    PlayerResult = result;
                    Console.WriteLine(UserName + " has picked their weapon: " + playersPick);
                    RemoveItem(playersPick);
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
