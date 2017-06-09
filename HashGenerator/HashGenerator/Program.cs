using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HashidsNet;

namespace HashGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new HashService().Encode(1033));
            foreach (var i in new HashService().Decode(""))
            {
                Console.WriteLine(i);
            }
            string input = "";
            while ((input = Console.ReadLine()) != "q")
            {
                Console.WriteLine(new HashService().Encode(int.Parse(input)));
            }
        }
    }

    public class HashService
    {
        // I think this is thread-safe but not positive
        private Hashids _hasher;

        public HashService(string salt = "SaltySailorSavesSingingSwordfish", int minHashLength = 8, string alphabet = null)
        {
            if (string.IsNullOrWhiteSpace(alphabet))
            {
                _hasher = new Hashids(salt, minHashLength);
            }
            else
            {
                _hasher = new Hashids(salt, minHashLength, alphabet: alphabet);
            }
        }

        /// <summary>
        /// Encodes the provided numbers into a hashed string
        /// </summary>
        /// <param name="numbers">the numbers to encode</param>
        /// <returns>the hashed string</returns>
        public string Encode(params int[] numbers)
        {
            return _hasher.Encode(numbers);
        }

        /// <summary>
        /// Encodes the provided numbers into a hashed string
        /// </summary>
        /// <param name="numbers">the numbers to encode</param>
        /// <returns>the hashed string</returns>
        public string Encode(IEnumerable<int> numbers)
        {
            return _hasher.Encode(numbers.ToArray());
        }

        /// <summary>
        /// Decodes the provided hash into
        /// </summary>
        public virtual int[] Decode(string hash)
        {
            return _hasher.Decode(hash);
        }
    }
}
