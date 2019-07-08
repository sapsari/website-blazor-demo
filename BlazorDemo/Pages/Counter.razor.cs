using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace MerryYellow.BlazorDemo.Pages
{

    public class CounterModel : ComponentBase
    {
        public string StatusText = "All ready";
        //public string StatusType = ""

        public string Output = "HELELOY";

        public string Source;
        public string SelectedPattern;
        public string SelectedClass;

        public List<string> patternList;
        public List<string> classList;

        public Func<string> getSource;

        public IEnumerable<string> GetPatternList()
        {
            try
            {
                if (patternList != null)
                    return patternList;
                patternList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetPatternList());
                this.SelectedPattern = patternList.ElementAtOrDefault(0);
                return patternList;
            }
            catch(Exception e)
            {
                StatusText = e.ToString();
                patternList = null;
                return new List<string>();
            }
        }

        public IEnumerable<string> GetClassList()
        {
            try
            {
                //var source = this.getSource?.Invoke();
                //var source = "";
                var source = Source;

                if (string.IsNullOrEmpty(source))
                    classList = new List<string>();
                else
                    classList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetClassList(source));

                SelectedClass = classList.ElementAtOrDefault(0);
                return classList;
            }
            catch (Exception e)
            {
                StatusText = e.ToString();
                classList = null;
                return new List<string>();
            }

            //this.classList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetClassList(source));
        }


        Guid lastSourceUpdateGuid;
        public async Task OnSourceChangedAsync()
        {
            var guid = Guid.NewGuid();
            this.lastSourceUpdateGuid = guid;
            await Task.Delay(3000);

            if (guid == lastSourceUpdateGuid)
                await ApplyPatternAsync();
        }

        public async Task ApplyPatternAsync()
        {
            //MerryYellow.RoslynWeb.Compiler.ApplyPattern()
            StatusText += "PATTERN APPLIED";
            //StatusText += "{" + SelectedPattern + "," + SelectedClass + "}";

            try
            {
                MerryYellow.RoslynWeb.Compiler.OnLogged += Compiler_OnLogged;
                var newSource = Source;
                if (!string.IsNullOrEmpty(Source) && !string.IsNullOrEmpty(SelectedPattern) && !string.IsNullOrEmpty(SelectedClass))
                    newSource = MerryYellow.RoslynWeb.Compiler.ApplyPattern(Source, SelectedPattern, SelectedClass);
            }
            catch (Exception e)
            {
                StatusText = e.ToString();
            }


            StateHasChanged();

            //return Task.FromResult()
        }

        private void Compiler_OnLogged(int level, string message)
        {
            //throw new NotImplementedException();
            StatusText += message;
            StateHasChanged();
        }

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
