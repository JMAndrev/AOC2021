using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace AOC2021
{
    class Day08
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day08.txt").ToList();
       
        public static string Run()
        {
            Tuple<int, int> solution = PartTwo();
            int solution1 = solution.Item1;
            int solution2 = solution.Item2;

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static Tuple<int, int> PartTwo()
        {
            int sumPart1 = 0;
            int sumPart2 = 0;

            foreach (string item in fileInputList)
            {
                List<string> tempList = new List<string>(item.Split(new[] { ' ', '|' }, StringSplitOptions.RemoveEmptyEntries));
                tempList.RemoveAll(string.IsNullOrEmpty);

                for (int i = 0; i < tempList.Count; i++)
                {
                    char[] codeArray = tempList[i].ToCharArray();
                    Array.Sort(codeArray);
                    tempList[i] = new string(codeArray);
                }

                Dictionary<string, int> digitsString = new Dictionary<string, int>();
                Dictionary<int, string> digitsInt = new Dictionary<int, string>();
                for (int j = 0; j < tempList.Count-4; j++)
                {
                    switch (tempList[j].Length)
                    {
                        case 2:
                            digitsString.Add(tempList[j], 1);
                            digitsInt.Add(1, tempList[j]);
                            break;
                        case 3:
                            digitsString.Add(tempList[j], 7);
                            digitsInt.Add(7, tempList[j]);
                            break;
                        case 4:
                            digitsString.Add(tempList[j], 4);
                            digitsInt.Add(4, tempList[j]);
                            break;
                        case 7:
                            digitsString.Add(tempList[j], 8);
                            digitsInt.Add(8, tempList[j]);
                            break;
                    }
                }
                /* 
                 * 6 = (6') contains (8-7) 
                 * 2 = (5') contains (6-4)
                 * 5 = (5') contains (8-2)
                 * 3 = (5') contains 1
                 * 9 = (6') contains 3
                 * 0 not in dictionary
                * */

                string bdeg = GetUncommonLetters(digitsInt[8], digitsInt[7]);
                string six = FindStringWithSubstring(tempList, 6, bdeg);
                digitsInt.Add(6, six);
                digitsString.Add(six, 6);

                string aceg = GetUncommonLetters(digitsInt[6], digitsInt[4]);
                string two = FindStringWithSubstring(tempList, 5, aceg);
                digitsInt.Add(2, two);
                digitsString.Add(two, 2);

                string bcf = GetUncommonLetters(digitsInt[8], digitsInt[2]);
                string five = FindStringWithSubstring(tempList, 5, bcf);
                digitsInt.Add(5, five);
                digitsString.Add(five, 5);

                string three = FindStringWithSubstring(tempList, 5, digitsInt[1]);
                digitsInt.Add(3, three);
                digitsString.Add(three, 3);

                string nine = FindStringWithSubstring(tempList, 6, digitsInt[3]);
                digitsInt.Add(9, nine);
                digitsString.Add(nine, 9);

                string zero = FindStringNotInDictionary(tempList, digitsString, 6);
                digitsInt.Add(0, zero);
                digitsString.Add(zero, 0);

                for (int j = tempList.Count-1; j > tempList.Count-5; j--)
                {
                    int powValue = tempList.Count - (j+1);
                    int decyfered = digitsString[tempList[j]];
                    sumPart2 += decyfered * (int)Math.Pow(10, powValue);

                    if (decyfered == 1 || decyfered == 4 || decyfered == 7 || decyfered == 8)
                        sumPart1++;
                }
            }
            
            return Tuple.Create(sumPart1, sumPart2);
        }
        private static string FindStringWithSubstring(List<string> stringList, int targetLength, string substringToCheck)
        {
            return stringList.FirstOrDefault(s => s.Length == targetLength && substringToCheck.All(s.Contains));
        }
        private static string GetUncommonLetters(string firstString, string secondString)
        {
            var firstSet = new HashSet<char>(firstString);
            var secondSet = new HashSet<char>(secondString); 
            var uncommonSet = new HashSet<char>(firstSet);
            uncommonSet.SymmetricExceptWith(secondSet);
            string uncommonChars = new string(uncommonSet.ToArray());

            return uncommonChars;
        }

        private static string FindStringNotInDictionary(List<string> stringList, Dictionary<string, int> dictionary, int targetLength)
        {
            var filteredList = stringList.Where(s => s.Length == targetLength);
            string notInDictionary = filteredList.FirstOrDefault(s => !dictionary.ContainsKey(s));

            return notInDictionary;
        }
    }
}
