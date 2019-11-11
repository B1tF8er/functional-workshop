# LINQ #

LINQ stands for `L`anguage `IN`tegrated `Q`uery. It is a functional library
and offers implementations for many common operations on lists
or, more generally, on “sequences.” As instances of IEnumerable should technically be called,
the most common of which are mapping, sorting, and filtering

- Makes extensive use of higher-order functions as arguments
- Uses method syntax or query syntax

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

internal static class TestLINQ
{
    private static readonly string Dashes = new string('-', 40);

    private static readonly IEnumerable<char> Alphabet = Enumerable
        .Range('a', 'z' - 'a' + 1)
        .Select(i => (Char)i);

    private static readonly IEnumerable<int> LazyNumbers = Enumerable.Range(0, 50);

    private static IEnumerable<IEnumerable<int>> NestedLazyNumbers
    {
        get
        {
            yield return LazyNumbers;
            yield return LazyNumbers;
        }
    }

    internal static void Run()
    {
        Map();
        Filter();
        Zip();
        Take();
        Skip();
        TakeWhile();
        SkipWhile();
        OrderBy();
        OrderByDescending();
        FoldRight();
        FoldLeft();
        Bind();
    }

    private static void Map()
    {
        LazyNumbers
            .Select(number => number * 10)
            .Print(nameof(Map));
    }

    private static void Filter()
    {
        LazyNumbers
            .Where(number => number > 5)
            .Print(nameof(Filter));
    }

    private static void Zip()
    {
        LazyNumbers
            .Zip(Alphabet, (number, letter) => $"{number} --> {letter}")
            .Print(nameof(Zip));
    }

    private static void Take()
    {
        LazyNumbers
            .Take(10)
            .Print(nameof(Take));
    }

    private static void Skip()
    {
        LazyNumbers
            .Skip(40)
            .Print(nameof(Skip));
    }

    private static void TakeWhile()
    {
        LazyNumbers
            .TakeWhile(number => number < 10)
            .Print(nameof(TakeWhile));
    }

    private static void SkipWhile()
    {
        LazyNumbers
            .SkipWhile(number => number < 40)
            .Print(nameof(SkipWhile));
    }

    private static void OrderBy()
    {
        LazyNumbers
            .OrderBy(number => number)
            .Print(nameof(OrderBy));
    }

    private static void OrderByDescending()
    {
        LazyNumbers
            .OrderByDescending(number => number)
            .Print(nameof(OrderByDescending));
    }

    private static void FoldRight()
    {
        LazyNumbers
            .Take(10)
            .Aggregate((accumulator, next) => accumulator + next)
            .ToEnumerable()
            .Print(nameof(FoldRight));
    }

    private static void FoldLeft()
    {
        LazyNumbers
            .Skip(40)
            .Reverse()
            .Aggregate((accumulator, next) => accumulator - next)
            .ToEnumerable()
            .Print(nameof(FoldLeft));
    }

    private static void Bind()
    {
        NestedLazyNumbers
            .SelectMany(innerEnumerable => innerEnumerable)
            .OrderBy(number => number)
            .Take(20)
            .Print(nameof(Bind));
    }

    private static void Print<TItem>(this IEnumerable<TItem> items, string message)
    {
        var spaces = new string(' ', 31 - message.Length);
        WriteLine($"{Dashes} {message}{spaces}{Dashes}");

        foreach (var item in items)
            WriteLine(item);
    }

    private static IEnumerable<TItem> ToEnumerable<TItem>(this TItem item)
    {
        yield return item;
    }
}
```
