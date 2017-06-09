using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication2
{
    class Program
    {
        private static List<List<byte>> arr;
        private static int index = 0, globalNumOfDisgits = 5;

        static void Main()
        {
            var numOfChars = 4;
            arr = new List<List<byte>>();

            permutations("", globalNumOfDisgits, numOfChars);

            //PrintArray(arr);

            var shapesList = Step2(arr);

            //PrintArray(shapesList);

            var transformedArray = TransformArray(shapesList);

            PrintArray(transformedArray);
        }

        private static List<List<byte>> TransformArray(List<List<byte>> shapesList)
        {
            var result = new List<List<byte>>();
            foreach (var shape in shapesList)
            {
                var distinctValues = shape.Distinct().Count();
                switch (distinctValues)
                {
                    case 1:
                        result.Add(new List<byte> { 0, 0, 0, 0 });
                        break;
                    case 2:
                        var zeroExist = shape.Contains(0);
                        var oneExist = shape.Contains(1);
                        List<byte> item;
                        if (zeroExist == false && oneExist == false)
                            item = shape.Select(x => x == 2 ? (byte)0 : (byte)1).ToList();
                        else if (zeroExist && oneExist)
                            item = shape;
                        else
                        {
                            item = zeroExist
                                ? shape.Select(x => x == 0 ? (byte) 0 : (byte) 1).ToList()
                                : shape.Select(x => x == 1 ? (byte) 1 : (byte) 0).ToList();
                        }
                        result.Add(item);
                        break;
                    case 3:
                        result.Add(shape.Select(x => x == 3 ? (byte) 2 : x).ToList());
                        break;
                    case 4:
                        result.Add(shape);
                        break;
                }
            }
            return result;
        }

        private static void PrintArray(List<List<byte>> list)
        {
            foreach (var a in list)
            {
                foreach (var b in a)
                    Console.Write(b);
                Console.WriteLine();
            }
        }

        private static List<List<byte>> Step2(List<List<byte>> list)
        {
            List<string> shapes = new List<string>();

            foreach (var a in list)
            {
                var min = FindMin(a);
                var shape = SubtractArray(a, min);
                if (!shapes.Contains(shape))
                    shapes.Add(shape);
            }

            return shapes.Select(s => new List<byte>(s.ToCharArray().Select(b => byte.Parse(b.ToString())))).ToList();
        }

        private static string SubtractArray(List<byte> bytes, byte min)
        {
            var result = "";
            foreach (var b in bytes)
            {
                result += b - min;
            }
            return result;
        }

        private static byte FindMin(List<byte> a)
        {
            var min = a[0];
            foreach (var b in a)
            {
                if (b < min)
                    min = b;
            }
            return min;
        }

        static void permutations(string text, int numberOfDigits, int numberOfChars)
        {
            if (numberOfDigits > 0)
                for (int j = 0; j < numberOfChars; j++)
                    permutations(text + j.ToString(), numberOfDigits - 1, numberOfChars);
            else
            {
                arr.Add(text.ToCharArray().Select(x => byte.Parse(x.ToString())).ToList());
            }
        }

    }
}
