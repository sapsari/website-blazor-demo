
# Blazor Demo with Roslyn

### [Try online](https://patternmaker.netlify.com)

### [2nd demo](https://enchanter.netlify.com) (cloned from this, with IL Code view)

This is a sample website that uses Blazor (client-side) to run Roslyn based projects on the browser with Monaco Editor.

**Notice:** This is tested with versions below, other versions may not work correctly since it uses preview packages:
* Blazor 3.1.0-preview4 (3.1.0-preview4.19579.2) && .Net Core v3.1.100 (SDK 3.1.100) && Visual Studio 2019 Preview (version 16.5.0 Preview 1.0)
* Blazor v3.0.0-preview9 (3.0.0-preview9.19457.4) && .Net Core v3.0.0-rc1 (SDK 3.0.100-rc1-014190) && Visual Studio 2019 Preview (version 16.3.0 preview4.0)
* Blazor v3.0.0-preview8 (3.0.0-preview8.19405.7) && .Net Core v3.0.0-preview8 (SDK 3.0.100-preview8-013656) && Visual Studio 2019 Preview (version 16.3.0 preview2.0)
* Blazor v3.0.0-preview7 (3.0.0-preview7.19365.7) && .Net Core v3.0.0-preview7 (SDK 3.0.100-preview7-012821) && Visual Studio 2019 Preview (version 16.3.0 preview1.0)
* Blazor v3.0.0-preview6 (3.0.0-preview6.19307.2) && .Net Core v3.0.0-preview6 (SDK 3.0.100-preview6-012264) && Visual Studio 2019 Preview (version 16.2.0 preview3.0)

**How to use:**

* [Install Blazor](https://docs.microsoft.com/en-us/aspnet/core/blazor/get-started?view=aspnetcore-3.0&tabs=visual-studio)
* In project RoslynWeb, remove PatternMaker from the references, add your own Roslyn based project or dll. Also cleanup method calls to PatternMaker.
* When building or publishing; add "Microsoft.CodeAnalysis.CSharp.Workspaces.dll" to assemblyReferences in blazor.boot.json each time you build; also copy the dll to _bin folder for once (project RoslynWebExecutable should have this dll on its output folder)

Also don't forget that Blazor uses Mono runtime, not .Net Core. So be prepared to receive some errors on your Roslyn based project. I had to fix three or four parts, mostly related with symbols.

**Known bugs:**

* System.NullReferenceException: Object reference not set to an instance of an object. at Microsoft.CodeAnalysis.SmallDictionary2[K,V].Insert     
This is related with Mono, if you are receiving this, you may need to downgrade Blazor version to 3.1.0-preview2, or wait for a fix. 
Related issues [*](https://github.com/mono/mono/issues/18119) [*](https://github.com/mono/mono/issues/18120)

