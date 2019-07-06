using System;

namespace MerryYellow.RoslynWeb
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var rw = Compiler.GetClassList("");
        }
    }
}
