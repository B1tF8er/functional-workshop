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
                new Generic<int>(42),
                new Generic<long>(42L),
                new Generic<float>(42.5F),
                new Generic<double>(42D),
                new Generic<decimal>(42.5M),
                new Generic<bool>(false),
                new Generic<DateTime>(DateTime.Now),
                new Generic<byte>(0x0042),
                new Generic<char>('A'),
                new Generic<string>("George"),
                new Generic<Func<int, int>>(x => x + x),
                new Generic<Action<int, int>>((x, y) => WriteLine(x + y)),
                new Generic<dynamic>(7.5F + 35L),
                new Generic<object>(new { a = 42 })
            }
            .ForEach(WriteLine);
        }

        internal class Generic<T>
        {
            private T GenericReadOnlyProperty { get; }

            internal Generic(T genericType) => GenericReadOnlyProperty = genericType;

            public override string ToString() => $"{GenericReadOnlyProperty} - {typeof(T)}";
        }
    }
}
