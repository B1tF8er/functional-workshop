namespace fn
{
    using System;
    using static Constants.Numbers;
    using static Constants.Separators;
    using static System.Console;

    internal static class MathExtensions
    {
        /*
         * Arrow notations, this is right assosiative
         * (int) -> ((int) -> (int))
         * int -> int -> int
         */

        internal static class Multiplication
        {
            private static Func<int, Func<int, int>> Multiply() =>
                (multiplicand) => (multiplier) => multiplicand * multiplier;

            internal static int Multiply((int, int) factors) =>
                Multiply()(factors.Item1)(factors.Item2);

            internal static Func<int, int> MultiplyBy(int multiplicand) =>
                Multiply()(multiplicand);

            internal static int Single(int multiplier) => MultiplyBy(One)(multiplier);

            internal static int Double(int multiplier) => MultiplyBy(Two)(multiplier);

            internal static int Triple(int multiplier) => MultiplyBy(Three)(multiplier);
        }

        internal static class Division
        {
            private static Func<int, Func<int, int>> Divide() =>
                (divisor) => (dividend) => dividend / divisor;

            internal static int Divide((int, int) factors) =>
                Divide()(factors.Item1)(factors.Item2);

            internal static Func<int, int> DivideyBy(int divisor) =>
                Divide()(divisor);

            internal static int ByOne(int dividend) => DivideyBy(One)(dividend);

            internal static int ByTwo(int dividend) => DivideyBy(Two)(dividend);

            internal static int ByThree(int dividend) => DivideyBy(Three)(dividend);
        }

        internal static class Substraction
        {
            private static Func<int, Func<int, int>> Substract() =>
                (subtrahend) => (minuend) => minuend - subtrahend;

            internal static int Substract((int, int) factors) =>
                Substract()(factors.Item1)(factors.Item2);

            internal static Func<int, int> Minus(int subtrahend) =>
                Substract()(subtrahend);

            internal static int MinusOne(int subtrahend) => Minus(One)(subtrahend);
            internal static int MinusTwo(int subtrahend) => Minus(Two)(subtrahend);
            internal static int MinusThree(int subtrahend) => Minus(Three)(subtrahend);
        }

        internal static class Addition
        {
            private static Func<int, Func<int, int>> Add() =>
                (augend) => (addend) => augend + addend;

            internal static int Add((int, int) factors) =>
                Add()(factors.Item1)(factors.Item2);

            internal static Func<int, int> Plus(int augend) =>
                Add()(augend);

            internal static int PlusOne(int addend) => Plus(One)(addend);
            internal static int PlusTwo(int addend) => Plus(Two)(addend);
            internal static int PlusThree(int addend) => Plus(Three)(addend);
        }

        internal static class Examples
        {
            internal static class M
            {
                internal static int Five => Multiplication.Single(5);
                internal static int Ten => Multiplication.Double(5);
                internal static int Fifteen => Multiplication.Triple(5);
            }

            internal static class D
            {
                internal static int Five => Division.ByOne(5);
                internal static int Two => Division.ByTwo(5);
                internal static int One => Division.ByThree(5);
            }

            internal static class S
            {
                internal static int Four => Substraction.MinusOne(5);
                internal static int Three => Substraction.MinusTwo(5);
                internal static int Two => Substraction.MinusThree(5);
            }

            internal static class A
            {
                internal static int Six => Addition.PlusOne(5);
                internal static int Seven => Addition.PlusTwo(5);
                internal static int Eight => Addition.PlusThree(5);
            }

            internal static void RunMath()
            {
                WriteLine($"{Dashes} Multiply  {Dashes}");
                WriteLine(M.Five);
                WriteLine(M.Ten);
                WriteLine(M.Fifteen);
                WriteLine($"{Dashes} Divide    {Dashes}");
                WriteLine(D.Five);
                WriteLine(D.Two);
                WriteLine(D.One);
                WriteLine($"{Dashes} Substract {Dashes}");
                WriteLine(S.Four);
                WriteLine(S.Three);
                WriteLine(S.Two);
                WriteLine($"{Dashes} Add       {Dashes}");
                WriteLine(A.Six);
                WriteLine(A.Seven);
                WriteLine(A.Eight);
            }
        }
    }
}
