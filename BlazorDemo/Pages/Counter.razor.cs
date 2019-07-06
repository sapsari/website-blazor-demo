using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
//using MerryYellow.PatternMaker;

namespace BlazorDemo.Pages
{
    public class CounterModel : ComponentBase
    {
        public string Output = "HELELOY";

        public void Run()
        {
            Output += "Y";

            //var patterns = Maker.GetPatternList();
            //Output += string.Concat(patterns);

            var source = 
@"
using System;
 
public class MyClass
{
    public static void Main()
    {
    }
}
";

            Output += "<";


            try
            {

            var classList = MerryYellow.RoslynWeb.Compiler.GetClassList(source);
            Output += string.Concat(classList);
            }
            catch(Exception e)
            {
                Output += e.ToString();
            }

            Output += ">";

            //Maker.

            StateHasChanged();
        }


    }
}
