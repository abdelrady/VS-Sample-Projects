using System;
using System.Collections.Generic;
using System.Linq;

namespace hacker_rank
{
    class Program
    {
        static void Main(string[] args)
        {
            //char[] messageCharArray = {'S', 'O', 'S'};

            //string S = Console.ReadLine();
            //int changedLetters = 0;

            //for (int i = 0; i < S.Length; i++)
            //    if (S[i] != messageCharArray[i%3])
            //        changedLetters += 1;
            //Console.WriteLine(changedLetters);


            //string[] tokens_n = Console.ReadLine().Split(' ');
            //int n = Convert.ToInt32(tokens_n[0]);
            //int m = Convert.ToInt32(tokens_n[1]);
            //string[] c_temp = Console.ReadLine().Split(' ');
            //int[] c = Array.ConvertAll(c_temp, Int32.Parse);

            //int maxNearDistance = 0;
            //var nearestLength = n;
            //if (n == m)
            //{
            //    Console.WriteLine(maxNearDistance.ToString());
            //    return;
            //}
            //c = c.OrderBy(x => x).ToArray();

            //var maxDist = c[0] > Math.Abs(c[c.Length - 1] - n) ? c[0] : Math.Abs(c[c.Length - 1] - n);
            //for (int i = 0; i < c.Length; i++)
            //{
            //    if(c[i+1] - c[i] > maxDist)
            //        maxDist = 
            //}


            //int maxNearDistance = 0;
            //var nearestLength = n;
            //if (n == m)
            //{
            //    Console.WriteLine(maxNearDistance.ToString());
            //    return;
            //}
            //c = c.OrderBy(x => x).ToArray();

            //for (int i = 0; i < n; i++)
            //{
            //    nearestLength = n;
            //    if (Array.BinarySearch(c, i) >= 0)
            //        continue;
            //    for (int j = 0; j < c.Length; j++)
            //    {
            //        if (Math.Abs(i - c[j]) < nearestLength)
            //            nearestLength = Math.Abs(i - c[j]);
            //        if (nearestLength == 1)
            //            break;
            //    }

            //    maxNearDistance = (maxNearDistance < nearestLength) ? nearestLength : maxNearDistance;
            //}

            //Console.WriteLine(maxNearDistance);


            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            char[][] arr = new char[n][];

            for (int i = 0; i < n; i++)
                arr[i] = Console.ReadLine().ToArray();

            var foundPlus = false;
            var firstPlusArea = 1;
            var secondPlusArea = 1;
            int area;
            var overlappedList = new List<int[]>();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    //if (foundPlus || arr[i][j] == 'G')
                    //    foundPlus = true;

                    if (arr[i][j] == 'G' && GetMaxPlus(arr, i, j, 1, ref overlappedList, out area))
                    {
                        if (area > secondPlusArea)
                            secondPlusArea = area;
                        else if (area > firstPlusArea)
                            firstPlusArea = area;
                    }
                }
            }

            Console.WriteLine(firstPlusArea * secondPlusArea);
        }

        private static bool GetMaxPlus(char[][] arr, int i, int j, int depth, ref List<int[]> overlappedList, out int area)
        {
            foreach (var array in overlappedList)
                if (HasOverlap(array, i, j) || HasOverlap(array, i, j + depth) || HasOverlap(array, i, j - depth) ||
                    HasOverlap(array, i + depth, j) || HasOverlap(array, i - depth, j))
                {
                    area = 1;
                    return false;
                }
            if (i + depth < arr.Length && i - depth >= 0 && j + depth < arr[0].Length && j - depth >= 0)
                if (arr[i][j - depth] == arr[i][j + depth] && arr[i - depth][j] == arr[i + depth][j] &&
                    arr[i][j + depth] == 'G' && arr[i + depth][j] == 'G')
                {
                    area = 1 + depth * 4;
                    int inDeptharea;
                    if (GetMaxPlus(arr, i, j, depth + 1, ref overlappedList, out inDeptharea)) ;
                    area = inDeptharea > area ? inDeptharea : area;
                    overlappedList.Add(new int[] { i, j, depth });
                    return true;
                }
            area = 1;
            return false;
        }

        private static bool HasOverlap(int[] array, int i, int j)
        {
            if(GenerateOverlapList(array).Any(x=>x[0]==i && x[1] == j))
                return true;
            return false;
        }

        private static IEnumerable<int[]> GenerateOverlapList(int[] array)
        {
            //yield return new int[] {array[0], array[1]};

            for (int i = array[0]-array[2]; i <= array[0]+array[2]; i++)
                yield return new int[] { i, array[1] };

            for (int j = array[1] - array[2]; j <= array[1] + array[2]; j++)
                yield return new int[] { array[0], j };
        }
    }
}
