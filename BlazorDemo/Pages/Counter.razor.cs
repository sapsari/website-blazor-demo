using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MerryYellow.PatternMaker;

namespace BlazorDemo.Pages
{
    public class CounterModel : ComponentBase
    {
        public string Output = "HELELOY";

        public void Run()
        {
            Output += "Y";

            var patterns = Maker.GetPatternList();

            Output += string.Concat(patterns);

            StateHasChanged();
        }


    }
}
