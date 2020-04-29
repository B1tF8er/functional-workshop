# Recursion #

In functional programming, recursion is preferred over loops
because by using it we don't need a mutable state
while solving some problem, and this makes possible to specify
a semantic in simpler terms. Thus solutions can be simpler, in a formal sense.

```csharp
using static System.Console;
using Predicate = System.Func<int, bool>;
using Step = System.Func<int, int>;

internal static class TestRecursion
{
    internal static void Run()
    {
        WriteLine(Counter.CountDownFrom(10, stopAt: 0, decrementBy: 2));
        WriteLine(Counter.CountUpTo(20, startAt: 0, incrementBy: 2));
        WriteLine(Counter.CountDownFrom(30, stopAt: 0, decrementBy: 5));
        WriteLine(Counter.CountUpTo(40, startAt: 0, incrementBy: 5));
    }

    private static class Counter
    {
        public static int CountUpTo(int stopAt, int startAt, int incrementBy) =>
            Count(startAt, (it) => it == stopAt, (it) => it + incrementBy);

        public static int CountDownFrom(int startAt, int stopAt, int decrementBy) =>
            Count(startAt, (it) => it == stopAt, (it) => it - decrementBy);

        // Here we use recursion.
        private static int Count(int it, Predicate mustStop, Step step) =>
            mustStop(it)
                ? it
                : Count(step(it), mustStop, step);
    }
}
```
