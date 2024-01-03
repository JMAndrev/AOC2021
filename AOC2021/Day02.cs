using System;
using System.Linq;
using System.IO;

namespace AOC2021
{
    class Day02
    {
        private static string[] fileInputArray = File.ReadAllLines(@"Day02.txt").ToArray();
        public static string Run()
        {
            int solution1 = Part1();
            int solution2 = Part2();

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static int Part1()
        {
            int horizontal = 0;
            int depth = 0;
            for (int i = 0; i < fileInputArray.Length; i++)
            {
                string[] line = fileInputArray[i].Split(' ');
                string command = line[0];
                int number = int.Parse(line[1]);

                switch (command)
                {
                    case "forward":
                        horizontal += number;
                        break;

                    case "up":
                        depth -= number;
                        break;

                    case "down":
                        depth += number;
                        break;
                }
            }
            return horizontal * depth;
        }

        private static int Part2()
        {
            int horizontal = 0;
            int depth = 0;
            int aim = 0;
            for (int i = 0; i < fileInputArray.Length; i++)
            {
                string[] line = fileInputArray[i].Split(' ');
                string command = line[0];
                int number = int.Parse(line[1]);

                switch (command)
                {
                    case "forward":
                        horizontal += number;
                        depth += aim * number;
                        break;

                    case "up":
                        aim -= number;
                        break;

                    case "down":
                        aim += number;
                        break;
                }

            }
            return horizontal * depth;
        }
    }
}
        
