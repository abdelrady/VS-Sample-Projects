using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> stringList = new List<string>() { "Apple", "Banana", "Mango" };

            IEnumerable<object> list = stringList; //this is covariant cause IEnumerable<out T>


            //List<object> anotherlist = stringList;  //Not allowed, because List<T> is not covariant = invariant

  
            int T = Convert.ToInt32(Console.ReadLine());
            int[] N = new int[T];

            for (int a0 = 0; a0 < T; a0++)
            {
                N[a0] = Convert.ToInt32(Console.ReadLine());
            }
            for (int a0 = 0; a0 < T; a0++)
            {
                int a = 0, b = 0;
                CalcValidNumber(N[a0], ref a, ref b);
                Console.WriteLine(2*a+b);
            }
        }

        private static void CalcValidNumber(int num, ref int a, ref int b)
        {
            var notFound = true;
            var validNums = new List<int> {4};
            a = 1;
            b = 0;
            bool zeroTurn = true;
            int timesBy10=1, tempTimesByTen = 1;
            int i = 0;
            while (notFound)
            {
                if (validNums[i]%num == 0)
                    notFound = false;
                else
                {
                    if (tempTimesByTen>0)
                    {
                        validNums.Add(validNums[i] * 10);
                        b++;
                        tempTimesByTen--;
                    }
                    else
                    {
                        validNums.Add(validNums[i] + 4);
                        a++;
                        timesBy10 += 1;
                        tempTimesByTen = timesBy10;
                    }
                }
            }
        }

    }
}
