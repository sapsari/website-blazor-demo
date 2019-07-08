using System;
using System.Linq;

namespace MerryYellow.RoslynWeb
{
    class Program
    {
        const string Source = @"
using System;
namespace ConsoleApplication1
    {
        class Program
        {
            static void Main(string[] args)
            {
            }
        }
    }
";

    static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var pl = Compiler.GetPatternList();
            var cl = Compiler.GetClassList(Source);
            var ns = Compiler.ApplyPattern(Source, pl.ElementAt(0), cl.ElementAt(0));

            
        }
    }
}
