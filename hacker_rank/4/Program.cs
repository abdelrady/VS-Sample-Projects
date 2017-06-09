using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4
{
    class Program
    {
        private static int RR, RB, BB, BR;
        static int i_RR = 0, i_RB = 0, i_BB = 0, i_BR = 0;
        static void Main(string[] args)
        {
            string[] tokens_A = Console.ReadLine().Split(' ');
            //RR
            RR = Convert.ToInt32(tokens_A[0]);
            //RB
            RB = Convert.ToInt32(tokens_A[1]);
            //BB
            BB = Convert.ToInt32(tokens_A[2]);
            //BR
            BR = Convert.ToInt32(tokens_A[3]);

            decimal num = 0;

            FormulateFlowers("RR", ref num);
            FormulateFlowers("RB", ref num);
            FormulateFlowers("BR", ref num);
            FormulateFlowers("BB", ref num);
            Console.WriteLine(num);
        }

        private static void FormulateFlowers(string str, ref decimal num)
        {
            var result = TestPattern(str);
            if (result.HasValue && result.Value)
            {
                num += 1;
                if (num > (decimal)(Math.Pow(10, 9) + 7)) num = num % (decimal)(Math.Pow(10, 9) + 7);
                return;
            }
            else if (!result.HasValue) return;
            else
            {
                FormulateFlowers(str + "R", ref num);
                FormulateFlowers(str + "B", ref num);
            }
        }

        private static bool? TestPattern(string str)
        {
            i_RR = 0;
            i_RB = 0;
            i_BB = 0;
            i_BR = 0;
            for (int i = 0; i < str.Length - 1; i++)
            {
                if (str[i] == str[i + 1] && str[i] == 'R')
                    i_RR += 1;
                else if (str[i] == str[i + 1] && str[i] == 'B')
                    i_BB += 1;
                else if (str[i] == 'R' && str[i + 1] == 'B')
                    i_RB += 1;
                else if (str[i] == 'B' && str[i + 1] == 'R')
                    i_BR += 1;
            }
            if (i_RR > RR || i_RB > RB || i_BR > BR || i_BB > BB)
                return null;
            if (i_RR == RR && i_RB == RB && i_BR == BR && i_BB == BB)
                return true;
            return false;
        }

    }
}
