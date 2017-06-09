using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace deserializeDic_test
{
    class Program
    {
        static void Main(string[] args)
        {
            var str = "{1041: null, 1042: null}";
            var obj = JsonConvert.DeserializeObject<Dictionary<int, int?>>(str);

            Console.WriteLine(obj.ElementAt(0).Key);
        }
    }
}
