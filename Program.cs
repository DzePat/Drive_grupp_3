
﻿using System.ComponentModel;

﻿using System;
using System.Globalization;
using System.Text;

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
            Console.SetWindowSize(82,30);
            SkapaBana();
            int i = 0;
            foreach (string c in banan)
            {
                if (i == 19){
                    foreach (char ch in c)
                    {
                        if (ch == ' ')
                        {
                            int index= c.IndexOf(ch);
                            index += 2;
                            StringBuilder sb = new StringBuilder(c);
                            sb.Insert(index, "<>");
                            Console.WriteLine(sb);
                            break;
                        } ;
                    }
                }
                else Console.WriteLine(c);
                i++;
            }
        }

        public static void SkapaBana()
        {
            int startpoint = GetRandomNumber(30,50);
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
                            first = first + "     ";
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

        private static void Banatillhöger(int previous, int addorsub, int i, int index)
        {
            string current = banan[i - 1];
            char[] chars = current.ToCharArray();
            int low = 0;
            int high = 5;
            string main = "";
            for (int c = addorsub; c > 0; c--)
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
            int low = addorsub;
            int high = 4;
            string main = "";
            for (int d = addorsub; d < 0; d++)
            {
                if (index - addorsub >= 0 && index + 4 <= 80)
                {
                    chars[index + low] = ' ';
                    chars[index + high] = '0';
                    string result1 = toString(chars);
                    main = result1;
                    low = low + 1;
                    high = high - 1;
                }
            }
            if (main != "")
            {
                banan.Add(main);
            }
            else { banan.Add(banan[previous]); }
        }

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