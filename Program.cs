
﻿using System.ComponentModel;

﻿using System;
using System.Globalization;
using System.Text;

using System.Data;

using System.Runtime.InteropServices;


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
            ConsoleKeyInfo input;
            Console.SetWindowSize(82, 30);
            SkapaBana();
            string temp = "";
            string bana = "";
            int pos = getposition();

            foreach (string c in banan)
            {
                bana = temp + c;
                temp = $"{bana}\n";
            }
            do
            {
                if (Console.KeyAvailable == true)
                {
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.LeftArrow) --pos;
                    else if (input.Key == ConsoleKey.RightArrow) ++pos;
                };
                StringBuilder sb = new StringBuilder(bana);
                sb.Remove(pos, 1);
                sb.Insert(pos, "A");
                Console.WriteLine(sb.ToString());
                Thread.Sleep(33);
                Console.SetCursorPosition(0, 0);
                Console.CursorVisible = false;
            } while (true);
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
                            first = first + emptySpaces(11);
                            j = j + 9;
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
                        banan.Add(movestringtoleft(banan[previous]));
                    }
                    else if (addorsub >= 1)
                    {
                        banan.Add(movestringtoright2(banan[previous]));

                    }
                    else { banan.Add(banan[previous]); }
                }
                previous = i;
            }
        }

        public static int getposition()
        {
            int lastrow = banan.Count;
            int pos = 0;
            string bana = "";
            string temp = "";
            foreach (string c in banan)
            {
                bana = temp + c;
                temp = $"{bana}\n";
            }
            for(int i = 1590;i < 1639; i++)
            {
                if (bana[i] == ' ')
                {
                    pos = i + 5;
                    break;
                }
            }
            return pos;
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
        public static string movestringtoright(string main)
        {
            char[] chars = main.ToCharArray();
            char last = chars[chars.Length - 1];
            char current;
            char next = 'a';
            for (int i = 0; i < main.Length-1;i++)
            {       
                if (i == 0)
                {
                    current = chars[chars.Length-1];
                    next = chars[i];
                    chars[i] = current;
                }
                else if(i > 0)
                {
                    current = chars[i];
                    chars[i] = next;
                    next = current;
                }
            }
            return toString(chars);
        }

        public static string movestringtoright2(string main)
        {
           string tomove = ""; 
            for (int i = 0; i < main.Length; i++)
            {  
                if (i == 0) tomove += main[main.Length - 1];
                else tomove += main[i-1];
            }
            return tomove;
        }


        public static string movestringtoleft(string main)
        {
            string tomove = "";
            char first = main[0];
            for(int i = 0; i < main.Length; i++)
            {
                if (i < main.Length - 1)
                {
                    tomove = tomove + main[i + 1];
                }              
            }
            tomove = tomove + first;
            return tomove;
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