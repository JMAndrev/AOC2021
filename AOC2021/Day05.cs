using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace AOC2021
{
    class Day05
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day05.txt").ToList();
        private static Dictionary<string, int> coordinatesDict = new Dictionary<string, int>();
        public Day05()
        {}
        public static string Run()
        {
            int solution1 = Part1();
            int solution2 = Part2();

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }
        private static int Part1()
        {
            for (int i = 0; i < fileInputList.Count; i++)
            {
                Regex regex = new Regex(@"\d+");
                MatchCollection matches = regex.Matches(fileInputList[i]);
                int[] numbers = matches.Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();

                if (numbers[0] == numbers[2] || numbers[1] == numbers[3])
                {
                    int max = (numbers[0] - numbers[2] == 0) ? (Math.Abs(numbers[1] - numbers[3])) : (Math.Abs(numbers[0] - numbers[2]));
                    for (int j = 0; j <= max; j++)
                    {
                        var newX = numbers[0];
                        var newY = numbers[1];
                        if(numbers[0] - numbers[2] != 0)
                            newX = numbers[0] + j * (numbers[2] - numbers[0] > 0 ? 1 : -1);
                        if (numbers[1] - numbers[3] != 0)
                            newY = numbers[1] + j * (numbers[3] - numbers[1] > 0 ? 1 : -1);
                        string currKey = newX + "," + newY;
                        AddValues(currKey);
                    }
                }
            }
            return CountRepeated();
        }

        private static int Part2()
        {
            for (int i = 0; i < fileInputList.Count; i++)
            {
                Regex regex = new Regex(@"\d+");
                MatchCollection matches = regex.Matches(fileInputList[i]);
                int[] numbers = matches.Cast<Match>().Select(m => int.Parse(m.Value)).ToArray();

                for(int j = 0; j <= Math.Max(numbers[0], numbers[2]) - Math.Min(numbers[0], numbers[2]); j++)
                {
                    if (Math.Abs(numbers[0] - numbers[2]) == Math.Abs(numbers[1] - numbers[3])){

                        var newX = numbers[0] + j * (numbers[2] - numbers[0] > 0 ? 1 : -1);
                        var newY = numbers[1] + j * (numbers[3] - numbers[1] > 0 ? 1 : -1);
                        string currKey = newX.ToString() + "," + newY.ToString();
                        AddValues(currKey);
                    }
                }
            }
            return CountRepeated();
        }

        private static void AddValues(string key)
        {
            if (coordinatesDict.ContainsKey(key))
                coordinatesDict[key]++;
            else
                coordinatesDict[key] = 1;
        }

        private static int CountRepeated()
        {
            int count = 0;
            foreach (var key in coordinatesDict.Keys)
            {
                count += (coordinatesDict[key] > 1) ? 1 : 0;
            }
            return count;
        }
    }
}
