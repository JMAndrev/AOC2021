using System;
using System.Linq;
using System.IO;


namespace AOC2021
{
    class Day01
    {
        public Day01() {}

        private static int[] fileInputArray = File.ReadAllLines(@"Day01.txt").Select(int.Parse).ToArray();
        public static string Run()
        {
           
            int solution1 = Part1();
            int solution2 = Part2();
            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static int Part1() {
            int count = 0;
            for (var i = 1; i < fileInputArray.Length; i++)
            {
                count += (fileInputArray[i] > fileInputArray[i - 1]) ? 1:0;
            }
            return count;
        }

        private static int Part2() {
            int count = 0;

            for (var i = 0; i < fileInputArray.Length-3; i++)
            {
                int sum1 = fileInputArray[i] + fileInputArray[i + 1] + fileInputArray[i + 2];
                int sum2 = fileInputArray[i + 1] + fileInputArray[i + 2] + fileInputArray[i + 3];

                count += (sum2 > sum1) ? 1 : 0;
            }

            return count-1;
        }
    }
}
