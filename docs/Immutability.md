# Immutability #

In `object-oriented` and `functional` programming,
an immutable object (unchangeable object) is an object whose state
cannot be modified after it is created. In C# we can use
this [Nuget](https://www.nuget.org/packages/System.Collections.Immutable/) package to enforce it.
And we can also follow some practices that I will show you next

```csharp
using System;
using System.Collections.Generic;
using System.Collections.Immutable; // Nuget package
using System.Linq;
using static System.Console;

internal static class TestImmutability
{
    // readonly fields are immutable
    // once they are initialized. at run-time
    private static readonly string dashes = new string('-', 50);
    private static readonly IEnumerable<int> numbers = Enumerable.Range(1, int.MaxValue);

    // const fields are immutable as well. at compile-time
    private const int Ten = 10;
    private const int Six = 6;
    private const int Three = 3;

    internal static void Run()
    {
        List();
        Stack();
        Queue();
    }

    private static void List()
    {
        Separator("Immutable List");

        var list = ImmutableList<int>.Empty;
        Action<int> listAdder = number => list = list.Add(number * Ten);

        numbers
            .Skip(100)
            .Take(10)
            .AddTo(listAdder);

        list.PrintToConsole();
    }

    private static void Stack()
    {
        Separator("Immutable Stack");

        var stack = ImmutableStack<int>.Empty;
        Action<int> stackAdder = number => stack = stack.Push(number * Six);

        numbers
            .SkipWhile(n => n < 200)
            .TakeWhile(n => n < 220)
            .Where(n => n % 3 == 0)
            .AddTo(stackAdder);

        stack.PrintToConsole();
    }

    private static void Queue()
    {
        Separator("Immutable Queue");

        var queue = ImmutableQueue<int>.Empty;
        Action<int> queueAdder = number => queue = queue.Enqueue(number * Three);

        numbers
            .SkipWhile(n => n < 1000)
            .TakeWhile(n => n < 1011)
            .Where(n => n % 2 == 0)
            .AddTo(queueAdder);

        queue.PrintToConsole();
    }

    private static void AddTo<T>(this IEnumerable<T> items, Action<T> addToOtherCollection)
    {
        foreach (var item in items)
            addToOtherCollection(item);
    }

    private static void PrintToConsole<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
            WriteLine(item);
    }

    private static void Separator(this string text)
    {
        WriteLine(dashes);
        WriteLine(text);
        WriteLine(dashes);
    }
}
```
