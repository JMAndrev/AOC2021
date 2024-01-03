using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;


namespace AOC2021
{
    class Day09
    {
        private static string[] fileInputArray = File.ReadAllLines(@"Day09.txt");
        private static int[][] PointsArray;

        public static string Run()
        {
            PointsArray = fileInputArray.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            int solution1 = Part1();
            long solution2 = Part2();
            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }
        private static int Part1()
        {
            int mins = 0;
            for (int i = 0; i < PointsArray.Length; i++)
            {
                for (int j = 0; j < PointsArray[i].Length; j++)
                {
                    if ((i != 0) && (PointsArray[i][j] >= PointsArray[i - 1][j]))
                        continue;
                    if ((i != PointsArray.Length - 1) && (PointsArray[i][j] >= PointsArray[i + 1][j]))
                        continue;
                    if ((j != 0) && (PointsArray[i][j] >= PointsArray[i][j - 1]))
                        continue;
                    if ((j != PointsArray[0].Length - 1) && (PointsArray[i][j] >= PointsArray[i][j + 1]))
                        continue;
                    mins += 1 + PointsArray[i][j];
                }
            }
            return mins;
        }
        public static long Part2()
        {
            List<int> sizesList = new List<int>();
            for (int i = 0; i < PointsArray.Length; i++)
            {
                for (int j = 0; j < PointsArray[0].Length; j++)
                {
                    if (PointsArray[i][j] != 9)
                    {
                        List<Tuple<int, int>> queue = new List<Tuple<int, int>>();
                        Tuple<int, int> newTuple = Tuple.Create(i, j);
                        queue.Add(newTuple);
                        int size = 0;

                        while (queue.Count > 0)
                        {
                            Tuple<int, int> currentPoint = queue[0];
                            queue.RemoveAt(0);

                            int x = currentPoint.Item1;
                            int y = currentPoint.Item2;

                            if (PointsArray[x][y] != 9)
                            {
                                PointsArray[x][y] = 9;
                                size += 1;
                                queue = AddNeighborToQueue(queue, x - 1, y, PointsArray);
                                queue = AddNeighborToQueue(queue, x + 1, y, PointsArray);
                                queue = AddNeighborToQueue(queue, x, y - 1, PointsArray);
                                queue = AddNeighborToQueue(queue, x, y + 1, PointsArray);

                            }
                        }
                        sizesList.Add(size);
                    }
                }
            }

            return sizesList.OrderByDescending(x => x).Take(3).Aggregate((x, y) => x*y);
        }

        private static List<Tuple<int, int>> AddNeighborToQueue(List<Tuple<int, int>> queue, int x, int y, int[][] matrix)
        {
            if (x >= 0 && x < matrix.Length && y >= 0 && y < matrix[0].Length && matrix[x][y] != 9)
            {
                Tuple<int, int> newTuple = Tuple.Create(x, y);
                queue.Add(newTuple);
            }
            return queue;
        }

    }
}