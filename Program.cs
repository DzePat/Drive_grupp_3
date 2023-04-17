
﻿using System.ComponentModel;

﻿using System;


namespace Drive
{
    public class Program
    {
        private static Random random = new Random();
        public static int GetRandomNumber(int min, int max)
        {
            lock (random)
            {
                return random.Next(min, max);
            }
        }
        public static List<string> banan = new List<string>();
        static void Main(string[] args)
        {
            SkapaBana();
            foreach (string c in banan)
            {
                Console.WriteLine(c);
            }
        }

        public static void SkapaBana()
        {
            int startpoint = GetRandomNumber(20, 60);
            int previous = 0;
            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    string first = "";
                    for (int j = 0; j < 80; j++)
                    {
                        if (startpoint == j)
                        {
                            first = first + "     ";
                            j = j + 4;
                        }
                        else
                        {
                            first = first + 0;
                        }
                    }
                    banan.Add(first);
                    Console.WriteLine(banan[0]);
                }
                else if (i != 0)
                {
                    int addorsub;
                    addorsub = GetRandomNumber(-1, 1);
                    int index = 0;
                    foreach (char c in banan[previous])
                    {
                        if (c == ' ')
                        {
                            break;
                        }
                        else { index++; }
                    }
                    if (addorsub == -1)
                    {
                        string current = banan[previous];
                        char[] chars = current.ToCharArray();
                        if (index - 1 >= 0 && index + 4 <= 20)
                        {
                            chars[index - 1] = ' ';
                            chars[index + 4] = '0';
                            string result = "";
                            foreach (char c in chars)
                            {
                                result += c;
                            }
                            banan.Add(result);
                        }
                        else { banan.Add(banan[previous]); }

                    }
                    else if (addorsub == 1)
                    {
                        string current = banan[i - 1];
                        char[] chars = current.ToCharArray();
                        if (index + 5 <= 20) {
                            chars[index] = '0';
                            chars[index + 5] = ' ';
                            string result = "";
                            foreach (char c in chars)
                            {
                                result += c;
                            }
                            banan.Add(result);
                        }
                        else { banan.Add(banan[previous]); }

                    }
                    else { banan.Add(banan[previous]); }
                    previous = i;
                }
            }
        }
        public static void menu()
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
                if (key == ConsoleKey.LeftArrow) { }
                else if (key == ConsoleKey.RightArrow) { }
                else if (key == ConsoleKey.UpArrow) { }
                else if (key == ConsoleKey.Escape || key == ConsoleKey.Q || key == ConsoleKey.X)
                { Console.WriteLine("\nGame is interrupted"); break; }
                else Console.WriteLine("wrong input");
            } while (true);



        }
    }
}