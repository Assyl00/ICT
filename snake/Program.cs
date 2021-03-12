using System;

namespace snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 40);
            Console.SetBufferSize(40, 40);
            Console.SetCursorPosition(7, 15);
            Console.Write("Enter username: ");
            string name = Console.ReadLine();
            
            ConsoleKeyInfo press = Console.ReadKey(true);
            switch (press.Key)
            {
                case ConsoleKey.Enter:
                    Console.Clear();
                    Game game = new Game();
                    while (game.IsRunning)
                    {
                        
                        game.KeyPressed(Console.ReadKey(true));
                        
                    }

                    break;
            }

            //        Game game = new Game();
            // while (game.IsRunning)
            //{  
            //   game.KeyPressed(Console.ReadKey(true));

            //}
                 
            

            

            
        }
    }
}
