# Side Effects #

To clarify this definition, we must define exactly
what a side effect is. A function is said to have side effects
if it does any of the following,

- Mutates global state — “Global” here means any state
    that’s visible outside of the function’s scope.
    For example, a private instance field is considered global
    because it’s visible from all methods within the class.
- Mutates its input arguments
- Throws exceptions
- Performs any I/O operation — This includes any interaction
    between the program and the external world,
    including reading from or writing to the console,
    the filesystem, or a database, and interacting with
    any process outside the application’s boundary.

```csharp
using static System.Console;

internal class TestSideEffects
{
    internal static void Run()
    {
        var ab = JoinWithLogs("a", "b");
        var abab = JoinWithLogs(ab, ab);

        WriteLine(ab);
        WriteLine(abab);
    }

    private static string JoinWithLogs(string lhs, string rhs)
    {
        // Here we have the side effect of writing to console
        // but it could be any I/O operation.
        WriteLine($"lhs before: {lhs}");
        WriteLine($"rhs before: {rhs}");

        var result = $"{lhs}{rhs}";

        WriteLine($"result: {result}");
        return result;
    }
}
```
