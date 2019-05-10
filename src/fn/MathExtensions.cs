
namespace fn
{
    using System;
    using static Constants.Numbers;
    
    using UnaryF = System.Func<int, int>;
    using BinaryF = System.Func<int, System.Func<int, int>>;

    internal static class MathExtensions
    {
        /*
         * Arrow notations, this is right assosiative
         * (int) -> ((int) -> (int))
         * int -> int -> int
         */

        internal static class Multiplication
        {
            private static BinaryF Multiply() => (multiplicand) => (multiplier) => multiplicand * multiplier;
            internal static int Multiply((int, int) factors) => Multiply()(factors.Item1)(factors.Item2);
            internal static UnaryF MultiplyBy(int multiplicand) => Multiply()(multiplicand);
            internal static int Single(int multiplier) => MultiplyBy(One)(multiplier);
            internal static int Double(int multiplier) => MultiplyBy(Two)(multiplier);
            internal static int Triple(int multiplier) => MultiplyBy(Three)(multiplier);
        }

        internal static class Division
        {
            private static BinaryF Divide() => (divisor) => (dividend) => dividend / divisor;
            internal static int Divide((int, int) factors) => Divide()(factors.Item1)(factors.Item2);
            internal static UnaryF DivideyBy(int divisor) => Divide()(divisor);
            internal static int ByOne(int dividend) => DivideyBy(One)(dividend);
            internal static int ByTwo(int dividend) => DivideyBy(Two)(dividend);
            internal static int ByThree(int dividend) => DivideyBy(Three)(dividend);
        }

        internal static class Substraction
        {
            private static BinaryF Substract() => (subtrahend) => (minuend) => minuend - subtrahend;
            internal static int Substract((int, int) factors) => Substract()(factors.Item1)(factors.Item2);
            internal static UnaryF Minus(int subtrahend) => Substract()(subtrahend);
            internal static int MinusOne(int subtrahend) => Minus(One)(subtrahend);
            internal static int MinusTwo(int subtrahend) => Minus(Two)(subtrahend);
            internal static int MinusThree(int subtrahend) => Minus(Three)(subtrahend);
        }

        internal static class Addition
        {
            private static BinaryF Add() => (augend) => (addend) => augend + addend;
            internal static int Add((int, int) factors) => Add()(factors.Item1)(factors.Item2);
            internal static UnaryF Plus(int augend) => Add()(augend);
            internal static int PlusOne(int addend) => Plus(One)(addend);
            internal static int PlusTwo(int addend) => Plus(Two)(addend);
            internal static int PlusThree(int addend) => Plus(Three)(addend);
        }
    }
}
