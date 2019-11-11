# Pure Functions #

It is said that a function is pure when its output
depends entirely on the input parameters. And cause
no side effects. Therefore they improve,

- Testability
- Correctness
- Readability

With no side effects there are no surprises. our code
does what it says, no more. and the cognitive load
to understand the code is less

```csharp
using static System.Console;

internal class TestPureFunctions
{
    private const string A = "a";
    private const string B = "b";
    private const string AB = "ab";
    private const string ABAB = "abab";

    internal static void Run()
    {
        var ab = Join(A, B);
        // We can expect the same result always
        // if we pass the same parameters
        WriteLine(ab == AB);
        WriteLine(Join(A, B) == AB);

        var abab = Join(AB, AB);
        // This also has an impact
        // on Referential Transparency
        WriteLine(abab == ABAB);
        WriteLine(Join(AB, AB) == ABAB);
    }

    private static string Join(string lhs, string rhs)
        => $"{lhs}{rhs}";
}
```
