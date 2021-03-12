using System;
using System.Collections.Generic;
using System.IO;

namespace Lab2
{
    class Program
    {
        private static void F3()
        {

            Stack<Layer> history = new Stack<Layer>();
            String path = @"C:\Users\пк\Desktop\ict";
            history.Push(new Layer(new DirectoryInfo(path), 0));

            bool escape = false;
            

            while (!escape)
            {
                

                Console.Clear();
                
                //if (history.Peek().GetCurrentObject().GetType() == typeof(FileInfo))
                //{
                //    history.Peek().Open(history.Peek().GetCurrentObject().ToString());
                //}

                if (history.Count > 0)
                {
                    history.Peek().PrintInfo();
                    

                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("No file");
                    
                    break;
                }
                //if (history.Peek().isFile)
                //{
                //    history.Peek().Open(history.Peek().GetCurrentObject().ToString());
                //}
                ConsoleKeyInfo consoleKeyInfo = Console.ReadKey(true);

                switch (consoleKeyInfo.Key)
                {
                    case ConsoleKey.Enter:
                        if (history.Peek().GetCurrentObject().GetType() == typeof(DirectoryInfo))
                        {
                            history.Push(new Layer(history.Peek().GetCurrentObject() as DirectoryInfo, 0));
                            
                        }
                        else if (history.Peek().GetCurrentObject().GetType() == typeof(FileInfo))
                        {
                            history.Peek().isFile = true;
                            //history.Peek().Open(history.Peek().GetCurrentObject().ToString());
                        }
                        break;
                    case ConsoleKey.S:
                        Console.Clear();
                        break;
                    case ConsoleKey.UpArrow:
                        history.Peek().SetNewPosition(-1);
                        
                        break;
                    case ConsoleKey.DownArrow:
                        history.Peek().SetNewPosition(1);
                        
                        break;
                    case ConsoleKey.Escape:
                        history.Pop();
                        
                        break;
                }
            }
        }
        static void Main(string[] args)
        {
            F3();
        }

    }
}
