
﻿using System;

﻿using Drive;


using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive
{
    internal class TrackClass
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
        public static int prevRandom = 0;
        public static List<string> banan = new List<string>();
        public static int roadwidth = 20;

        public static void MoveForward()
        {
            string firstele = banan[0];
            int min = -4;
            int max = 5;
            if (firstele[0] == ' ') max += 1;
            if (firstele[firstele.Length - 1] == ' ') min -= 1;
            if (prevRandom < 0) max = 2; //vägen svängde till vänster förra gågngen 
            if (prevRandom > 0) min = -1;
            if (firstele.First() == ' ') min = 1;
            if (firstele.Last() == ' ') max = -1;
            int randomnumber = GetRandomNumber(min, max);
            if (randomnumber >= 1 && firstele[firstele.Length - 1] != ' ')
            {
                banan.Insert(0, MoveStringToRight(firstele));
            }
            else if (randomnumber <= -1 && firstele[0] != ' ')
            {
                banan.Insert(0, MoveStringToLeft(firstele));
            }
            else
            {
                banan.Insert(0, firstele);
            }
            banan.RemoveAt(banan.Count- 1);
            prevRandom = randomnumber;
        }

        public static string MoveStringToRight(string main)
        {
            string tomove = "";
            for (int i = 0; i < main.Length; i++)
            {
                if (i == 0) tomove += main[main.Length - 1];
                else tomove += main[i - 1];
            }
            return tomove;
        }

        public static string MoveStringToLeft(string main)
        {
            string tomove = "";
            char first = main[0];
            for (int i = 0; i < main.Length; i++)
            {
                if (i < main.Length - 1)
                {
                    tomove = tomove + main[i + 1];
                }
            }
            tomove = tomove + first;
            return tomove;
        }
        public static void CreateTrack()
        {

            int previous = 0;
            int RandomNumber;
            Random rnd = new Random();
            for (int i = 0; i < 20; i++)
            {
                if (i == 0)
                {
                    string first = "";
                    int j = 0;
                    while (j < 80)
                    {
                        if (startpoint == j)
                        {
                            first = first + EmptySpaces(roadwidth);
                            j = j + roadwidth-2;
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

                    int min = -4;
                    int max = 5;
                    if (prevRandom < 0) max = 2; //vägen svängde till vänster förra gågngen 
                    if (prevRandom > 0) min = -1;

                    RandomNumber = rnd.Next(min, max);
                    int index = 0;
                    foreach (char c in banan[previous])
                    {
                        if (c == ' ')
                        {
                            break;
                        }
                        else { index++; }
                    }
                    if (RandomNumber <= -1)
                    {
                        banan.Add(MoveStringToLeft(banan[previous]));
                    }
                    else if (RandomNumber >= 1)
                    {
                        banan.Add(MoveStringToRight(banan[previous]));

                    }
                    else { banan.Add(banan[previous]); }
                    prevRandom = RandomNumber;
                }
                previous = i;
            }
        }
        public static int GetPosition()

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
            for (int i = 1590; i < 1639; i++)
            {
                if (bana[i] == ' ')
                {
                  
                    pos = i + (roadwidth / 2);

                    break;
                }
            }
            return pos;
        }

        public static string EmptySpaces(int x)
        {
            string empty = "";
            for (int i = 0; i < x; i++)
            {
                empty = empty + " ";
            }
            return empty;
        }
    }
}
