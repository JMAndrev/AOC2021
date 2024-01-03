using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2021
{
    class Day11
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day11.txt").ToList();
        //1647

        private static List<List<Tuple<int, int>>> energyList;

        public static string Run()
        {
            int solution1 = Part1();
            int solution2 = Part2();

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }
        private static void createLines() {
            energyList = new List<List<Tuple<int, int>>>();
            for (int i = 0; i < fileInputList.Count; i++)
            {
                List<int> tempList = fileInputList[i].Select(c => int.Parse(c.ToString())).ToList();
                List<Tuple<int, int>> listOfPairs = new List<Tuple<int, int>>();
                for (int j = 0; j < tempList.Count; j++)
                {
                    Tuple<int, int> newPair = Tuple.Create(tempList[j], 0);
                    listOfPairs.Add(newPair);
                }
                energyList.Add(listOfPairs);
            }
        }

        private static int Part1()
        {
            int steps = 100;
            createLines();
            int flashlights = 0;
            for (int s = 0; s < steps; s++)
            {
                for (int i = 0; i < energyList.Count; i++)
                {
                    for (int j = 0; j < energyList[i].Count; j++)
                    {
                        bool check = SetPointValue(i, j);
                        if (check)
                            CheckAllSides(i, j);
                    }
                }
                flashlights += CountFlashlights();
            }

            return flashlights;
        }

        private static int Part2()
        {
            createLines();
            int counter = 0;
            int searched = energyList.Count * energyList[0].Count;
            int steps = 0;

            while (counter != searched)
            {
                for (int i = 0; i < energyList.Count; i++)
                {
                    for (int j = 0; j < energyList[i].Count; j++)
                    {
                        bool check = SetPointValue(i, j);
                        if (check)
                            CheckAllSides(i, j);
                    }
                }
                counter = CountFlashlights();
                steps++;
            }

            return steps;
        }

        private static bool SetPointValue(int x, int y)
        {
            if (energyList[x][y].Item2 == 0)
            {
                int num = energyList[x][y].Item1 + 1;
                if (num > 9)
                    num = 0;
                if (num == 0)
                {
                    energyList[x][y] = Tuple.Create(num, 1);
                    return true;
                }
                else
                {
                    energyList[x][y] = Tuple.Create(num, 0);
                }
            }
            return false;
        }

        private static void CheckAllSides(int x, int y)
        {
            if (!(y == 0) && SetPointValue(x, y - 1))
                CheckAllSides(x, y - 1);
            if (!(y == energyList[x].Count - 1) && SetPointValue(x, y + 1))
                CheckAllSides(x, y + 1);
            if (!(x == 0) && !(y == 0) && SetPointValue(x - 1, y - 1))
                CheckAllSides(x - 1, y - 1);
            if (!(x == 0) && SetPointValue(x - 1, y))
                CheckAllSides(x - 1, y);
            if (!(x == 0) && !(y == energyList[x].Count - 1) && SetPointValue(x - 1, y + 1))
                CheckAllSides(x - 1, y + 1);
            if (!(x == energyList.Count - 1) && !(y == 0) && SetPointValue(x + 1, y - 1))
                CheckAllSides(x + 1, y - 1);
            if (!(x == energyList.Count - 1) && SetPointValue(x + 1, y))
                CheckAllSides(x + 1, y);
            if (!(x == energyList.Count - 1) && !(y == energyList[x].Count - 1) && SetPointValue(x + 1, y + 1))
                CheckAllSides(x + 1, y + 1);

        }

        private static int CountFlashlights()
        {
            int sum = 0;
            for (int i = 0; i < energyList.Count; i++)
            {
                for (int j = 0; j < energyList[i].Count; j++)
                {
                    if (energyList[i][j].Item2 == 1)
                    {
                        energyList[i][j] = Tuple.Create(energyList[i][j].Item1, 0);
                        sum++;
                    }
                }
            }
            return sum;
        }
    }
}
