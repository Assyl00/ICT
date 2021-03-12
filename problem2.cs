using System;

namespace prob2
{
    public class Solution
    {
        public string DefangIPaddr(string address)
        {
            if (address == null || address.Length == 0)
                return "";
            var letters = new System.Text.StringBuilder();
            foreach (var i in address)
            {
                if (i == '.')
                    letters.Append("[.]");
                else
                    letters.Append(i);
            }
            return letters.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            string address = "1.1.1.1.1.1";
            Console.Write(s.DefangIPaddr(address));
            //foreach (int x in s.DefangIPaddr(address: "1.1.1.1.1"))
              //  Console.Write(x + " ");
        
        }
    }
}
