
[![CI](https://github.com/fabiosvm/cplus/actions/workflows/ci.yml/badge.svg)](https://github.com/fabiosvm/cplus/actions/workflows/ci.yml)

# The C+ Programming Language

This a project to learn Compiler Design and Implementation. The goal is to implement a compiler for a C-like programming language.

## Dependencies

In order to build and run the project you need to have the following dependencies installed:

- [.NET 8.0](https://dotnet.microsoft.com/en-us/download)

## Building

To build the project, browse to the root folder and run the following command:

```
dotnet build
```

## Using the Compiler

To compile a source file run the following command:

```
dotnet run <source-file>
```

Example:

```
dotnet run examples/hello.cp
```

> **Note:** Actually the compiler just prints the diagnostic and the AST of the source file.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.
