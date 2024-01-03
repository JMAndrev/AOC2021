using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021
{
    class Day04
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day04.txt").ToList();
        public static string Run()
        {
            List<int> bingoValues = fileInputList[0].Split(',').Select(Int32.Parse).ToList();
            fileInputList.RemoveAt(0);
            List<string> bingoData = fileInputList.Where(x => !string.IsNullOrEmpty(x)).ToList();
            
            int solution1 = Part1(bingoData, bingoValues);
            int solution2 = Part2(bingoData, bingoValues);

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }
        private static int Part1(List<string> boardsData, List<int> bingoValues) {
            List<Bingo> boardsList = CreateBoards(boardsData);
            int result = 0;
            for (int i = 0; i < bingoValues.Count; i++)
            {
                for (int j = 0; j < boardsList.Count; j++)
                {
                    result = boardsList[j].FindValue(bingoValues[i]);
                    if (result != 0)
                    {
                        return result;
                    }
                }
            }
            return result;

        }
        private static int Part2(List<string> boardsData, List<int> bingoValues) { 
            List<Bingo> boardsList = CreateBoards(boardsData);
            int result = 0;
            for (int i = 0; i < bingoValues.Count; i++)
            {
                for (int j = boardsList.Count - 1; j >= 0; j--)
                {
                    Bingo testBingo = boardsList[j];
                    result = testBingo.FindValue(bingoValues[i]);

                    if (result != 0)
                    {
                        boardsList.RemoveAt(j);
                    }
                }
            }
            return result;
        }
        private static List<Bingo> CreateBoards(List<string> bingoList)
        {
            int bingoSize = 5;
            List<Bingo> boardsBoards = new List<Bingo>();

            for (int i = 0; i < bingoList.Count; i = i + 5)
            {
                List<List<int>> rows = new List<List<int>>();

                for (int j = 0; j < bingoSize; j++)
                {
                    List<String> tempList = new List<String>((bingoList[i + j]).Split(' '));
                    List<int> row = tempList.Where(x => !string.IsNullOrEmpty(x)).Select(int.Parse).ToList();
                    rows.Add(row);
                }
                Bingo board = new Bingo(i, rows);
                boardsBoards.Add(board);
            }
            return boardsBoards;
        }
    }

    class Bingo
    {
        List<List<int>> rows;
        List<List<int>> checkRows;
        List<List<int>> checkColumns;
        int boardNumber;
        int bingoLength;

        public Bingo(int boardNum, List<List<int>> allRows)
        {
            boardNumber = boardNum;
            rows = allRows;
            bingoLength = allRows.ElementAt(0).Count;
            checkRows = new List<List<int>>();
            checkColumns = new List<List<int>>();
            SetEmptyRowsAndColumns();
        }
        public void SetEmptyRowsAndColumns()
        {
            for (int i = 0; i < bingoLength; i++)
            {
                List<int> singleRow = new List<int>();
                List<int> singleColumn = new List<int>();
                for (int j = 0; j < bingoLength; j++)
                {
                    singleRow.Add(- 1);
                    singleColumn.Add(-1);
                }
                singleRow.Add(0);
                singleColumn.Add(0);
                checkRows.Add(singleRow);
                checkColumns.Add(singleColumn);
            }
        }
        public int AddToChecked(int rowNum, int colNum, int bingoValue)
        {
            checkRows[rowNum][colNum] = bingoValue;
            int inRow = checkRows[rowNum][bingoLength];
            checkRows[rowNum][bingoLength] = inRow + 1;
            checkColumns[colNum][rowNum] = bingoValue;
            int inCol = checkColumns[colNum][bingoLength];
            checkColumns[colNum][bingoLength] = inCol + 1;
            int sum = 0;
            if (inRow + 1 == bingoLength || inCol + 1 == bingoLength)
            {
                sum = SumEmpty();
            }
            return sum;
        }
        public int FindValue(int searchedValue)
        {
            int sum = 0;
            for (int i = 0; i < rows.Count; i++)
            {
                for (int j = 0; j < bingoLength; j++)
                {
                    int test = rows[i][j];
                    if (rows[i][j] == searchedValue)
                    {
                        sum = AddToChecked(i, j, searchedValue);
                    }
                    if (sum != 0)
                    {
                        return sum * searchedValue;
                    }
                }
            }
            return 0;
        }
        public int SumEmpty()
        {
            int emptySum = 0;
            for (int i = 0; i < bingoLength; i++)
            {
                for (int j = 0; j < bingoLength; j++)
                {
                    if (rows[i][j] != checkRows[i][j])
                        emptySum += rows[i][j];
                }
            }
            return emptySum;
        }
    }
}
