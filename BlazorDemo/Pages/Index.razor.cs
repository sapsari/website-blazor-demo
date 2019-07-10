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
        [Inject]
        protected Microsoft.JSInterop.IJSRuntime JsRuntime { get; set; }

        [Inject]
        protected HttpClient Http { get; set; }

        public bool IsInitialized;

        public LogType StatusType = LogType.Standard;
        public string StatusText = "Initializing...";
        public string DebugText = "DebugLine";


        string _selectedPattern, _selectedClass;
        public string SelectedPattern
        {
            get => _selectedPattern;
            set
            {
                if (_selectedPattern != value)
                {
                    _selectedPattern = value;
                    if (IsInitialized) ApplyPatternAsync(); // do NOT await
                }
            }
        }
        public string SelectedClass
        {
            get => _selectedClass;
            set
            {
                if (_selectedClass != value)
                {
                    _selectedClass = value;
                    if (IsInitialized) ApplyPatternAsync(); // do NOT await
                }
            }
        }

        bool internalSet;

        List<string> _patternList;
        public List<string> _classList = new List<string>();

        public string Source;

        

        public IEnumerable<string> GetPatternList()
        {
            try
            {
                if (_patternList != null)
                    return _patternList;
                _patternList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetPatternList());
                this.SelectedPattern = _patternList.ElementAtOrDefault(0);
                return _patternList;
            }
            catch(Exception e)
            {
                Log(e);
                _patternList = null;
                return new List<string>();
            }
        }

        public IEnumerable<string> GetClassList()
        {
            try
            {
                /*if (!IsInitialized)
                    return new List<string>();
                if (_classList != null)
                    return _classList;*/

                //var source = this.getSource?.Invoke();
                //var source = "";
                //var source = this.JS_GetSourceAsync().GetAwaiter().GetResult();
                var source = Source;

                if (string.IsNullOrEmpty(source))
                    _classList = new List<string>();
                else
                    _classList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetClassList(source));

                //DebugText = $"_classList.Count:{_classList.Count} from source {source}";

                if (!string.IsNullOrEmpty(SelectedClass) && _classList.Contains(SelectedClass))
                    ;//no-op
                else
                    _selectedClass = _classList.ElementAtOrDefault(0);

                return _classList;
            }
            catch (Exception e)
            {
                Log(e);
                _classList = null;
                return new List<string>();
            }

            //this.classList = new List<string>(MerryYellow.RoslynWeb.Compiler.GetClassList(source));
        }


        Guid lastSourceUpdateGuid;
        public async Task OnSourceChangedAsync()
        {
            Log("Will apply pattern in seconds");

            var guid = Guid.NewGuid();
            this.lastSourceUpdateGuid = guid;
            await Task.Delay(3000);

            if (guid == lastSourceUpdateGuid)
            {
                ResetLog();

                this.Source = await JS_GetSourceAsync();
                //this._classList = null;
                GetClassList(); // needed for renaming class
                await ApplyPatternAsync();
            }
        }

        static int counter = 1;

        public async Task ApplyPatternAsync()
        {
            //MerryYellow.RoslynWeb.Compiler.ApplyPattern()
            //StatusText += "PATTERN APPLIED";
            //StatusText += "{" + SelectedPattern + "," + SelectedClass + "}";

            //var source = await this.JS_GetSourceAsync();
            var source = Source;
            var modifiedSource = string.Empty;

            Log($"Pattern {SelectedPattern} applied over class {SelectedClass}");
            //StatusText = $"Pattern {SelectedPattern} applied over class {SelectedClass} {counter++} {_classList?.Count}";

            try
            {
                if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(SelectedPattern) && !string.IsNullOrEmpty(SelectedClass))
                    modifiedSource = MerryYellow.RoslynWeb.Compiler.ApplyPattern(source, SelectedPattern, SelectedClass);
            }
            catch (Exception e)
            {
                Log(e);
            }


            await JS_SetSourceAsync(modifiedSource);

            StateHasChanged();

            // for updating select properly (so that currently selected item will continue to be selected after the list changes)
            SelectedClass = SelectedClass;
            //DebugText = "SC: " + SelectedClass;
            StateHasChanged();

            //return Task.FromResult()
        }

        static int counterSource = 1;
        public async Task<string> JS_GetSourceAsync()
        {
            try
            {
                var source = await JsRuntime.InvokeAsync<string>("GetMonacoEditorContent");
                //DebugText = $"{counterSource++} Source:{source}";
                return source;
            }
            catch (Exception e)
            {
                Log(e);
                return string.Empty;
            }
        }

        public async Task<string> JS_SetSourceAsync(string modifiedSource)
        {
            try
            {
                return await JsRuntime.InvokeAsync<string>("SetMonacoEditorContent", modifiedSource);
            }
            catch (Exception e)
            {
                Log(e);
                return string.Empty;
            }
        }

        public async Task<string> JS_SetThemeAsync(string theme)
        {
            try
            {
                return await JsRuntime.InvokeAsync<string>("SetMonacoEditorTheme", theme);
            }
            catch (Exception e)
            {
                Log(e);
                return string.Empty;
            }
        }

        public enum LogType { Error, Warning, Standard}
        public void Log(string message, LogType type = LogType.Standard)
        {
            if (type <= StatusType)
            {
                StatusType = type;
                StatusText = message;
            }
            StateHasChanged();
            if (type == LogType.Standard)
                AutoResetLogAsync(); // do NOT await
        }
        public void Log(Exception e) => Log(e.ToString(), LogType.Error);
        
        public void ResetLog()
        {
            StatusType = LogType.Standard;
            StatusText = "";
            StateHasChanged();
        }

        Guid lastLogResetGuid;
        public async Task AutoResetLogAsync()
        {
            var guid = Guid.NewGuid();
            this.lastLogResetGuid = guid;
            await Task.Delay(5000);

            if (guid == lastLogResetGuid && StatusType == LogType.Standard)
            {
                ResetLog();
            }
        }

        public async Task OnThemeChangedAsync(UIChangeEventArgs e)
        {
            var theme = e.Value.ToString();
            await JS_SetThemeAsync(theme == "Light" ? "vs" : "vs-dark");
            Log("Theme changed");
        }

        /*
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
        */

    }
}
