using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab2
{
    public class Layer
    {
        public DirectoryInfo dir
        {
            get;
            set;
        }
        public int pos
        {
            get;
            set;
        }
        public List<FileSystemInfo> content
        {
            get;
            set;
        }
        public bool isFile = false;
        public Layer(DirectoryInfo dir, int pos)
        {
            this.dir = dir;
            this.pos = pos;
            this.content = new List<FileSystemInfo>();


            content.AddRange(this.dir.GetDirectories());
            content.AddRange(this.dir.GetFiles());
        }

        long GetSizeOfDirectory(DirectoryInfo d)
        {
            long totalSize = d.EnumerateFiles().Sum(file => file.Length);
            totalSize += d.EnumerateDirectories().Sum(dir => GetSizeOfDirectory(dir));

            return totalSize;
        }
        public void PrintInfo()
        {
            long size;
            DateTime date;
            Console.BackgroundColor = ConsoleColor.Blue;
            

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.White;
            int cnt = 0;
            foreach (DirectoryInfo d in dir.GetDirectories())
            {
                date = d.CreationTime;
                if (cnt == pos)
                {
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine("Name: " + d.Name + " Size: " + GetSizeOfDirectory(d) + "bytes " + " Was created in: " + date);
                cnt++;
            }
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            foreach (FileInfo f in dir.GetFiles())
            {
                
                size = f.Length;
                date = f.CreationTime;
                if (cnt == pos)
                {
                    if (isFile)
                    {
                        Console.WriteLine(Open(f.ToString()));
                    }
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    

                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.Blue;
                }
                Console.WriteLine("Name: " + f.Name + " size: " + size + "bytes " + " was created in: " + date);
                cnt++;
                
            }
            
        }

        public FileSystemInfo GetCurrentObject()
        {
            return content[pos];
        }

        public void SetNewPosition(int d)
        {
            if (d > 0)
            {
                pos++;
            }
            else
            {
                pos--;
            }
            if (pos >= content.Count)
            {
                pos = 0;
            }
            else if (pos < 0)
            {
                pos = content.Count - 1;
            }
        }

        public string Open(string path)
        {

            string text = File.ReadAllText(path);
            return text;
            //Console.WriteLine(text);

        }


    }
}
