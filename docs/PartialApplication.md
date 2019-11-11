# Partial Application #

Providing a function with fewer arguments than it expects is called
Partial Application of functions. In order to produce more reusable functions
and create better abstractions we need a mechanism
to preset some of the arguments of a function. Partial application is the technique
used to transform functions of higher arity (functions that take multiple arguments)
into multiple functions that take less arguments.

```csharp
using System;
using static System.Console;

internal class TestPartialApplication
{
    internal static void Run()
    {
        WriteLine(MaleFormalGreeter("World"));
        WriteLine(FemaleFormalGreeter("World"));
        WriteLine(PlusFive(5));
        WriteLine(PlusFive(18));
    }

    private static Func<string, Func<string, string>> Greeter =>
        (firstName) => (lastName) => $"{firstName} {lastName}";

    private static Func<string, string> MaleFormalGreeter =>
        (lastName) => Greeter("Mr.")(lastName);

    private static Func<string, string> FemaleFormalGreeter =>
        (lastName) => Greeter("Ms.")(lastName);

    private static Func<int, Func<int, int>> Sum =>
        (lhs) => (rhs) => lhs + rhs;

    private static Func<int, int> PlusFive =>
        (factor) => Sum(factor)(5);
}
```
