using Lab3.scripts;
using System.Net.NetworkInformation;

namespace Lab3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome, Kenny! September 14th, 2025");

            GameManager manager = new GameManager();
            manager.Play();

            Console.WriteLine("+ adds");
            Console.WriteLine("- subtracts");
            Console.WriteLine("/ divides");
            Console.WriteLine("* multiplies");
            Console.WriteLine("++ adds 1");
            Console.WriteLine("-- subtracts 1");
            Console.WriteLine("% gets the remainder of two variables");

            Console.WriteLine("Goodbye!");

            //Let each player pick which die to use
            //Remove the chosen dice from the players inventory
            //Calculate each players dice rolls (pick random number for each player according to dice), whichever player rolls the higher number gets 1 score
            //Redo above 4 times
            //If players are 2:2/tied, have them roll die again in sudden death
        }
    }
}
