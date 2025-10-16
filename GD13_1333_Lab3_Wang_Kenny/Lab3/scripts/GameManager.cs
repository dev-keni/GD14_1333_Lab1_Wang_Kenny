using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
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
        Room[,] mapGrid = new Room[3,3];
        Room? currentRoom;
        int currentX, currentY;

        private void GenerateMap()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int result = dieRoller.Roll("d2");
                    if (result == 1)
                    {
                        mapGrid[i,j] = new EncounterRoom();
                    }
                    else if (result == 2)
                    {
                        mapGrid[i,j] = new TreasureRoom();
                    }
                }
            }
        }

        private void DisplayMap()
        {

        }

        public void Play()
        {
            GenerateMap();
            currentRoom = mapGrid[1,1];
            currentX = 1;
            currentY = 1;
            cpu.isBot = true;
            //Get player's username
            Console.WriteLine("\r\n♫.•°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫\r\n───▄▄▄▄▄▄\r\n─▄▀░░░░░░▀▄░██░██ █████ ██░░ ██░░ █████\r\n▐░▄▄▄░░▐▀▌░▌██▄██ ██▄▄▄ ██░░ ██░░ ██░██\r\n▐░░░░░░░░░░▌██▀██ ██▀▀▀ ██░░ ██░░ ██░██\r\n▐░░▀▄░░▄▀░░▌██░██ █████ ████ ████ █████\r\n─▀▄░░▀▀░░▄▀\r\n───▀▀▀▀▀▀---\r\n♫.•°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫°”˜˜”°•.♫");
            Console.WriteLine(" ");
            Console.WriteLine("Welcome! To Apocalyptic Search! You find yourself in an abandoned home in an apocalypse.");
            Console.WriteLine(" ");
            Console.WriteLine("Please enter your name: ");

            string? userName = Console.ReadLine();
            if (userName != null || userName != "") //Legit check the name
            {
                player.UserName = userName;

                PrintText("green","Welcome, " + player.UserName+"!");

                inGame = true;
                Navigate();
            }
            else
            {
                Console.WriteLine("Invalid input.");
                Play();
            }

        }

        private void Navigate()
        {
            PrintText("blue","What would you like to do, " + player.UserName + "?");
            Console.WriteLine(" ");
            Console.WriteLine("Move: n e s w");
            Console.WriteLine("Search: search");
            Console.WriteLine("Inventory: inv");
            string? answer = Console.ReadLine();
            if (answer != null && answer == "n" || answer == "e" || answer == "s" || answer == "w")
            {
                switch (answer)
                {
                    case "n":
                        CheckRoom("y", 1);
                    break;
                    case "e":
                        CheckRoom("x", 1);
                    break;
                    case "s":
                        CheckRoom("y", -1);
                        break;
                    case "w":
                        CheckRoom("x", -1);
                        break;
                }
            }
            else if (answer != null && answer == "search")
            {
                PrintText("red", "Theres nothing here.");
                //Make the Player object insert new item VIA Room.cs's OnRoomSearch, then match an item from Items.cs lmao
                Navigate();
            }
            else if (answer != null && answer == "inv")
            {
                PrintText("yellow", "You looked in your backpack and you currently have: " + string.Join(", ", player.Inventory));
                Navigate();
            }
        }

        private void CheckRoom(string dir, int increment)
        {
            if (dir == "y")
            {
                if (currentY + increment < 3 && currentY+increment >= 0) //HARDCODED INDEX!!! D:
                {
                    currentY += increment;
                    currentRoom = mapGrid[currentX, currentY];
                    if (mapGrid[currentX, currentY].Enter() == true)
                    {
                        FlipACoin();
                    }
                    Navigate();
                }
                else
                {
                    PrintText("red", "There is no exit in that direction.");
                    Navigate();
                }
            }
            else if(dir == "x")
            {
                if (currentX + increment < 3 && currentX + increment >= 0) //HARDCODED INDEX!!! D:
                {
                    currentX += increment;
                    currentRoom = mapGrid[currentX, currentY];
                    if (mapGrid[currentX, currentY].Enter() == true)
                    {
                        FlipACoin();
                    }
                    Navigate();
                }
                else
                {
                    PrintText("red", "There is no exit in that direction.");
                    Navigate();
                }
            }
        }

        private void FlipACoin() 
        {
            string choice="";
            int plrChoice = 0;

            //Get the player input
            Console.WriteLine("Heads or Tails?");
            string? playersPick = Console.ReadLine();

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
                    Console.WriteLine("The coin flipped " + choice + ". The enemy gets to go first.");
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
            Console.WriteLine(player.UserName + " attacked and landed " + PlayerResult + " damage.");
            Console.WriteLine("The enemy attacks and does " + CPUResult + " damage.");

            //Compare the die to see who wins, if its the same number, then Tie
            if (PlayerResult > CPUResult)
            {
                player.Score += 1;
                Console.WriteLine(player.UserName + " wins! You get 1 score! The score is now "+player.UserName+": "+player.Score+" | CPU: "+ cpu.Score);
            }
            else if (PlayerResult == CPUResult)
            {
                Console.WriteLine("You both fail to attack eachother meaningfully!");
                FlipACoin();
            }
            else
            {
                cpu.Score += 1;
                Console.WriteLine("Bandit wins! They get 1 score! The score is now " + player.UserName + ": " + player.Score + " | CPU: " + cpu.Score);
            }
            //If the score reaches 5 then end the game
            if (player.Score == 5)
            {
                Console.WriteLine(player.UserName + " murdered the bandit!");
                GameOver();
            }
            else if (cpu.Score == 5)
            {
                Console.WriteLine("The bandit kills you and you die.");
                GameOver();
            }

        }

        private void GameOver()
        {
            inGame = false;
            Console.WriteLine("█▀██▀█─▀██▀─▀█▀────█─────▀██▄─▀█▀─▀██▀▄█▀\r\n──██────██▄▄▄█────▄██─────██▀▄─█───███▀\r\n──██────██───█───▄█▄██────██─▀▄█───██▀█\r\n─▄██▄──▄██▄─▄█▄─▄█▄─▄██▄─▄██▄─▀█──▄██▄▀█▄\r\n\r\n───────▀█▄──▄█▀──▄█▀█▄──██───██──────────\r\n────────▀█▄▄█▀──██───██─██───██──────────\r\n──────────██────██───██─██───██──────────\r\n─────────▄██▄────▀█▄█▀───▀█▄█▀───────────");
            Console.WriteLine("for playing. Goodbye!");
        }

        private void PrintText(string color, string text)
        {
            switch (color)
            {
                case "red":
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" ");
                    Console.WriteLine(text);
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    break;
                case "blue":
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine(" ");
                    Console.WriteLine(text);
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    break;
                case "green":
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" ");
                    Console.WriteLine(text);
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    break;
                case "yellow":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" ");
                    Console.WriteLine(text);
                    Console.WriteLine(" ");
                    Console.ResetColor();
                    break;
                // ... more cases
                default:
                    // Code to execute if no case matches
                    break;
            }
        }
    }
}
