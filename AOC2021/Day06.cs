using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace AOC2021
{
    class Day06
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day06.txt").ToList();

        public static string Run()
        {
            List<int> lanternfishValues = fileInputList[0].Split(',').Select(Int32.Parse).ToList();
            long solution1 = SumFish(lanternfishValues, 80, 8, 6);
            long solution2 = SumFish(lanternfishValues, 256, 8,6);

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static long SumFish(List<int> fish, int Days, int StartDays, int ReoccuringDays)
        {
            long[] numberOfFish = new long[StartDays + 1];
            for (int i = 0; i <= StartDays; i++)
            {
                numberOfFish[i] = 0;
            }

            for (int i = 0; i < fish.Count; i++)
            {
                numberOfFish[fish[i]]++;
            }

            for (int i = 0; i < Days; i++)
            {
                long[] tempList = new long[StartDays + 1];
                long temp6 = 0;
                for (int j = 0; j < numberOfFish.Length; j++)
                {
                    if (numberOfFish[j] != 0 && j != 0)
                    {
                        tempList[j - 1] = numberOfFish[j];
                    }
                    else if (numberOfFish[j] != 0 && j == 0)
                    {
                        temp6 = numberOfFish[0];
                        tempList[StartDays] = numberOfFish[0];
                    }
                    else if (numberOfFish[j] == 0 && j != 0)
                    {
                        tempList[j - 1] = numberOfFish[j];
                    }
                    else if (numberOfFish[j] == 0 && j == 0)
                    {
                        tempList[j] = numberOfFish[j];
                    }
                }
                tempList[ReoccuringDays] += temp6;
                for (int j = 0; j <= StartDays; j++)
                {
                    numberOfFish[j] = tempList[j];
                }
            }

            long sum = 0;

            for (int i = 0; i <= StartDays; i++)
            {
                sum += numberOfFish[i];
            }

            return sum;
        }
    }
}
