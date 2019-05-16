namespace fn
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static Constants.Numbers;
    using static Constants.Separators;
    using static MathExtensions;
    using static System.Console;

    internal static class Examples
    {
        private static IDictionary<string, int> MathOperations = new Dictionary<string, int>()
        {
            { " Multiply  ", 0 },
            { " Divide    ", 3 },
            { " Substract ", 6 },
            { " Add       ", 9 },
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

        private static IDictionary<string, Action> Samples = new Dictionary<string, Action>()
        {
            { " Delegates           ", TestDelegates.Run },
            { " Actions             ", TestActions.Run },
            { " Funcs               ", TestFuncs.Run },
            { " Curried Functions   ", TestCurriedFunctions.Run },
            { " Partial Application ", TestPartialApplication.Run },
            { " Lazy Execution      ", TestLazyExecution.Run },
        };

        internal static void Run()
        {
            RunMath();
            RunSamples();
        }
        
        private static void RunMath()
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

        private static void RunSamples()
        {
            foreach (var sample in Samples)
            {
                WriteLine($"{Dashes}{sample.Key}{Dashes}");
                sample.Value();
            }
        }
    }
}
