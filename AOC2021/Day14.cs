using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.IO;

namespace AOC2021
{
    class Day14
    {
        private static Dictionary<string, string> FormulaMapDictionary;
        private static Dictionary<string, BigInteger> CurrentMapDictionary;
        private static Dictionary<char, BigInteger> SingleLettersDictionary;
        private static List<string> fileInputList = File.ReadAllLines(@"Day14.txt").ToList();

        public static string Run()
        {
            FormulaMapDictionary = new Dictionary<string, string>();
            CurrentMapDictionary = new Dictionary<string, BigInteger>();
            SingleLettersDictionary = new Dictionary<char, BigInteger>();
            string formulaString = fileInputList[0];

            PrepareData();

            var inputStringList = PrepareFormula(formulaString);
            BigInteger solution2 = CalculatePolymer(inputStringList, 40);

            return ("part 2: " + solution2);
        
        }
        private static BigInteger CalculatePolymer(List<string> inputString, int numberOfTimes)
        {
                for (int i = 0; i < numberOfTimes; i++)
                {
                    Dictionary<string, BigInteger> tempFormula = new Dictionary<string, BigInteger>();
                    for (int j = 0; j < inputString.Count; j++)
                    {
                        List<string> tempList = inputString[j].Select(c => c.ToString()).ToList();
                        BigInteger numOfTimes = CurrentMapDictionary[inputString[j]];
                        var secondLetter = FormulaMapDictionary[inputString[j]];
                        var letter2 = secondLetter.ToCharArray();
                        BigInteger num2 = SingleLettersDictionary[letter2[0]];
                        SingleLettersDictionary[letter2[0]] += numOfTimes;

                        var newKey1 = string.Concat(tempList[0], secondLetter);
                        var newKey2 = string.Concat(secondLetter, tempList[1]);
                        BigInteger occur = CurrentMapDictionary[inputString[j]];
                        CurrentMapDictionary[inputString[j]] = BigInteger.Zero;

                        if (!tempFormula.ContainsKey(newKey1))
                            tempFormula.Add(newKey1, occur);
                        else
                        {
                            tempFormula[newKey1] += occur;
                            tempFormula[newKey1] = tempFormula[newKey1];
                        }
                        if (!tempFormula.ContainsKey(newKey2))
                            tempFormula.Add(newKey2, occur);
                        else
                        {
                            tempFormula[newKey2] += occur;
                            tempFormula[newKey2] = tempFormula[newKey2];
                        }
                    }

                    inputString.Clear();
                    inputString.AddRange(tempFormula.Keys);
                    foreach (var entry in tempFormula)
                    {
                        string tempKey = entry.Key;
                        CurrentMapDictionary[tempKey] = tempFormula[tempKey];
                    }
                }

                var maxKey = SingleLettersDictionary.First(x => x.Value == SingleLettersDictionary.Values.Max()).Key;
                var minKey = SingleLettersDictionary.First(x => x.Value == SingleLettersDictionary.Values.Min()).Key;
                BigInteger valueMax = SingleLettersDictionary[maxKey];
                BigInteger valueMin = SingleLettersDictionary[minKey];
                BigInteger sub = valueMax - valueMin;
            return sub;
        }

        private static void PrepareData()
        {
            for (int i = 2; i < fileInputList.Count; i++)
            {
                string[] parts = fileInputList[i].Split(new string[] { " -> " }, StringSplitOptions.None);
                var part0 = parts[0];
                var part1 = parts[1];
                FormulaMapDictionary.Add(parts[0], parts[1]);
                CurrentMapDictionary.Add(parts[0], BigInteger.Zero);
                var singlesArray = parts[0].ToString().ToCharArray();
                if (!SingleLettersDictionary.ContainsKey(singlesArray[0]))
                    SingleLettersDictionary[singlesArray[0]] = BigInteger.Zero;
                if (!SingleLettersDictionary.ContainsKey(singlesArray[1]))
                    SingleLettersDictionary[singlesArray[1]] = BigInteger.Zero;
            }
        }

        private static List<string> PrepareFormula(string formulaString)
        {
            var charFormulaArray = formulaString.ToCharArray();
            var inputString = new List<string>();
            for (int i = 0; i < charFormulaArray.Length - 1; i++)
            {
                string key = string.Concat(charFormulaArray[i], charFormulaArray[i + 1]);
                if (i == 0)
                    SingleLettersDictionary[charFormulaArray[i]] += BigInteger.One;
                if (i != charFormulaArray.Length - 1)
                    SingleLettersDictionary[charFormulaArray[i + 1]] += BigInteger.One;
                inputString.Add(key);
                BigInteger occur = CurrentMapDictionary[key];
                CurrentMapDictionary[key] = occur + BigInteger.One;
            }
            return inputString;
        }
    }
}