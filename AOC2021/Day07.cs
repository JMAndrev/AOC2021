using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace AOC2021
{
    class Day07
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day07.txt").ToList();
        public static string Run()
        {
            List<int> crabStartPosition = fileInputList[0].Split(',').Select(Int32.Parse).ToList();
            long solution1 = Part1(crabStartPosition);
            long solution2 = Part2(crabStartPosition);

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static long Part1(List<int> crabList)
        {
            crabList.Sort();
            int fuel = int.MaxValue;
            for (int i = 0; i < crabList[crabList.Count - 1]; i++)
            {
                int tempFuel = 0;
                for (int j = 0; j < crabList.Count; j++)
                {
                    tempFuel += Math.Abs(crabList[j] - i);
                }
                if (tempFuel < fuel) fuel = tempFuel;
            }
            return fuel;
        }
        private static long Part2(List<int> crabList)
        {
            crabList.Sort();
            long fuel = long.MaxValue;
            for (int i = 0; i <= crabList[crabList.Count - 1]; i++)
            {
                long tempFuel = 0;
                for (int j = 0; j < crabList.Count; j++)
                {
                    int steps = 0;
                    if (Math.Abs(crabList[j] - i) > 0) steps = sumSteps(Math.Abs(crabList[j] - i));
                    tempFuel += steps;
                }
                if (tempFuel < fuel) fuel = tempFuel;
            }
            return fuel;
        }
        private static int sumSteps(int stepsNum)
        {
            int stepsSum = 0;
            for (int i = 1; i <= stepsNum; i++)
            {
                stepsSum += i;
            }
            return stepsSum;
        }
    }
}
