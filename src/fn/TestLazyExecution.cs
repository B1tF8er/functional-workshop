namespace fn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Console;

    internal class TestLazyExecution
    {
        private static IEnumerable<int> Numbers
        {
            get
            {
                yield return 1;
                yield return 2;
                yield return 3;
                yield return 4;
                yield return 5;
                yield return 6;
                yield return 7;
                yield return 8;
                yield return 9;
                yield return 10;
            }
        }

        internal static void Run()
        {
            LazinessWithFunctions();
            LazinessWithEnumerators();
        }

        private static void LazinessWithFunctions()
        {
            var random = new Random();
            Func<int> leftExpression = () => 23;
            Func<int> rightExpression = () => 42;
            Func<int> lazyExecuted = () => random.NextDouble() < 0.5 ? leftExpression() : rightExpression();

            // all the previous code is evaluated
            // until we call lazyExecuted function
            WriteLine(lazyExecuted());
        }

        private static void LazinessWithEnumerators()
        {
            var skipFour = Numbers.Skip(4);
            var takeFour = skipFour.Take(4);

            // all the previous code is evaluated
            // until we enumerate the collection in a foreach
            foreach (var number in takeFour)
                WriteLine(number);
        }
    }
}
