using Lab3.scripts;
using System.Net.NetworkInformation;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome, Kenny! September 17th, 2025");

            GameManager manager = new GameManager();
            manager.Play();


            //Console.WriteLine("Goodbye!");

            //Welcome the user
            //Ask them for their username
            //Let each player pick which die to use
            //Remove the chosen dice from the players inventory
            //Calculate each players dice rolls (pick random number for each player according to dice), whichever player rolls the higher number gets 1 score
            //Redo above 4 times
            //If players are 2:2/tied, have them roll die again in sudden death
        }
    }
}
