using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
        bool inGame = false;
        int PlayerResult, CPUResult;
        List<Player> TurnOrder = new List<Player>(2);

        public void Play()
        {
            cpu.isBot = true;
            //Get player's username
            Console.WriteLine("\r\n♫.•°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫\r\n───▄▄▄▄▄▄\r\n─▄▀░░░░░░▀▄░██░██ █████ ██░░ ██░░ █████\r\n▐░▄▄▄░░▐▀▌░▌██▄██ ██▄▄▄ ██░░ ██░░ ██░██\r\n▐░░░░░░░░░░▌██▀██ ██▀▀▀ ██░░ ██░░ ██░██\r\n▐░░▀▄░░▄▀░░▌██░██ █████ ████ ████ █████\r\n─▀▄░░▀▀░░▄▀\r\n───▀▀▀▀▀▀---\r\n♫.•°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫");
            Console.WriteLine("Welcome! To Dice Off! You will be facing off against my evil dice rolling computer program. First to 5 wins!");
            Console.WriteLine("Please enter your username: ");
            string? userName = Console.ReadLine();
            if (userName != null || userName != "")
            {
                player.UserName = userName;
                Console.WriteLine("Your username is " + player.UserName);
                inGame = true;
                FlipACoin();
            }
            else
            {
                Console.WriteLine("Invalid input.");
                Play();
            }

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
                    TurnOrder.Insert(0, player);
                    TurnOrder.Insert(1, cpu);
                    inGame = true;
                    GameLoop();
                }
                else
                {
                    Console.WriteLine("The coin flipped " + choice + ". CPU gets to go first.");
                    TurnOrder.Insert(0, cpu);
                    TurnOrder.Insert(1, player);
                    inGame = true;
                    GameLoop();
                }
            }
            else
            {
            //For if the player ever inputted anything other than Heads/Tails
                Console.WriteLine("Error. Try again.");
                FlipACoin();
            }
        }

        //Loop the game
        private void GameLoop()
        {
            while (inGame)
            {
                CheckTurn();
                Compare();
            }
        }

        //Check the turn order
        private void CheckTurn()
        {
            if (TurnOrder[0]==player)
            {
                PlayerResult = player.Turn();
                CPUResult = cpu.Turn();
            }
            else
            {
                CPUResult = cpu.Turn();
                PlayerResult = player.Turn();
            }
        }

        private void Compare()
        {
            Console.WriteLine(player.UserName + " rolled and got " + PlayerResult + ".");
            Console.WriteLine("CPU rolled and got " + CPUResult + ".");

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
            //If the score reaches 5 then end the game
            if (player.Score == 5)
            {
                Console.WriteLine(player.UserName + " has won!");
                GameOver();
            }
            else if (cpu.Score == 5)
            {
                Console.WriteLine("My evil CPU has won! You lose! Hahahaha!");
                GameOver();
            }

        }

        private void GameOver()
        {
            inGame = false;
            Console.WriteLine("█▀██▀█─▀██▀─▀█▀────█─────▀██▄─▀█▀─▀██▀▄█▀\r\n──██────██▄▄▄█────▄██─────██▀▄─█───███▀\r\n──██────██───█───▄█▄██────██─▀▄█───██▀█\r\n─▄██▄──▄██▄─▄█▄─▄█▄─▄██▄─▄██▄─▀█──▄██▄▀█▄\r\n\r\n───────▀█▄──▄█▀──▄█▀█▄──██───██──────────\r\n────────▀█▄▄█▀──██───██─██───██──────────\r\n──────────██────██───██─██───██──────────\r\n─────────▄██▄────▀█▄█▀───▀█▄█▀───────────");
            Console.WriteLine("for playing. Goodbye!");
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
