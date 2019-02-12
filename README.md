# Phnx
`Phnx` is a framework designed to work with the `.NET` family of libraries to take common tasks and code, and simplify them into easy to use and implement code. This ranges from simplifying password hashing to creating API requests.

The framework is split into multiple packages, so that developers can pick and choose which parts of the framework they want to use, without needing to add the entire piece.

## Compatibility & Dependencies
All `.NET Framework` dependant libraries are designed for `.NET Framework 4.5` or higher.
All `ASP.NET` dependant libraries are designed for `ASP.NET MVC 5` or higher.
There are no `EntityFramework` dependant libraries - only `EntityFrameworkCore`.
All `.NET Core`, `EntityFrameworkCore`, `AspNetCore` or `.NET Standard` libraries are targeted at version `2.0` of their respective libraries.

* Projects within a `Windows` subdirectory have dependencies on the `.NET Framework`

* Projects within a `Core` subdirectory have dependencies on `.NET Core`

* Projects within an `EFCore` subdirectory have dependencies on `Microsoft.EntityFrameworkCore`

* Projects within an `AspNet` subdirectory have dependencies on `ASP.NET MVC 5`

* Projects within an `AspNetCore` subdirectory have dependencies on `Microsoft.AspNetCore`

* All other projects use `.NET Standard`

## Help & Documentation
Documentation is available, hosted by GitHub Pages [here](https://phoenix-apps.github.io/Phnx-Wiki). This documentation is updated with each release.

## Contributing
We welcome contributions. Please follow the [Phoenix Standards](https://github.com/phoenix-apps/Standards) when developing on any of our projects.

Before getting started, run the `build_all_core.cmd` (for Windows) or `build_all_core.sh` (for Linux/Mac OS) to build all standard and core projects. Rerunning this after you're finished is a good idea to crudely check for breaking changes, but it's not necessary.
