using System;
using System.Collections.Generic;
using System.Text;

namespace snake
{
    class Score
    {
        int score;

        public Score(int score)
        {
            this.score = score;
        }

        public int getScore()
        {
            return score;
        }

        public void Increase()
        {
            score++;
        }

        //public void PrintScore()
        //{
        //    Console.SetCursorPosition(15, 2);
        //    Console.
        //}
    }
}
