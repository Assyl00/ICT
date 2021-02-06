using System;

namespace problem4
{
    public class Solution
    {
        public bool ArrayStringsAreEqual(string[] word1, string[] word2)
        {
            string a, b;
            a = string.Join("", word1);
            b = string.Join("", word2);
            if (a == b)
                return true;
            else
                return false;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            string[] word1 = { "a", "bc" };
            string[] word2 = { "ab", "c" };
            Console.Write(s.ArrayStringsAreEqual(word1, word2));
           
        }
    }
}
