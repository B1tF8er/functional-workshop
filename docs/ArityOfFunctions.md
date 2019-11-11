# Arity Of Functions #

In logic, mathematics, and computer science, the arity of a function or operation
is the number of arguments or operands that the function takes,

- `Nullary`, takes no arguments
- `Unary`, takes one argument
- `Binary`, takes two arguments
- `Ternary`, takes three arguments
- `n-ary`, takes *n* arguments

```csharp
using System.Linq;
using static System.Console;

internal class TestArity
{
    internal static void Run()
    {
        WriteLine(Nullary());
        WriteLine(Unary(1));
        WriteLine(Binary(1, 2));
        WriteLine(Ternary(1, 2, 3));
        WriteLine(N_Ary(1, 2, 3, 4));
        WriteLine(N_Ary(Enumerable.Range(1, 16).ToArray()));
        WriteLine(N_Ary(Enumerable.Range(1, 100).ToArray()));
    }

    private static string Nullary()
        => "No arguments";

    private static string Unary(int one)
        => $"One argument: {one}";

    private static string Binary(int one, int two)
        => $"Two arguments: {one} and {two}";

    private static string Ternary(int one, int two, int three)
        => $"Three arguments: {one}, {two} and {three}";

    private static string N_Ary(params int[] arguments)
        => $"N arguments: {arguments.Length}";
}
```
