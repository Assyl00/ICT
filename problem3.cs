using System;

namespace problem3
{
    public class Solution
    {
        public int NumberOfSteps(int num)
        {
            int cnt = 0;
            while (num > 0)
            {
                if (num % 2 == 0)
                    num /= 2;
                else
                    num--;
                cnt++;
            }
            return cnt;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            int num = 14;
            Console.WriteLine(s.NumberOfSteps(num));
        }
    }
}
