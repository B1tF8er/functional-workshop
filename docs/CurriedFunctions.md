# Curried Functions #

Named after mathematician Haskell Curry, currying is the process of transforming
an `n-ary` function f that takes arguments t1, t2, ..., tn
into a `unary` function that takes t1 and yields a new function that takes t2,
and so on, ultimately returning the same result as f once the arguments
have all been given. In other words, an n-ary function with signature

`(T1, T2, ..., Tn) → R`

when curried has signature

`(T1) (T2) ... (Tn) → R`

By itself this technique is pretty useless, but it does enable the use
of partial application.

```csharp
using System;
using static System.Console;

internal class TestCurriedFunctions
{
    internal static void Run()
    {
        WriteLine(Greeter("Hello")("World"));
        WriteLine(Sum(5)(18));
    }

    private static Func<string, Func<string, string>> Greeter =>
        (firstName) => (lastName) => $"{firstName} {lastName}";

    private static Func<int, Func<int, int>> Sum =>
        (lhs) => (rhs) => lhs + rhs;
}
```
