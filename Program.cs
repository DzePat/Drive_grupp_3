
using System.ComponentModel;

using System;
using System.Globalization;
using System.Text;

using System.Data;

using System.Runtime.InteropServices;

using System.ComponentModel.DataAnnotations;

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
        public static int prevaddorsub = 0;
        public static List<string> banan = new List<string>();
        static void Main(string[] args)
        {
            
            int points = 0;
            bool gameIsPlayed = true;

            ConsoleKeyInfo input;
            
            Menu();

            if (gameIsPlayed)
            {
                Initialize();
                SkapaBana();
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"{i}...");
                    Thread.Sleep(500);
                }
                Console.Write("Start!!!");
                Thread.Sleep(500);
                Console.Clear();
                int pos = getposition();
                do
                {
                    string temp = "";
                    string bana = "";
                    foreach (string c in banan)
                    {
                        bana = temp + c;
                        temp = $"{bana}\n";
                    }
                    if (Console.KeyAvailable == true)
                    {
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.LeftArrow) --pos;
                        else if (input.Key == ConsoleKey.RightArrow) ++pos;
                    };
                    StringBuilder sb = new StringBuilder(bana);
                    if (bana[pos] == ' ')
                    {
                        sb.Remove(pos, 1);
                        sb.Insert(pos, "A");
                    }
                    else
                    {
                        sb.Remove(pos, 1);
                        sb.Insert(pos, "X");
                        Thread.Sleep(300);
                        GameOver();
                    }
                    Console.WriteLine(sb.ToString());
                    Thread.Sleep(300);
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                    moveforward();
                } while (gameIsPlayed);
            }


            static void Initialize()
            {
                int width = 82;
                int height = 30;

                if (OperatingSystem.IsWindows())
                {
                    Console.SetWindowSize(width, height);
                }
            }

            static void SkapaBana()
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
                        while (j < 80)
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

                        int min = -4;
                        int max = 5;
                        if (prevaddorsub < 0) max = 2; //vägen svängde till vänster förra gågngen 
                        if (prevaddorsub > 0) min = -1;

                        addorsub = rnd.Next(min, max);
                        int index = 0;
                        foreach (char c in banan[previous])
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
                        prevaddorsub = addorsub;
                    }
                    previous = i;
                }
            }
            /* void CreateNewRow()
             {
            //TBD: bryta ut en "skapa ny rad" function utifrån Skapa Bana
             }
            */

            static int getposition()
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
                        pos = i + 5;
                        break;
                    }
                }
                return pos;
            }

            static string emptySpaces(int x)
            {
                string empty = "";
                for (int i = 0; i < x; i++)
                {
                    empty = empty + " ";
                }
                return empty;
            }

            static void moveforward()
            {
                string firstele = banan[0];
                int min = -4;
                int max = 5;
                if (prevaddorsub < 0) max = 2; //vägen svängde till vänster förra gågngen 
                if (prevaddorsub > 0) min = -1;
                int randomnumber = GetRandomNumber(min,max);
                if(randomnumber >= 1)
                {
                    banan.Insert(0, movestringtoright2(firstele));
                }
                else if(randomnumber <= -1) 
                {
                    banan.Insert(0,movestringtoleft(firstele));
                }
                else
                {
                    banan.Insert(0, firstele);
                }
                banan.RemoveAt(banan.Count- 1);
                prevaddorsub = randomnumber;
            }

            static string movestringtoright(string main)
            {
                char[] chars = main.ToCharArray();
                char last = chars[chars.Length - 1];
                char current;
                char next = 'a';
                for (int i = 0; i < main.Length - 1; i++)
                {
                    if (i == 0)
                    {
                        current = chars[chars.Length - 1];
                        next = chars[i];
                        chars[i] = current;
                    }
                    else if (i > 0)
                    {
                        current = chars[i];
                        chars[i] = next;
                        next = current;
                    }
                }
                return toString(chars);
            }


            static string movestringtoright2(string main)
            {
                string tomove = "";
                for (int i = 0; i < main.Length; i++)
                {
                    if (i == 0) tomove += main[main.Length - 1];
                    else tomove += main[i - 1];
                }
                return tomove;
            }

            static string movestringtoleft(string main)
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

            static string toString(char[] chars)
            {
                string thestring = "";
                foreach (char c in chars)
                {
                    thestring = thestring + c;
                }
                return thestring;
            }

            void Menu()
            {
                Console.WriteLine("This is a car game.");
                Console.WriteLine("Stay on the road!");
                Console.WriteLine("Use left and right arrows to control your car.");
                //Console.WriteLine($"Try to beat the highscore {XX} points"); //NYI
                Console.WriteLine("Press enter to start: ");
                while (Console.ReadKey().Key != ConsoleKey.Enter) 
                {
                     //   
                }
                Console.Clear();
            }


            void TimerPoints()
            {
                int seconds = 0;
                int minutes = 0;

                int pointsMultiplier = 10;

                while (gameIsPlayed)
                {
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    seconds++;
                    points = (seconds + (minutes * 60)) * pointsMultiplier;

                    if (seconds == 60)
                    {
                        seconds = 0;
                        minutes++;
                    }
                    Console.Write(seconds.ToString("D2") + ":" + minutes.ToString("D2").PadRight(10) + "Points: " + points);
                }
            }

            //TBD Update() function för att växa fram banen, Vänta på CreateNewRow() funtion
            /* void Update()
             {
                 int lastRow = banan.Count() - 1;
                 banan.RemoveAt(lastRow);

                 for (int i = lastRow - 1; i > 0; i--)
                 {

                     var item = banan[i];
                     banan.RemoveAt(i);
                     banan.Insert(i + 1, item);
                 }
                 var newItem = CreateNewRow(previous);
                 banan.Insert(0, newItem);
             }
            */

            void HighScore()
            {
                string[] playerpoints = File.ReadAllLines("points.txt");
                int player = 0;
                Console.WriteLine("Highscore list\nPoints      Player namne");
                while (player < playerpoints.Length)
                {
                    string[] printArray = playerpoints[player].Split("|");
                    Console.WriteLine($"{printArray[0]}         {printArray[1]}");
                    player++;
                }
            }

            void AddHighScore(int scoreplayer) //Can't add to list, only uppdate
            {
                string[] playerpoints = File.ReadAllLines("points.txt");
                int player = 0;
                while (player < playerpoints.Length)
                {
                    string[] printArray = playerpoints[player].Split("|");
                    if (scoreplayer > Int32.Parse(printArray[0]))
                    {
                        Console.WriteLine($"Congratulations, you are {player + 1} on the highscore list");
                        Console.Write("Your name?: ");
                        string newPlayerPoints = $"{scoreplayer.ToString()}|{Console.ReadLine()}";
                        playerpoints[player] = newPlayerPoints;
                        break;
                    }
                    player++;
                }
                using (StreamWriter file = new StreamWriter("points.txt"))
                {
                    foreach (string line in playerpoints)
                    {
                        file.WriteLine(line);
                    }
                }
            }


            void GameOver()
            {
                gameIsPlayed = false;
                Console.Clear();
                Console.WriteLine("GAME OVER!");
                Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                Console.WriteLine($"Your score: {points}");
                while (true)
                {
                    Console.WriteLine("Play Again?");
                    Console.WriteLine("'Y' / 'N'");
                    char answer = Console.ReadKey().KeyChar;
                    if (answer == 'Y' || answer == 'y')
                    {
                        Console.Clear();
                        Menu();
                        return;
                    }
                    else if (answer == 'N' || answer == 'n')
                    {
                        Console.Clear();
                        Console.WriteLine("Thanks for playing");
                        Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                        Environment.Exit(0);
                        return;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid answer, please answer 'Y' for yes or 'N' for no");
                    }
                }

            }


        }
    }
}
