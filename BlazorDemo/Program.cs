using Microsoft.AspNetCore.Blazor.Hosting;

namespace BlazorDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args) =>
            BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();

        public static void Foo()
        {
            //var sr = Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter();
            //var ws = new Microsoft.CodeAnalysis.AdhocWorkspace();
            var r = new FooRewriter();
        }

        public class FooRewriter : Microsoft.CodeAnalysis.CSharp.CSharpSyntaxRewriter
        {

        }
    }
}
