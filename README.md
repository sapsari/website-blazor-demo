
# Blazor Demo with Roslyn

### [Try online](https://patternmaker.netlify.com)

### [2nd demo](https://enchanter.netlify.com) (cloned from this, with IL Code view)

This is a sample website that uses Blazor (client-side) to run Roslyn based projects on the browser with Monaco Editor.

Latest version uses .NET 5

**How to use:**

* In project RoslynWeb, remove PatternMaker from the references, add your own Roslyn based project or dll. Also cleanup method calls to PatternMaker.

Also don't forget that Webassembly has some restrictions since it runs on browsers. So be prepared to receive some errors on your Roslyn based project.

