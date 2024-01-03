using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Dynamic;


namespace AOC2021
{
    class Class1
    {
        private static string[] fileInputArray = File.ReadAllLines(@"Day09.txt");
        private static int[][] PointsArray;

        public static string Run()
        {
            PointsArray = fileInputArray.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            int[][] basinGrid = PointsArray;
            long solution2 = floodFill(basinGrid);
            
            return  ("part2: " + solution2);
        }


        public static long floodFill(int[][] matrix)
        {
            List<int> sizes = new List<int>();
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    if (matrix[i][j] != 9)
                    {
                        List<Tuple<int, int>> queue = new List<Tuple<int, int>>();
                        Tuple<int, int> newTuple = Tuple.Create(i, j);
                        var result = reccurentFill(newTuple, queue, matrix, 0, 0);
                        matrix = result.Item1;
                        sizes.Add(result.Item2);
                    }
                }
            }
            sizes.Sort();
            long sum = sizes[sizes.Count - 1] * sizes[sizes.Count - 2] * sizes[sizes.Count - 3];

            return sum;
        }

        
        private static (int[][], int) reccurentFill(Tuple<int, int> newTuple, List<Tuple<int, int>> queue, int[][] matrix, int steps, int sum)
        {
            int x = newTuple.Item1;
            int y = newTuple.Item2;      
            newTuple = getCoordinates(newTuple, matrix);
            matrix[x][y] = 9;

            sum += 1;
            if (queue.Count > 0)
            {
                var newPointVal = queue[0];
                queue.RemoveAt(0);

                while (queue.Count > 0 && matrix[newPointVal.Item1][newPointVal.Item2] == 9)
                {
                    newPointVal = queue[0];
                    queue.RemoveAt(0);
                    steps += queue.Count;
                }

                if (queue.Count == 0 && matrix[newPointVal.Item1][newPointVal.Item2] == 9)
                    return (matrix, sum);

                var res = reccurentFill(newPointVal, queue, matrix, steps, sum);
                matrix = res.Item1;
                sum = res.Item2;
            }

            return (matrix, sum);
        }

        private static Tuple<int, int> getCoordinates(Tuple<int, int> newPoint, int[][] matrixArray)
        {
            int x = newPoint.Item1;
            int y = newPoint.Item2;
            if (x != 0 && matrixArray[x - 1][y] != 9)
                newPoint = Tuple.Create(x - 1, y);
            if (x + 1 != matrixArray.Length && matrixArray[x + 1][y] != 9)
                newPoint = Tuple.Create(x + 1, y);
            if (y != 0 && matrixArray[x][y - 1] != 9)
                newPoint = Tuple.Create(x, y - 1);
            if (y + 1 != matrixArray[0].Length && matrixArray[x][y + 1] != 9)
                newPoint = Tuple.Create(x, y + 1);

            return newPoint;
        }
    }
}