namespace fn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Constants.Numbers;
    using static Constants.Separators;
    using static MathExtensions;
    using static System.Console;

    internal static class Math
    {
        private static IDictionary<string, int> MathOperations = new Dictionary<string, int>()
        {
            { " Multiply  ", Zero },
            { " Divide    ", Three },
            { " Substract ", Six },
            { " Add       ", Nine }
        };

        private static IEnumerable<int> Operations
        {
            get
            {
                yield return Multiplication.Single(Constants.Numbers.Five);
                yield return Multiplication.Double(Constants.Numbers.Five);
                yield return Multiplication.Triple(Constants.Numbers.Five);
                yield return Division.ByOne(Constants.Numbers.Five);
                yield return Division.ByTwo(Constants.Numbers.Five);
                yield return Division.ByThree(Constants.Numbers.Five);
                yield return Substraction.MinusOne(Constants.Numbers.Five);
                yield return Substraction.MinusTwo(Constants.Numbers.Five);
                yield return Substraction.MinusThree(Constants.Numbers.Five);
                yield return Addition.PlusOne(Constants.Numbers.Five);
                yield return Addition.PlusTwo(Constants.Numbers.Five);
                yield return Addition.PlusThree(Constants.Numbers.Five);
            }
        }

        internal static void Run()
        {
            foreach (var type in MathOperations)
                GetOperationsFor(type.Key, type.Value, Three);
        }

        private static void GetOperationsFor(string message, int from, int to)
        {
            WriteLine($"{Dashes}{message}{Dashes}");

            foreach (var operation in Operations.Skip(from).Take(to))
                WriteLine(operation);
        }
    }
}
