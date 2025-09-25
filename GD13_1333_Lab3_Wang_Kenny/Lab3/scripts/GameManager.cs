using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.scripts
{
    internal class GameManager
    {
        
        DieRoller dieRoller = new DieRoller();

        //Create data for player and CPU
        Player player = new Player();
        Player cpu = new Player();

        //Variables
        bool PlayerFirst;
        int PlayerResult;
        int CPUResult;
        string PlayerDice;
        String CPUDice;

        public void Play()
        {
            //Get player's username
            Console.WriteLine("Enter username: ");
            string userName = Console.ReadLine();
            if (userName != null || userName != "")
            {
                player.UserName = userName;
                Console.WriteLine("Your username is " + player.UserName);
                FlipACoin();
            }
            else
            {
                Console.WriteLine("Invalid input.");
                Play();
            }

            //Console.WriteLine(dieRoller.Roll().ToString());
        }

        private void FlipACoin() 
        {
            string choice="";
            int plrChoice = 0;

            //Get the player input
            Console.WriteLine("Heads or Tails?");
            string playersPick = Console.ReadLine();

            //validate the player's answer
            if (playersPick != null && playersPick == "Heads" || playersPick == "Tails")
            {
                //convert player choice to int so it can be compared to dieRoller.Roll's return (int).
                if (playersPick == "Heads")
                {
                    plrChoice = 1;
                }
                else if (playersPick == "Tails")
                {
                    plrChoice = 2;
                }

                //roll a dice of 2, basically heads/tails. 1 is heads, 2 is tails
                int result = dieRoller.Roll("d2");
                if (result == 1)
                {
                    choice = "Heads";
                }
                else if (result == 2)
                {
                    choice = "Tails";
                }

                //compare the player choice to the generated roll
                if (result == plrChoice)
                {
                    Console.WriteLine("The coin flipped "+choice+"! You get to go first!");
                    PlayerFirst = true;
                    PlayerTurn();
                }
                else
                {
                    Console.WriteLine("The coin flipped " + choice + ". CPU gets to go first.");
                    PlayerFirst = false;
                    CPUTurn();
                }
            }
            else
            {
                //For if the player ever inputted anything other than Heads/Tails
                Console.WriteLine("Error. Try again.");
                FlipACoin();
            }
        }

        private void PlayerTurn()
        {
            //Get player input and display all available choices
            Console.WriteLine("Select a dice to play: "+ string.Join(", ", player.Inventory));
            string playersPick = Console.ReadLine();

            //Check validity of player's choice
            if (playersPick != null && player.Inventory.Contains(playersPick))
            {
                int result = dieRoller.Roll(playersPick);
                PlayerResult = result;
                PlayerDice = playersPick;
                Console.WriteLine(player.UserName + " has picked their dice.");

                //Check turn order
                if (PlayerFirst)
                {
                    CPUTurn();
                }
                else
                {
                    Compare();
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
                PlayerTurn();
            }
        }

        private void CPUTurn()
        {
            Random selection = new Random();

            //picks a random possible die from the "inventory"
            int randomIndex = selection.Next(cpu.Inventory.Length);

            string choice = cpu.Inventory[randomIndex];

            CPUDice = choice;

            //Use dieroller to get the result
            int result = dieRoller.Roll(choice);

            //store the result
            CPUResult = result;

            Console.WriteLine("CPU has picked their dice.");

            //If player went first, compare the results. If not, let the player pick a dice
            if(PlayerFirst)
            {
                Compare();
            }
            else
            {
                PlayerTurn();
            }
        }

        private void Compare()
        {
            Console.WriteLine(player.UserName + " rolled " + PlayerDice + " and got " + PlayerResult + ".");
            Console.WriteLine("CPU rolled " + CPUDice + " and got " + CPUResult + ".");
            //Compare the die to see who wins, if its the same number, then Tie
            if (PlayerResult > CPUResult)
            {
                player.Score += 1;
                Console.WriteLine(player.UserName + " wins! You get 1 score! The score is now "+player.UserName+": "+player.Score+" | CPU: "+ cpu.Score);
            }
            else if (PlayerResult == CPUResult)
            {
                Console.WriteLine("Tied! Play again!");
                FlipACoin();
            }
            else
            {
                cpu.Score += 1;
                Console.WriteLine("CPU wins! They get 1 score! The score is now " + player.UserName + ": " + player.Score + " | CPU: " + cpu.Score);
            }
        }

        //Leftovers from class
        private void OnPointAcquired()
        {
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
