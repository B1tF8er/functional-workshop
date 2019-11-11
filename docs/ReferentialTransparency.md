# Referential Transparency #

In functional programming, referential transparency is generally
defined as the fact that an expression, in a program, may be replaced
by its value (or anything having the same value) without changing the result
of the program. This implies that methods should always return the same value
for a given argument (purity of functions), without having any other effect.

This functional programming concept also applies to imperative programming, though,
and can help you make your code clearer.

```csharp
using static System.Console;

internal class TestReferentialTransparency
{
    private const int Two = 2;
    private const int Nine = 9;
    private const int Sixteen = 16;
    private const int Eighteen = 18;

    internal static void Run()
    {
        OperationsWithReferentialTransparency();
        OperationsWithoutReferentialTransparency();
    }

    private static void OperationsWithReferentialTransparency()
    {
        // YES - Sum(16, 2) == 18 and Mul(9, 2) == 18
        // and the result of the program will be the same
        var sum = Sum(Sixteen, Two) == Eighteen;
        var mul = Mul(Nine, Two) == Eighteen;
    }

    private static void OperationsWithoutReferentialTransparency()
    {
        // NO - Sum(16, 2) == 18 and Mul(9, 2) == 18
        // but WE ARE NOT logging to the console the result
        var sum = SumLog(Sixteen, Two) == Eighteen;
        var mul = MulLog(Nine, Two) == Eighteen;
    }

    private static int Sum(int a, int b) => a + b;

    private static int Mul(int a, int b) => a * b;

    private static int SumLog(int a, int b)
    {
        var result = a + b;
        WriteLine($"Returning {result}");
        return result;
    }

    private static int MulLog(int a, int b)
    {
        var result = a * b;
        WriteLine($"Returning {result}");
        return result;
    }
}
```
