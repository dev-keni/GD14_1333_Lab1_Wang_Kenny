namespace GD14_1333_Lab1_Wang_Kenny
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World! Kenny Wang September 3rd, 2025");
            DisplayIt(0);
            DisplayIt(1);
            DisplayIt(2);
            DisplayIt(4);
            DisplayIt(8);
            DisplayIt(16);
            DisplayIt(32);
            DisplayIt(64);
            DisplayIt(100);
            DisplayIt(255);
            Console.WriteLine("Goodbye, World! Kenny Wang September 3rd, 2025");
        }
        private static void DisplayIt(int deci)
        {
            string hex = deci.ToString("X");
            string binary = Convert.ToString(deci, 2);
            Console.WriteLine($"Decimal: {deci}, Binary: {binary}, Hex: {hex}");
        }
    }
}
