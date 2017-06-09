using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace ConsoleApplication3
{
    class Program
    {
        static void Main(string[] args)
        {

            // Get the file version for the notepad.
            var dllPath =
                @"D:\Intel_Machine_26_06_2014\PAS_System\GIT_Master\pas-pas\Src\Master\Pas.WebSite\bin\pas.website.dll";
            FileVersionInfo myFileVersionInfo = FileVersionInfo.GetVersionInfo(dllPath);

            // Print the file name and version number.
            Console.WriteLine("File: " + myFileVersionInfo.FileDescription + '\n' +
                              "Version number: " + myFileVersionInfo.FileVersion);


            return;

            //Console.WriteLine(solution22(5, new int[] { 5, 5, 1, 7, 2, 3, 5 }));
            //Console.WriteLine(solution22(5, new int[] { 5 }));
            //return;

            //solution2(new int[] { -1, 3, -4, 5, 1, -6, 2, 1 }).ToList().ForEach(Console.WriteLine);
            //Console.WriteLine(solution(new int[] {-1, 3, -4, 5, 1, -6, 2, 1}));

            //Console.WriteLine(negaternary(-9));
            //Console.WriteLine(negaternary(23));
            //Console.WriteLine(negaternary(-1 * getEquivalent(new[] { 1, 0, 0, 1, 1 })));

            //solution(new int[] { }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 0 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 1 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 0, 0, 1 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 0, 1, 1 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 1, 0, 0, 1, 1 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //solution(new[] { 1, 0, 0, 1, 1, 1 }).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //return;

            //negaternary(-1 * getEquivalent(new[] { 1, 0, 0, 1, 1 })).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //negaternary(-1 * getEquivalent(new[] { 1, 0, 0, 1, 1, 1 })).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //negaternary2(-1 * getEquivalent(new[] { 1, 0, 0, 1, 1 })).ToList().ForEach(Console.Write);
            //Console.WriteLine();
            //negaternary2(-1 * getEquivalent(new[] { 1, 0, 0, 1, 1, 1 })).ToList().ForEach(Console.Write);
            //Console.WriteLine();

            Console.WriteLine("Moves from (0,0) to (4,5): " + knightMoves(4, 5));
        }

        static int[] solution(int[] A)
        {
            double decimalNumber = 0;
            List<int> results = new List<int>();
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 1)
                    decimalNumber += (A[i] * Math.Pow(-2, i));
            }
            var decimalNumberInt = -1 * (int)decimalNumber;
            if (decimalNumberInt == 0)
            {
                results.Add(0);
                return results.ToArray();
            }
            while (decimalNumberInt != 0)
            {
                int remainder = decimalNumberInt % -2;
                decimalNumberInt = decimalNumberInt / -2;

                if (remainder < 0)
                {
                    remainder += 2;
                    decimalNumberInt += 1;
                }

                results.Add(remainder);
            }
            return results.ToArray();
        }

        static int solution22(int X, int[] A)
        {
            if (A.Length <= 1)
                return 0;

            int xEquals = 0, xNonEquals = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == X)
                {
                    xEquals += 1;
                    A[i] = xEquals;
                }
                else A[i] = -1;
            }

            for (int i = A.Length - 1; i >= 0; i--)
            {
                if (A[i] != -1) xEquals = A[i] - 1;
                else xNonEquals += 1;

                if (xNonEquals == xEquals)
                    return i;
            }
            return 0;
        }

        static IEnumerable<int> solution2(int[] A)
        {
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int sumAfter = 0, sumBefore = 0;

            for (int i = 1; i < A.Length; i++)
            {
                sumAfter += A[i];
            }

            for (int i = 1; i < A.Length; i++)
            {
                sumBefore += A[i - 1];
                sumAfter -= A[i];
                if (sumAfter == sumBefore)
                    yield return i;
            }

            yield return -1;

        }

        static int solution55(int[] A)
        {
            if (A.Length == 0)
                return -1;
            if (A.Length == 1)
                return 0;
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            decimal sumAfter = 0, sumBefore = 0;

            for (int i = 1; i < A.Length; i++)
            {
                sumAfter += A[i];
            }
            if (sumAfter == 0)
                return 0;
            for (int i = 1; i < A.Length; i++)
            {
                sumBefore += A[i - 1];
                sumAfter -= A[i];
                if (sumAfter == sumBefore)
                    return i;
            }

            return -1;

        }


        static int solution3(int[] A)
        {
            if (A.Length == 0)
                return -1;
            if (A.Length == 1)
                return 0;

            return -1;
        }

        //failed
        static int solution4(int[] A)
        {
            if (A.Length == 0)
                return -1;
            if (A.Length == 1)
                return 0;

            decimal sum = 0;

            for (int i = 0; i < A.Length; i++)
            {
                sum += A[i];
            }
            if (sum == 0)
                return 0;
            for (int i = 1; i < A.Length; i++)
            {
                sum -= A[i];
                decimal x = sum / 2;
                if ((sum - (int)x) == (int)x)
                    return i;
            }

            return -1;

        }

        static int getEquivalent(int[] A)
        {
            double sum = 0;
            for (int i = 0; i < A.Length; i++)
            {
                if (A[i] == 1)
                    sum += (A[i] * Math.Pow(-2, i));
            }
            return (int)sum;
        }

        static int[] negaternary(int value)
        {
            string result = string.Empty;

            while (value != 0)
            {
                int remainder = value % -2;
                value = value / -2;

                if (remainder < 0)
                {
                    remainder += 2;
                    value += 1;
                }

                result = result + remainder.ToString();
            }
            //return result;
            return result.ToCharArray().Select(x => (int)x - 48).ToArray();
        }

        static int[] negaternary2(int value)
        {
            List<int> results = new List<int>();

            while (value != 0)
            {
                int remainder = value % -2;
                value = value / -2;

                if (remainder < 0)
                {
                    remainder += 2;
                    value += 1;
                }

                results.Add(remainder);
            }
            return results.ToArray();
        }

        static int knightMoves(int A, int B)
        {
            if ((A + B) % 3 == 0)
                return (A + B) / 3;
            
            int temp;
            if (A < 0) A = -1 * A;
            if (B < 0) B = -1 * B;
            if (A > B)
            {
                temp = A;
                A = B;
                B = temp;
            }
            if (B == (2 * A)) return A;
            if (A == B)
            {
                if (A % 3 == 0) return 2 * (A / 3);
                if (A % 3 == 1) return 2 + (2 * (A - 1) / 3);
                if (A % 3 == 2) return 4 + (2 * (A - 2) / 3);
            }
            if (A == 0)
            {
                if (B % 4 == 0) return B / 2;
                if (B % 4 == 1 || B % 4 == 3) return 3 + ((B - (B % 4)) / 2);
                if (B % 4 == 2) return 2 + ((B - 2) / 2);
            }
            if (B > (2 * A))
                return knightMoves((2 * A) - B, 2 * A - B) + knightMoves(B - A, 2 * (B - A));
            else return knightMoves(0, B - (2 * A)) + knightMoves(A, 2 * A);
        }
    }
}
