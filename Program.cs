

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
        public static int playerpos = 0;
        public static int playerLife = 3;
        public static string defaultPointFile = "..\\..\\..\\points.txt";
        public static string otherOSPoiintFile = "../../../points.txt";
        public static int points = 0;
        public static int time = 0;
        public static int pointsMultiplier = 10;
        public static bool gameIsPlayed = true;
        public static int damageTaken = 1;
        public static int timecheck = 0;
        static void Main(string[] args)
        {
            ConsoleKeyInfo input;
            Menu();
            Initialize();
            TrackClass.CreateTrack();
            CountDown();
            int life = SetLife(0);
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
                if (Console.KeyAvailable == true)
                {
                    input = Console.ReadKey();
                    if (input.Key == ConsoleKey.LeftArrow) --playerpos;
                    else if (input.Key == ConsoleKey.RightArrow) ++playerpos;
                    else if (input.Key == ConsoleKey.UpArrow) { if (playerpos + 83 > 200) { playerpos -= 83; } }
                    else if (input.Key == ConsoleKey.DownArrow) if (playerpos + 83 < 1659) { playerpos += 83; }
                };
                StringBuilder sb = new StringBuilder(bana);
                if (bana[playerpos] == ' ')
                {
                    sb.Remove(playerpos, 1);
                    sb.Insert(playerpos, "A");
                }
                else
                {
                    sb.Remove(playerpos, 1);
                    sb.Insert(playerpos, "X");
                    Thread.Sleep(100);
                    life = SetLife(1);
                    if (timecheck == 1)
                    {
                        sw.Restart();
                        time = 0;
                        points = 0;
                        timecheck = 0;
                    }
                }

                // Top bar
                points = CalculatePoints(time, pointsMultiplier);
                var topBar = GetTopBarString(time, points, life);
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
        //takes damage as argument and returns the amount of lifes left 
        //if player has 0 lifes initiates gameover method
        static int SetLife(int damageTaken)
        {
            if (playerLife != 0 && gameIsPlayed == true)
            {
                playerLife = playerLife - damageTaken;
                playerpos = TrackClass.GetPosition();
            }
            else
            {
                GameOver();
            }
            return playerLife;
        }

        //initializes console window width/height 
        static void Initialize()
        {
            int width = 82;
            int height = 30;

            if (OperatingSystem.IsWindows())
            {
                Console.SetWindowSize(width, height);
            }
            if (!OperatingSystem.IsWindows())
            {
                defaultPointFile = otherOSPoiintFile;
            }
        }
        //prints out the menu and takes player input to select dificulty
        static void Menu()
        {
            Console.WriteLine("This is a car game.");
            Console.WriteLine("Stay on the road!");
            Console.WriteLine("Use left and right arrows to control your car.");
            //NYI It�s possible to add new features/functions later.
            Console.WriteLine("Press enter to start: ");
            while (Console.ReadKey().Key != ConsoleKey.Enter)
            {
                Console.WriteLine("\nPlease press the Enter Key");
            }
            Console.Clear();
            //Console.WriteLine("Choose difficulty:\n1: Easy\n2: Normal\n3: Hard");
            //onsole.ReadKey();
            ConsoleKeyInfo key = GetUserInput("Choose difficulty:\n1: Easy\n2: Normal\n3: Hard");
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
                    key = GetUserInput("Choose difficulty:\n1: Easy\n2: Normal\n3: Hard \nPlease Enter 1, 2 or 3");
                }
            }
            Console.Clear();
            gameIsPlayed = true;
            time = 0;
            points = 0;
        }
        //caculates the amount of points earned each second multplied by the pointsmultiplier
        static int CalculatePoints(int time, int pointsMultiplier)
        {
            int points = time * pointsMultiplier;

            return points;
        }
        //prints out the tob bar which contains timer elapsed, points scored and lives
        static string GetTopBarString(int time, int points, int playerLife)
        {
            int newLife = 3;
            int minutes = time / 60;
            int seconds = time % 60;
            string minutesString = minutes.ToString("D2");
            string secondsString = seconds.ToString("D2");

            return $"Time {secondsString}:{minutesString} ============= Points: {points} ============ Life:  {newLife} / {playerLife}";
        }
        //prints out the current highscores
        static void HighScore()
        {
            string[] playerPoints = File.ReadAllLines(defaultPointFile);
            int player = 0;
            Console.WriteLine("Highscore list\nPoints      Player namne");
            while (player < playerPoints.Length)
            {
                string[] printArray = playerPoints[player].Split("|");
                Console.WriteLine($"{printArray[0],6}         {printArray[1],9}");
                player++;
            }
        }
        //adds player score to the highscores
        static void AddHighScore(int scoreplayer)
        {
            string[] playerPoints = File.ReadAllLines(defaultPointFile);

            int i = 0;
            int arrayLength = playerPoints.Length;

            while (i < arrayLength)
            {
                string[] printArray = playerPoints[i].Split("|");
                if (scoreplayer > Int32.Parse(printArray[0]))
                {
                    Console.WriteLine($"Congratulations, you are {i + 1} on the highscore list");
                    Console.Write("Your name?: ");
                    string newPlayerName = Console.ReadLine();
                    string newPlayerPoints = $"{scoreplayer.ToString()}|{newPlayerName}";
                    for (int j = arrayLength - 1; j > i; j--)
                    {
                        playerPoints[j] = playerPoints[j - 1];
                    }
                    playerPoints[i] = newPlayerPoints;

                    break;
                }
                i++;
            }

            using (StreamWriter file = new StreamWriter(defaultPointFile))
            {
                foreach (string line in playerPoints)
                {
                    file.WriteLine(line);
                }
            }
        }



            //prints out the game over message if the player hit the wall and has 0 lives left.
            //Adds player points to highscores and prints out the current highscores
            //Asks player if you want to try again? if yes player resets on a new track with 0 time elapsed.
            static void GameOver()

            {
                Console.Clear();
                if (playerLife == 0)
                {
                    Console.WriteLine("GAME OVER!");
                    Thread.Sleep(TimeSpan.FromMilliseconds(1000));
                    Console.WriteLine($"Your score: {points}");
                    Console.CursorVisible = true;
                    AddHighScore(points);
                    HighScore();
                    Thread.Sleep(5000);
                    Console.Clear();
                    TrackClass.banan.Clear();
                    TrackClass.CreateTrack();
                }
                while (true)
                {
                    Console.WriteLine("Play Again? \n 'Y' / 'N'");
                    char answer = Console.ReadKey().KeyChar;
                    if (answer == 'Y' || answer == 'y')
                    {
                        Console.Clear();
                        Menu();
                        reset();
                        timecheck = 1;
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
            //starts to the coundown from 3 to 1
            static void CountDown()
            {
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"{i}...");
                    Thread.Sleep(500);
                }
                Console.Write("Start!!!");
                Thread.Sleep(500);
                Console.Clear();
                playerpos = TrackClass.GetPosition();
            }
            //resets track, player life and starts the countdown again
            static void reset()
            {
                TrackClass.banan.Clear();
                TrackClass.CreateTrack();
                playerLife = 3;
                CountDown();
            }
            static ConsoleKeyInfo GetUserInput(string prompt)
            {
                Console.Write(prompt);
                return Console.ReadKey();
            }
        }

    }

