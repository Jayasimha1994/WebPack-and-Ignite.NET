using Apache.Ignite.Core;
using Apache.Ignite.Core.Compute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgniteProjectNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Compute();
            Console.ReadKey();
        }
        static void Compute()
        {
            try
            {
                using (var ignite = Ignition.Start())
                {
                    var funcs = "Count characters using callable".Split(' ')
                      .Select(word => new ComputeFunc { Word = word });

                    ICollection<int> res = ignite.GetCompute().Call(funcs);

                    var sum = res.Sum();
                    Console.WriteLine("Working");
                    Console.WriteLine(">>> Total number of characters in the phrase is '{0}'.", sum);
                }
            }
            catch (Exception e) { Console.WriteLine(e.Message); }
        }

        class ComputeFunc : IComputeFunc<int>
        {
            public string Word { get; set; }

            public int Invoke()
            {
                return Word.Length;
            }
        }
    }
}
