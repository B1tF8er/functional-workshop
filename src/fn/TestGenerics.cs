namespace fn
{
    using System;
    using System.Collections.Generic;
    using static System.Console;

    internal static class TestGenerics
    {
        internal static void Run() => CreateGenerics();

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

        internal class Generic<T>
        {
            private T GenericReadOnlyProperty { get; }

            private Generic(T genericArgument) => GenericReadOnlyProperty = genericArgument;

            internal static Generic<T> Create(T genericValue) => new Generic<T>(genericValue);

            public override string ToString() => $"{GenericReadOnlyProperty} - {typeof(T)}";
        }
    }
}
