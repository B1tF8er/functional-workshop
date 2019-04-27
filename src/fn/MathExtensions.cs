namespace fn
{
    using System;
    using static Constants.Numbers;
    using static Constants.Separators;
    using static System.Console;

    internal static class MathExtensions
    {
        internal static class Multiplication
        {
            private static Func<int, Func<int, int>> Multiply() =>
                (multiplicand) => (multiplier) => multiplicand * multiplier;

            internal static int Multiply((int, int) factors) =>
                Multiply()(factors.Item1)(factors.Item1);

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
                Divide()(factors.Item1)(factors.Item1);

            internal static Func<int, int> DivideyBy(int divisor) =>
                Divide()(divisor);

            internal static int ByOne(int dividend) => DivideyBy(One)(dividend);

            internal static int ByTwo(int dividend) => DivideyBy(Two)(dividend);

            internal static int ByThree(int dividend) => DivideyBy(Three)(dividend);
        }

        internal static class Examples
        {
            internal static class M
            {
                internal static int Five() => Multiplication.Single(5);
                internal static int Ten() => Multiplication.Double(5);
                internal static int Fifteen() => Multiplication.Triple(5);
            }

            internal static class D
            {
                internal static int Five() => Division.ByOne(5);
                internal static int Two() => Division.ByTwo(5);
                internal static int One() => Division.ByThree(5);
            }

            internal static void RunMath()
            {
                WriteLine($"{Dashes} Multiply {Dashes}");
                WriteLine(M.Five());
                WriteLine(M.Ten());
                WriteLine(M.Fifteen());
                WriteLine($"{Dashes} Divide   {Dashes}");
                WriteLine(D.Five());
                WriteLine(D.Two());
                WriteLine(D.One());
            }
        }
    }
}
