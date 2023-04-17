
﻿using System.ComponentModel;

﻿using System;
using System.Globalization;
using System.Text;
using System.Data;

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
        public static int startpoint = GetRandomNumber(30, 50);
        public static List<string> banan = new List<string>();
        static void Main(string[] args)
        {
            Console.SetWindowSize(82,30);
            SkapaBana();
            int i = 0;
            do
            {
                Thread.Sleep(1);
                Console.Clear();
                Console.WriteLine(printbana().ToString());
            } while (true);
        }

        public static StringBuilder printbana()
        {
            
            string printstring = "";
            for(int i = 0;i < banan.Count;i++)
            {
                if (i == 19)
                {
                    foreach (char ch in banan[i])
                    {
                        if (ch == ' ')
                        {
                            int index = banan[i].IndexOf(ch);
                            index += 4;
                            banan[i] = banan[i].Insert(index, "A");
                            banan[i] = banan[i].Remove(index + 1, 1);
                            printstring = printstring + banan[i];
                            break;
                        }
                    }
                }
                else
                {
                    printstring = printstring + banan[i] + "\n";
                }
            }
            StringBuilder sb = new StringBuilder(printstring);
            return sb;
        }

        public static void SkapaBana()
        {
            int previous = 0;
            int addorsub;
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    string first = "";
                    int j = 0;
                    while(j < 80)
                    {
                        if (startpoint == j)
                        {
                            first = first + emptySpaces(10);
                            j = j + 4;
                        }
                        else
                        {
                            first = first + 0;
                            j++;
                        }
                    }
                    banan.Add(first);
                }
                else if (i != 0)
                {
                    addorsub = rnd.Next(-1, 2);
                    int index = 0;
                    foreach(char c in banan[previous])
                    {
                        if (c == ' ')
                        {
                            break;
                        }
                        else { index++; }
                    }
                    if (addorsub <= -1)
                    {
                        banatillvänster(previous, addorsub, index);

                    }
                    else if (addorsub >= 1)
                    {
                        Banatillhöger(previous, addorsub, i, index);

                    }
                    else { banan.Add(banan[previous]); }
                }
                previous = i;
            }
        }

        private static string emptySpaces(int x)
        {
            string empty = "";
            for(int i = 0;i < x; i++)
            {
                empty = empty + " ";
            }
            return empty;
        }

        private static void Banatillhöger(int previous, int addorsub, int i, int index)
        {
            string current = banan[i - 1];
            char[] chars = current.ToCharArray();
            int randomnr = GetRandomNumber(1, 2);
            int low = 0;
            int high = 9;
            string main = "";
            for (int c = randomnr; c > 0; c--)
            {
                if (index + 5 <= 80)
                {
                    chars[index + low] = '0';
                    chars[index + high] = ' ';
                    string result2 = toString(chars);
                    main = result2;
                    low++;
                    high++;
                }

            }
            if (main != "")
            {
                banan.Add(main);
            }
            else { banan.Add(banan[previous]); }
        }

        private static void banatillvänster(int previous, int addorsub, int index)
        {
            string current = banan[previous];
            char[] chars = current.ToCharArray();
            int randomnr = GetRandomNumber(-1, 0);
            int low = -1;
            int high = 9;
            string main = "";
            for (int d = randomnr; d < 0; d++)
            {
                if (index - addorsub >= 0 && index + 9 <= 80)
                {
                    chars[index + low] = ' ';
                    chars[index + high] = '0';
                    string result1 = toString(chars);
                    main = result1;
                    low = low -1;
                    high = high - 1;
                }
            }
            if (main != "")
            {
                banan.Add(main);
            }
            else { banan.Add(banan[previous]); }
        }
       /* public static void movestringoleft(string main)
        {
            char first = main[0];
            for(int i = 0; i < main.Length; i++)
            {
                if (i < main.Length - 2)
                {
                    main = main[i + 1];
                }
                
            }
            banan[banan.Count - 1] = first;
        }*/
        public static string toString(char[] chars)
        {
            string thestring = "";
            foreach(char c in chars)
            {
                thestring = thestring + c;
            }
            return thestring;
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