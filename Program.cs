
using System.ComponentModel;

using System;
using System.Globalization;
using System.Text;

using System.Data;

using System.Runtime.InteropServices;

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace Drive
{
    public class Program
    {

        /*private static Random random = new Random();
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
        public static int roadwidth = 20;*/
        public static int speed = 250;
        static void Main(string[] args)
        {
            
            int points = 0;
            bool gameIsPlayed = true;

            ConsoleKeyInfo input;
          
            Menu();
            
            if (gameIsPlayed)
            {
                Initialize();
                TrackClass.CreateTrack();
                int pos = CountDown();
                do
                {
                    string temp = "";
                    string bana = "";
                    foreach (string c in TrackClass.banan)
                    {
                        bana = temp + c;
                        temp = $"{bana}\n";
                    }
                    if (Console.KeyAvailable == true)
                    {
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.LeftArrow) --pos;
                        else if (input.Key == ConsoleKey.RightArrow) ++pos;
                        else if (input.Key == ConsoleKey.UpArrow) pos -= 83;
                        else if (input.Key == ConsoleKey.DownArrow) if (pos+83 < 1659) { pos += 83; }
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
                        Thread.Sleep(100);
                        GameOver();
                    }
                    Console.WriteLine(sb.ToString());
                    Thread.Sleep(speed);
                    Console.SetCursorPosition(0, 0);
                    Console.CursorVisible = false;
                    TrackClass.MoveForward();
                } while (true);
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

            /*static void CreateTrack()
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
                            banan.Add(MoveStringToLeft(banan[previous]));
                        }
                        else if (addorsub >= 1)
                        {
                            banan.Add(MoveStringToRight(banan[previous]));

                        }
                        else { banan.Add(banan[previous]); }
                        prevaddorsub = addorsub;
                    }
                    previous = i;
                }
            }

            static int GetPosition()
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
                        pos = i + (roadwidth/2);
                        break;
                    }
                }
                return pos;
            }

            static string EmptySpaces(int x)
            {
                string empty = "";
                for (int i = 0; i < x; i++)
                {
                    empty = empty + " ";
                }
                return empty;
            }*/

            /*static void MoveForward()
            {
                string firstele = banan[0];
                int min = -4;
                int max = 5;
                if (firstele[0] == ' ' ) max += 1;
                if (firstele[firstele.Length - 1] == ' ') min -= 1;
                if (prevaddorsub < 0) max = 2; //vägen svängde till vänster förra gågngen 
                if (prevaddorsub > 0) min = -1;
                if(firstele.First() == ' ')min = 1;
                if (firstele.Last() == ' ') max = -1;
                int randomnumber = GetRandomNumber(min,max);
                if(randomnumber >= 1 && firstele[firstele.Length - 1] != ' ')
                {
                    banan.Insert(0, MoveStringToRight(firstele));
                }
                else if(randomnumber <= -1 && firstele[0] != ' ') 
                {
                    banan.Insert(0,MoveStringToLeft(firstele));
                }
                else
                {
                    banan.Insert(0, firstele);
                }
                banan.RemoveAt(banan.Count- 1);
                prevaddorsub = randomnumber;
            }

            static string MoveStringToRight(string main)
            {
                string tomove = "";
                for (int i = 0; i < main.Length; i++)
                {
                    if (i == 0) tomove += main[main.Length - 1];
                    else tomove += main[i - 1];
                }
                return tomove;
            }

            static string MoveStringToLeft(string main)
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
            }*/

            static string ToString(char[] chars)
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
                Console.WriteLine("Choose dificulty:\n1: Easy\n2: Normal\n3: Hard");
                ConsoleKeyInfo key = Console.ReadKey();
                bool check = true;
                while (check)
                {
                    if (key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.NumPad1)
                    {
                        TrackClass.roadwidth = 15;
                        speed = 250;
                        check = false;
                    }
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        TrackClass.roadwidth = 13;
                        speed = 175;
                        check = false;
                    }
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    {
                        TrackClass.roadwidth = 11;
                        speed = 100;
                        check = false;
                    }
                    else 
                    { 
                        Console.Clear();
                        Console.WriteLine("Choose dificulty:\n1: Easy\n2: Normal\n3: Hard");
                        key = Console.ReadKey();
                    }
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

            static int CountDown()
            {
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"{i}...");
                    Thread.Sleep(500);
                }
                Console.Write("Start!!!");
                Thread.Sleep(500);
                Console.Clear();
                int pos = TrackClass.GetPosition();
                return pos;
            }
        }
    }
}
