using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;


namespace MerryYellow.RoslynWeb
{
    public static class Compiler
    {
        public delegate void OnLoggedDelegate(PatternMaker.ELogger.Level level, string message);
        public static event OnLoggedDelegate OnLogged;

        static Compiler()
        {
            PatternMaker.ELogger.OnLogged -= ELogger_OnLogged;
            PatternMaker.ELogger.OnLogged += ELogger_OnLogged;
        }

        private static void ELogger_OnLogged(PatternMaker.ELogger.Level level, string message)
        {
            if (OnLogged != null)
            {
                OnLogged(level, message);
            }
        }

        public static IEnumerable<string> GetPatternList()
        {
            return PatternMaker.Maker.GetPatternList();
        }

        public static IEnumerable<string> GetClassList(string source)
        {
            var ws = CreateWorkspace();
            var newDoc = ws.AddDocument(ws.CurrentSolution.Projects.First().Id, "myfile.cs", SourceText.From(source));

            return PatternMaker.Maker.GetClassList(ws);
            //yield break;
        }

        public static string ApplyPattern(string source, string patternName, string className,
            string setting1 = null, string setting2 = null, bool settingA = false, bool settingB = false)
        {
            var ws = CreateWorkspace();
            var doc = ws.AddDocument(ws.CurrentSolution.Projects.First().Id, "myfile.cs", SourceText.From(source));

            //**--compile check here
            //var comp = ws.CurrentSolution.Projects.First().GetCompilationAsync().Result;

            var patternOptions = new PatternMaker.PatternOptionsCommon()
            {
                Option1 = setting1,
                Option2 = setting2,
                OptionA = settingA,
                OptionB = settingB,
            };

            var newSol = PatternMaker.Maker.ApplyPattern(ws.CurrentSolution, patternName, className, patternOptions);
            var newDoc = newSol.Projects.First().Documents.First();

            var newText = newDoc.GetTextAsync().Result;
            return newText.ToString();
            
            //**--unit test here
            //return PatternMaker.Maker.UnitTestForBlazor(ws.CurrentSolution);
        }

        static AdhocWorkspace CreateWorkspace()
        {
            var workspace = new AdhocWorkspace();
            
            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "MyProject", "MyProject", LanguageNames.CSharp);
            
            /*.
   WithMetadataReferences(new[]
   {
       MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
   });*/
            var project = workspace.AddProject(projectInfo);
            //var document = workspace.AddDocument(project.Id, "MyFile.cs", SourceText.From(code));
            
            return workspace;
        }

        public static string Format(string source)
        {
            var tree = CSharpSyntaxTree.ParseText(source);
            var root = tree.GetRoot();
            var normalized = root.NormalizeWhitespace();
            return normalized.ToString();
        }
    }
}
