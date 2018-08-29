# Documentation Language Specification

This document contains the language specification for the documentation in this library. It includes things like what to call parameters (such as IEnumerable<T>), and other language choices.

* For IEnumerable extension methods, always use `collection` as the name for the first parameter (`this`)

* Never refer to an extension method's first parameter as `this` in comments or documentation, always use `paramref` to refer to it

* Use `<see cref>` and `<see langword>` wherever appropriate, including for `null`, `true` and `false` in XML comments

* Self-close XML tags whenever possible (such as `see cref`, `paramref`, `typeparamref` and more)