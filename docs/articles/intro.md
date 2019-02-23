# Phnx
`Phnx` is a framework designed to work with the `.NET` family of libraries to take common tasks and code, and simplify them into easy to use and implement code. This ranges from simplifying password hashing to creating API requests.

The framework is split into multiple packages, so that developers can pick and choose which parts of the framework they want to use, without needing to add the entire piece.

If you don't want to pick which packages to download, and just want to quickly get started, download the [Phnx.All](https://www.nuget.org/packages/Phnx.All/) package. This contains all of the .NET Standard packages including the following:
* Phnx
* Phnx.Benchmark
* Phnx.Collections
* Phnx.Console
* Phnx.Data
* Phnx.Drawing
* Phnx.IO
* Phnx.IO.Json
* Phnx.IO.Threaded
* Phnx.Reflection
* Phnx.Security
* Phnx.Serialization
* Phnx.Web

## Compatibility & Dependencies
All `.NET Framework` dependant libraries are designed for `.NET Framework 4.5` or higher.
All `ASP.NET` dependant libraries are designed for `ASP.NET MVC 5` or higher.
There are no `EntityFramework` dependant libraries - only `EntityFrameworkCore`.
All `.NET Core`, `EntityFrameworkCore`, `AspNetCore` or `.NET Standard` libraries are targeted at version `2.0` of their respective libraries.

* Projects within a `Windows` project have dependencies on the `.NET Framework`

* Projects within a `Core` project have dependencies on `.NET Core`

* Projects within an `EFCore` project have dependencies on `Microsoft.EntityFrameworkCore`

* Projects within an `AspNet` project have dependencies on `ASP.NET MVC 5`

* Projects within an `AspNetCore` project have dependencies on `Microsoft.AspNetCore`

* All other projects use `.NET Standard`
