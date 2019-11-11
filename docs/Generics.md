# Generics #

A generic allows you to define a class with placeholders
for the type of its fields, methods, parameters, etc.
Generics replace these placeholders with some specific
type at compile time.

- Used to maximize code reuse, type safety, and performance
- Its most common use is to create collection classes
- Constraints work to limit the scope of the types

```csharp
using System;
using System.Collections.Generic;
using static System.Console;

internal static class TestGenerics
{
    internal static void Run()
    {
         CreateGenerics();
         CreateGenericsWithConstraints();
    }

    private static void CreateGenerics()
    {
        new List<object>
        {
            Generic<int>.Create(42),
            Generic<long>.Create(42L),
            Generic<float>.Create(42.5F),
            Generic<double>.Create(42D),
            Generic<decimal>.Create(42.5M),
            Generic<bool>.Create(false),
            Generic<DateTime>.Create(DateTime.Now),
            Generic<byte>.Create(0x0042),
            Generic<char>.Create('A'),
            Generic<string>.Create("George"),
            Generic<Func<int, int>>.Create(x => x + x),
            Generic<Action<int, int>>.Create((x, y) => WriteLine(x + y)),
            Generic<dynamic>.Create(7.5F + 35L),
            Generic<object>.Create(new { a = 42 })
        }
        .ForEach(WriteLine);
    }

    private static void CreateGenericsWithConstraints()
    {
        new List<object>
        {
            GenericWithConstraints<int, object>.Create(42, new { A = 42 }),
            GenericWithConstraints<long, object>.Create(42L, new { B = 42L }),
            GenericWithConstraints<float, object>.Create(42F, new { C = 42F }),
            GenericWithConstraints<double, object>.Create(42D, new { D = 42D }),
            GenericWithConstraints<decimal, object>.Create(42M, new { E = 42M }),
            GenericWithConstraints<bool, object>.Create(true, new { F = true }),
            GenericWithConstraints<DateTime, object>.Create(DateTime.Now, new { G = DateTime.Now }),
            GenericWithConstraints<byte, object>.Create(0x0042, new { H = 0x0042 }),
            GenericWithConstraints<char, object>.Create('J', new { I = 'J' })
        }
        .ForEach(WriteLine);
    }

    private class Generic<T>
    {
        private T GenericReadOnlyProperty { get; }

        private Generic(T genericArgument) => GenericReadOnlyProperty = genericArgument;

        internal static Generic<T> Create(T genericValue) => new Generic<T>(genericValue);

        public override string ToString() => $"{GenericReadOnlyProperty} - {typeof(T)}";
    }

    private class GenericWithConstraints<T, U>
        where T : struct
        where U : new()
    {
        private T @Struct { get; }

        private U @Object { get; }

        private GenericWithConstraints(T @struct, U @object) =>
            (@Struct, @Object) = (@struct, @object);

        internal static GenericWithConstraints<T, U> Create(T @struct, U @object) =>
            new GenericWithConstraints<T, U>(@struct, @object);

        public override string ToString() =>
            $"{@Struct} - {typeof(T)} && {@Object} - {typeof(U)} ";
    }
}
```
