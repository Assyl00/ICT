using System;

namespace problem1
{
    public class Solution
    {
        public int[] RunningSum(int[] nums)
        {
            for (int i = 1; i < nums.Length; i++)
            {
                nums[i] += nums[i - 1];
            }
            return nums;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Solution s = new Solution();
            foreach(int x in s.RunningSum(new int[] { 1, 2, 3, 4, 5}))
            Console.Write(x + " ");
        }
    }
}
