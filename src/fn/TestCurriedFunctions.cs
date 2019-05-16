namespace fn
{
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
            (argOne) => (argTwo) => $"{argOne} {argTwo}";

        private static Func<int, Func<int, int>> Sum =>
            (lhs) => (rhs) => lhs + rhs;
    }
}
