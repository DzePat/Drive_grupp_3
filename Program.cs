

using System.ComponentModel;

using System;
using System.Globalization;
using System.Text;

using System.Data;

using System.Runtime.InteropServices;

using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Diagnostics;

namespace Drive
{
    public class Program
    {

        public static int speed = 250;
        static void Main(string[] args)
        {
            int points = 0;
            int time = 0;
            int pointsMultiplier = 10;
            bool gameIsPlayed = false;

            ConsoleKeyInfo input;

            Menu();

            if (gameIsPlayed)
            {
                Initialize();
                TrackClass.CreateTrack();
                int pos = CountDown();

                var sw = new Stopwatch();
                sw.Start();
                do
                {
                    string temp = "";
                    string bana = "";
                    foreach (string c in TrackClass.banan)
                    {
                        bana = temp + c;
                        temp = $"{bana}\n";
                    }
                    Console.WriteLine(bana.Length);
                    if (Console.KeyAvailable == true)
                    {
                        input = Console.ReadKey();
                        if (input.Key == ConsoleKey.LeftArrow) --pos;
                        else if (input.Key == ConsoleKey.RightArrow) ++pos;
                        else if (input.Key == ConsoleKey.UpArrow) pos -= 83;
                        else if (input.Key == ConsoleKey.DownArrow) if (pos + 83 < 1659) { pos += 83; }
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

                    // Top bar
                    points = CalculatePoints(time, pointsMultiplier);
                    var topBar = GetTopBarString(time, points);
                    Console.WriteLine(topBar);

                    // Race track
                    Console.WriteLine(sb.ToString());

                    // Tick
                    time = sw.Elapsed.Seconds;
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
                        pointsMultiplier = 10;
                        check = false;
                    }
                    else if (key.Key == ConsoleKey.D2 || key.Key == ConsoleKey.NumPad2)
                    {
                        TrackClass.roadwidth = 13;
                        speed = 175;
                        pointsMultiplier = 20;
                        check = false;
                    }
                    else if (key.Key == ConsoleKey.D3 || key.Key == ConsoleKey.NumPad3)
                    {
                        TrackClass.roadwidth = 11;
                        speed = 100;
                        pointsMultiplier = 30;
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
                gameIsPlayed = true;
            }

            int CalculatePoints(int time, int pointsMultiplier)
            {
                int points = time * pointsMultiplier;

                return points;
            }

            string GetTopBarString(int time, int points)
            {
                int minutes = time / 60;
                int seconds = time % 60;
                string minutesString = minutes.ToString("D2");
                string secondsString = seconds.ToString("D2");

                return $"Time {secondsString}:{minutesString} ============= Points: {points}";
            }

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
