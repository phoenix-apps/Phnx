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
A wiki for this project is maintained on GitHub, in the Wiki tab. This can be found [here](https://github.com/phoenix-apps/Phnx/wiki). This wiki is updated with each release.

## Contributing
We welcome contributions. Please submit a pull request with your changes, and follow the [Documentation Language Specification](https://github.com/phoenix-apps/Phnx/blob/master/Styles%20and%20Standards/Documentation%20Language%20Specification.md) when updating software documentation for your changes (where necessary).

Please do not submit a pull request without an issue attached. If there isn't an issue reported for the fix (or refactoring) you're trying to make, please create one. This makes it easier for us to understand the reasons behind your change(s).

Before getting started, run the `build_all_core.cmd` (for Windows) or `build_all_core.sh` (for Linux/Mac OS) to build all standard and core projects. Rerunning this after you're finished is a good idea, but not necessary as long as the projects you've changed build.

Any additional logic or changes you make should have automated tests that validate their logic. Avoid writing tests that test _how_ your code works (such as calling other methods), but assert that the code produces the result (or exception) you expect.
