
# Blazor Demo with Roslyn

### [Try online](https://patternmaker.netlify.com)

This is a sample website that uses Blazor (client-side) to run Roslyn based projects on the browser.

**Notice:** It is developed with .Net Core v3.0.0-preview6 (SDK 3.0.100-preview6-012264) and Visual Studio 2019 Preview (version 16.2.0 preview3.0). Since it uses preview packages, it is very likely to work incorrectly with other versions.

How to use:

* [Install Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=visual-studio)
* In project RoslynWeb, remove PatternMaker from the references, add your own Roslyn based project or dll. Also cleanup method calls to PatternMaker.
* When building or publishing; add "Microsoft.CodeAnalysis.CSharp.Workspaces.dll" to assemblyReferences in blazor.boot.json each time you build; also copy the dll to _bin folder for once (RoslynWebExecutable should have this dll on its output folder)

Also don't forget that Blazor uses Mono runtime, not .Net Core. So be prepared to recieve some errors on your Roslyn based project. I had to fix three or four parts, mostly related with symbols.
