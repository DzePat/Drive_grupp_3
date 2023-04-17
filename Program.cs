using System;

namespace Drive
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is a car game.");
            Console.WriteLine("Stay on the road!");
            Console.WriteLine("Use left, right and up arrows to control your car.");
            //Console.WriteLine($"Try to beat the highscore {XX} points"); //NYI
            do
            {
              Console.Write("\nPress enter to start: ");
            } while (Console.ReadKey().Key != ConsoleKey.Enter);


            do
            {
                //NYI - show game start on console
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.LeftArrow) {  }
                else if (key == ConsoleKey.RightArrow) {  }
                else if (key == ConsoleKey.UpArrow) {  }
                else if (key == ConsoleKey.Escape || key == ConsoleKey.Q || key == ConsoleKey.X)
                {Console.WriteLine("\nGame is interrupted"); break; }
                else Console.WriteLine("wrong input"); 
            } while (true);

        }
    }
}