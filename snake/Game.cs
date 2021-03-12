using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Timers;

namespace snake
{
    class Game
    {
        //int score;
        Timer wormTimer = new Timer(100);
        Timer gameTimer = new Timer(1000);
        
        
        public static int Width { get { return 40; } }
        public static int Height { get { return 40; } }

        static string path = @"C:\Users\пк\source\repos\snake\Levels\Level1.txt";

        Worm w = new Worm('@', ConsoleColor.Green);
        Food f = new Food('$', ConsoleColor.Yellow);
        Wall wall = new Wall('#', ConsoleColor.DarkCyan, path);
        Score score = new Score(0);


        public bool IsRunning { get; set; }
        bool pause = false;

        public Game()
        {
            gameTimer.Elapsed += GameTimer_Elapsed;
            gameTimer.Start();
            wormTimer.Elapsed += Move2;
            wormTimer.Start();
            



            pause = false;
            IsRunning = true;
            Console.CursorVisible = false;
            Console.SetWindowSize(Width, Width);
            Console.SetBufferSize(Width, Width);
            //w.Draw();
            
            f.Draw();
            wall.Draw();

        }

        private void GameTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.Title = DateTime.Now.ToLongTimeString();
        }

        bool CheckWormEatFood()
        {
            return w.body[0].X == f.body[0].X && w.body[0].Y == f.body[0].Y;


        }

        public void getScores()
        {
            Console.SetCursorPosition(15, 0);
            Console.Write("Score: " + score.getScore());

        }
        bool CheckCollisionFoodWithWorm()
        {
            for (int i = 1; i < w.body.Count; i++)
            {
                if (w.body[i].X == f.body[0].X && w.body[i].Y == f.body[0].Y)
                {
                    return true;
                }
            }

            return false;
        }
        bool CheckCollisionFoodWithWall()
        {
            for (int i = 0; i < wall.body.Count; i++)
            {
                //for(int j = 0; j < wall.body.Count; j++){
                if (wall.body[i].X == f.body[0].X && wall.body[i].Y == f.body[0].Y)
                {
                    Console.SetCursorPosition(wall.body[i].X, wall.body[i].Y);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write('#');
                    return true;
                }
                //}
                //if (wall.a[f.body[0].X, f.body[0].Y] == 1)
                //    return true;
            }
            
            return false;
        }
           
            
        
        bool CheckCollisionWormWithWall()
        {
            for(int i = 0; i < wall.body.Count; i++)
            {
                if(wall.body[i].X == w.body[0].X && wall.body[i].Y == w.body[0].Y)
                {
                    return true;
                }
            }
            return false;
        }
        public void NextLevel()
        {
            
            if(w.body.Count == 4)
            {
                wormTimer.Enabled = false;
                Console.Clear();
                Console.SetCursorPosition(7, 15);
                Console.Write("Press A to go to next level!");
                ConsoleKeyInfo press = Console.ReadKey(true);
                switch (press.Key)
                {
                    case ConsoleKey.A:
                        Console.Clear();
                        wormTimer.Enabled = true;
                        w = new Worm('@', ConsoleColor.Green);
                        f = new Food('$', ConsoleColor.Yellow);
                        f.Draw();
                        wall = new Wall('#', ConsoleColor.DarkCyan, @"C:\Users\пк\source\repos\snake\Levels\Level2.txt");
                        score = new Score(0);
                        wall.Draw();
                        break;
                }
                
            }
            
        }

        void Move2(object sender, ElapsedEventArgs e)
        {
            
            w.Move();
            w.Draw();
            if (CheckCollisionFoodWithWall())
            {
                f.Generate();
                f.Draw();
            }

            if (CheckCollisionFoodWithWorm())
            {
                f.Generate();
                f.Draw();
            }

            if (CheckWormEatFood())
            {
                score.Increase();
                w.Increase(w.body[0]);
                f.Generate();
                f.Draw();
            }
            if (CheckCollisionWormWithWall())
            {
                wormTimer.Stop();
                Console.Clear();
                Console.SetCursorPosition(13, 15);
                Console.Write("You hit the wall!");
            }

            NextLevel();

            getScores();
           
        }
        public void KeyPressed(ConsoleKeyInfo pressedKey)
        {
            
            
            switch (pressedKey.Key)
            {
                case ConsoleKey.UpArrow:
                    w.ChangeDirection(0, -1);
                    break;
                case ConsoleKey.DownArrow:
                    w.ChangeDirection(0, 1);
                    break;
                case ConsoleKey.LeftArrow:
                    w.ChangeDirection(-1, 0);
                    break;
                case ConsoleKey.RightArrow:
                    w.ChangeDirection(1, 0);
                    break;
                case ConsoleKey.S:
                    w.Save("Worm");
                    //f.Save("Food");
                    break;
                case ConsoleKey.L:
                    wormTimer.Stop();
                    w.Clear();
                    f = new Food('$', ConsoleColor.Yellow);
                    wall = new Wall('#', ConsoleColor.DarkYellow, @"C:\Users\пк\source\repos\snake\Levels\Level1.txt");
                    //f = Food.Load("Food");
                    w = Worm.Load("Worm");
                    wormTimer.Start();
                    break;
                case ConsoleKey.Escape:
                    IsRunning = false;
                    break;
                case ConsoleKey.Spacebar:
                    if (!pause)
                    {
                        wormTimer.Stop();
                        pause = true;
                    }
                    else
                    {
                        wormTimer.Start();
                        pause = false;
                    }
                    break;
            }
 

        }
    }
}
