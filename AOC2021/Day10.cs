using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AOC2021
{
    class Day10
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day10.txt").ToList();
        private static Dictionary<char, char> symbolsDictionary = new Dictionary<char, char> { { '(', ')' }, { '[', ']' }, { '{', '}' }, { '<', '>' } };
        private static List<char> endSymbolsList = new List<char> { ')', ']', '}', '>' };

        public static string Run()
        {
            List<char[]> lines = new List<char[]>();
            for (int i = 0; i < fileInputList.Count; i++)
            {
                char[] tempArray = fileInputList[i].ToCharArray();
                lines.Add(tempArray);
            }

            int solution1 =  Part1(lines);
            long solution2 = Part2(lines);

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static int Part1(List<char[]> lines)
        {
            int corrupted = 0;
            foreach (var singleLine in lines)
            {
                List<char> lifo = new List<char>();
                foreach (var chunks in singleLine)
                {
                    if (chunks.Equals('{') || chunks.Equals('<') || chunks.Equals('[') || chunks.Equals('('))
                    {
                        lifo.Add(chunks);
                    }
                    else
                    {
                        char comparable = lifo[lifo.Count-1];
                        lifo.RemoveAt(lifo.Count - 1);

                        switch (chunks)
                        {
                            case '}':
                                if (!comparable.Equals('{')) corrupted += 1197;
                                break;
                            case '>':
                                if (!comparable.Equals('<')) corrupted += 25137;
                                break;
                            case ']':
                                if (!comparable.Equals('[')) corrupted += 57;
                                break;
                            case ')':
                                if (!comparable.Equals('(')) corrupted += 3;
                                break;
                            default:
                                break;
                        }
                            
                    }
                }
            }
            return corrupted;
        }

        public static long Part2(List<char[]> lines)
        {
            List<long> scoresList = new List<long>();
            var pointsDictionary = new Dictionary<char, int> { { '(', 1 }, { '[', 2 }, { '{', 3 }, { '<', 4 } };

            foreach (var singleLine in lines)
            {
                var symbolsStack = new Stack<char>();
                long sum = 0L;
                bool corrupted = false;

                foreach (var chunks in singleLine)
                {
                    if (pointsDictionary.ContainsKey(chunks))
                    {
                        symbolsStack.Push(chunks);
                    }
                    else
                    {
                        if (!(symbolsStack.Count  == 0) && endSymbolsList.Contains(chunks))
                        {
                            char comparable = symbolsStack.Pop();

                            if (!(symbolsDictionary[comparable] == chunks))
                            {
                                corrupted = true;
                                break;
                            }
                        }

                    }
                }
                while (!(symbolsStack.Count == 0) && !(corrupted == true))
                {
                    char searched = symbolsStack.Pop() ;
                    sum *= 5;
                    sum += pointsDictionary[searched];
                }
                if(sum != 0)
                    scoresList.Add(sum);
            }
            
            scoresList.Sort();
            int place = (scoresList.Count / 2);
            long middle = scoresList[place];
            
            return middle;
        }
    }
}
