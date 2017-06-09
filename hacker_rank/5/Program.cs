using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _5
{
    class Program
    {
        static void Main(string[] args)
        {
            //int newIdx;
            //var outcome = GetLargestSubstring("bcdefgabcde", "bcdefgabcdeabcdefg", 11, out newIdx);
            //Console.WriteLine(outcome);
            //Console.WriteLine(newIdx);
            //return;
            var testCasesNum = int.Parse(Console.ReadLine());
            int[][] prices = new int[testCasesNum][];
            string[] strs = new string[testCasesNum];
            for (int i = 0; i < testCasesNum; i++)
            {
                prices[i] = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                strs[i] = Console.ReadLine().Substring(0, prices[i][0]);
            }

            for (int i = 0; i < testCasesNum; i++)
                CalculatePrice(ref prices, ref strs, i);
        }

        private static void CalculatePrice(ref int[][] prices, ref string[] strs, int index)
        {
            var minPrice = 0;
            var currentStr = string.Empty;
            int i = 0, j = 0;
            while (currentStr != strs[index])
            {
                if (currentStr == string.Empty)
                {
                    currentStr += strs[index][i++];
                    minPrice += prices[index][1];
                }
                else
                {
                    int newIndex = i;
                    var subStr = GetLargestSubstring(currentStr, strs[index], i, out newIndex);

                    bool canCopy = false;
                    j = 0;
                    do
                    {
                        
                        if (currentStr.Length - j >= 1 && (currentStr.Length - j + i) <= strs[index].Length 
                            && currentStr.Contains(strs[index].Substring(i, currentStr.Length - j))
                            && (currentStr.Length - j) * prices[index][1] > prices[index][2])
                        {
                            canCopy = true;
                            currentStr += strs[index].Substring(i, currentStr.Length - j);
                            i = currentStr.Length;
                            minPrice += prices[index][2];
                        }
                        else
                            j++;
                    } while (j < currentStr.Length - 1);

                    if (!canCopy)
                    {
                        currentStr += strs[index][i++];
                        minPrice += prices[index][1];
                    }
                }
            }

            Console.WriteLine(minPrice);
        }

        private static string GetLargestSubstring(string currentStr, string mainStr, int i, out int newIndex)
        {
            var j = 0;
            var maxSubStr = string.Empty;
            string subStr;
            newIndex = i;

            while (mainStr.Length - i >maxSubStr.Length)
            {
                j = 0;
                while (j < currentStr.Length - 1)
                {
                    subStr = mainStr.Substring(i, mainStr.Length - j - i);
                    if (currentStr.Contains(subStr))
                        if (maxSubStr.Length < subStr.Length)
                        {
                            maxSubStr = subStr;
                            newIndex = i;
                        }
                        else break;
                    else j++;
                }
                i++;
            }
            return maxSubStr;
        }

    }
}
