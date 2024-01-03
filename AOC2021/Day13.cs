using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace AOC2021
{
    class Day13
    {
        public static string Run()
        {
            List<string> fileInputList = File.ReadAllLines(@"Day13.txt").ToList();
            List<int> xCoordinatesList = new List<int>();
            List<int> yCoordinatesLisr = new List<int>();
            int lineNum = 0;

            while (!(String.IsNullOrEmpty(fileInputList[lineNum]))) {
                List<string> row = new List<string>(fileInputList[lineNum].Split(',').ToList());
                xCoordinatesList.Add(int.Parse(row[0]));
                yCoordinatesLisr.Add(int.Parse(row[1]));
                lineNum++;
            }
            int xMax = xCoordinatesList.Max();
            int yMax = yCoordinatesLisr.Max();

            string[][] mapArray = new string[xMax + 1][];
            for (int i = 0; i <= xMax; i++)
            {
                mapArray[i] = new string[yMax + 1];
            }

            for (int i = 0; i < xCoordinatesList.Count; i++)
            {
                mapArray[xCoordinatesList[i]][yCoordinatesLisr[i]] = "x";
            }

            for (int i = lineNum + 1; i < fileInputList.Count; i++)
            {
                int fold = int.Parse(Regex.Replace(fileInputList[i], "[^0-9]", ""));
                if (fileInputList[i].Contains("x"))
                {
                    mapArray = FoldX(mapArray, fold);
                }
                else if (fileInputList[i].Contains("y"))
                {
                    mapArray = FoldY(mapArray, fold);
                }
            }

            for (int i = 0; i < mapArray[0].Length; i++)
            {
                for (int j = 0; j < mapArray.Length; j++)
                {
                    System.Console.Write(mapArray[j][i]);
                }
                System.Console.WriteLine();
            }

            return ("Solution is above");
        }

        private static string[][] FoldX(string[][] boardArray, int xFold)
        {
            int xMax = boardArray.Length;
            int yMax = boardArray[0].Length;
            string[][] newBoard = new string[xFold][];
            for (int i = 0; i < xFold; i++)
            {
                newBoard[i] = new string[yMax];
            }

            for (int i = xFold + 1; i < xMax; i++)
            {
                for (int j = 0; j < yMax; j++)
                {
                    if ((!(boardArray[i][j] == null) && (boardArray[i][j] == "x")) ||
                        (!(boardArray[xFold - (i - xFold)][j] == null) && (boardArray[xFold - (i - xFold)][j] == "x")))
                    {
                        newBoard[xFold - (i - xFold)][j] = "x";
                    }
                    else
                    {
                        newBoard[xFold - (i - xFold)][j] = ".";
                    }
                }
            }
            return newBoard;
        }

        private static string[][] FoldY(string[][] boardArray, int yFold)
        {
        int xMax = boardArray.Length;
        int yMax = boardArray[0].Length;
        string[][] newBoard = new string[xMax][];
        for (int i = 0; i < xMax; i++)
        {
            newBoard[i] = new string[yFold];
        }

        for (int i = yFold + 1; i < yMax; i++)
        {
            for (int j = 0; j < xMax; j++)
            {
                if ((!(boardArray[j][i] == null) && (boardArray[j][i] == "x")) ||
                    (!(boardArray[j][yFold - (i - yFold)] == null) && (boardArray[j][yFold - (i - yFold)] == "x")))
                {
                    newBoard[j][yFold - (i - yFold)] = "x";
                }
                else
                {
                    newBoard[j][yFold - (i - yFold)] = ".";
                }
            }
        }
        return newBoard;
    }
    }
}