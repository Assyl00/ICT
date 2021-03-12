using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace snake
{
    public class Food : GameObject
    {
        Random rnd = new Random();

        public Food()
        {

        }
        public Food(char sign, ConsoleColor color) : base(sign, color)
        {
            Point location = new Point { X = rnd.Next(1, Game.Width), Y = rnd.Next(1, Game.Height) };
            body.Add(location);
            //Draw();
        }
        public void Generate()
        {
            
                body[0].X = rnd.Next(1, 39);
                body[0].Y = rnd.Next(1, 39);
            //Draw();

        }
        public void Save(string title)
        {
            using (FileStream fs = new FileStream(title + ".xml", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Food));
                xs.Serialize(fs, this);
            }
        }

        public static Food Load(string title)
        {
            Food res = null;
            using (FileStream fs = new FileStream(title + ".xml", FileMode.Open, FileAccess.Read))
            {
                XmlSerializer xs = new XmlSerializer(typeof(Food));
                res = xs.Deserialize(fs) as Food;

            }

            return res;
        }
    }
}
