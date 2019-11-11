# First Class Functions #

When functions become first class citizens in a language. Means they
can,

- Be used as parameters in other functions
- Be used as the return type of a function
- They can be assigned to variables
- Be stored in a collection

```csharp
using System;
using System.Collections.Generic;
using static System.Console;
// you could use an alias to simplify types
// int -> int -> int
using Adder = System.Func<int, int, int>;

internal class TestFirstClassFunctions
{
    // Assigned to a variable
    private readonly static Adder adder = (x, y) => x + y;

    internal static void Run()
    {
        // Stored in a collection
        var functions = new Dictionary<string, Adder>
        {
            { "One", adder },
            { "Two", Generator(adder) },
        };

        var index = 1;

        foreach (var f in functions)
            WriteLine($"{f.Key} => {f.Value.Invoke(index, ++index)}");
    }

    // As return type and as parameter
    private static Adder Generator(Adder adder) => adder;
}
```
