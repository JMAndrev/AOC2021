using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace AOC2021
{
    class Day03
    {
        private static List<string> fileInputList = File.ReadAllLines(@"Day03.txt").ToList();
        public static string Run()
        {
            int solution1 = Part1();
            int solution2 = Part2();

            return ("part 1: " + solution1 + " ; " + "part2: " + solution2);
        }

        private static int Part1()
        {
            string gammaRate = "";
            string epsilonRate = "";
            int bitLength = fileInputList[0].Length;
            int count0 = 0;
            int count1 = 0;

            for (int i = 0; i < bitLength; i++)
            {
                count0 = fileInputList.Count(x => x[i] == '0');
                count1 = fileInputList.Count(x => x[i] == '1');

                gammaRate += (count0 > count1) ? "0" : "1";
                epsilonRate += (count0 > count1) ? "1" : "0";
            }

            return Convert.ToInt32(gammaRate, 2) * Convert.ToInt32(epsilonRate, 2);
        }
        private static int Part2()
        {
            int bitLength = fileInputList[0].Length;
            List<string> oxygenList = fileInputList.ToList();
            List<string> CO2List = fileInputList.ToList();

            for (int i = 0; i < bitLength; i++)
            {
                int O2count0 = oxygenList.Count(x => x[i] == '0');
                int O2count1 = oxygenList.Count(x => x[i] == '1');
                int CO2count0 = CO2List.Count(x => x[i] == '0');
                int CO2count1 = CO2List.Count(x => x[i] == '1');

                string frequentValueCO2 = (CO2count0 > CO2count1) ? "0" : "1";
                string frequentValueO2 = (O2count0 > O2count1) ? "0" : "1";

                if (frequentValueO2 == "0" && oxygenList.Count > 1)
                {
                    oxygenList = RemoveElement(oxygenList, i, '1');
                }
                if (frequentValueO2 == "1" && oxygenList.Count > 1)
                {
                    oxygenList = RemoveElement(oxygenList, i, '0');
                }
                if (frequentValueCO2 == "0" && CO2List.Count > 1)
                {
                    CO2List = RemoveElement(CO2List, i, '0');
                }
                if (frequentValueCO2 == "1" && CO2List.Count > 1)
                {
                    CO2List = RemoveElement(CO2List, i, '1');
                }
            }
            
            return Convert.ToInt32(oxygenList[0],2) * Convert.ToInt32(CO2List[0],2);
        }
        private static List<string> RemoveElement(List<string> checkedList, int bitPlace, char element)
        {
            for (int i = checkedList.Count - 1; i >= 0; i--)
            {
                string line = checkedList[i];
                char symbol = line[bitPlace];
                if (symbol == element && checkedList.Count > 1)
                {
                    checkedList.RemoveAt(i);
                }
            }
            
            return checkedList;
        }
    }
}
