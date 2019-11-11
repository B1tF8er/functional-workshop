# Higher Order Functions #

Higher Order Functions or `HOF`s are functions that take
other functions as inputs or return a function as output, or both.

```csharp
using System;
using static System.Console;
using Subtractor = System.Func<int, int, int>;

internal class TestHigherOrderFunctions
{
    private readonly static Subtractor subtractor = (x, y) => x - y;

    internal static void Run() =>
        WriteLine(Generator(subtractor)(44, 2));

    // This is a HOF
    // takes a function as argument
    // and returns a function
    private static Subtractor Generator(Subtractor subtractor)
        => subtractor;
}
```
