namespace fn
{
    using static Constants.Numbers;

    using Substract = System.Func<int, System.Func<int, int>>;
    using Minus = System.Func<int, int>;

    internal static partial class MathExtensions
    {
        internal static class Substraction
        {
            private static Substract Substract() => (subtrahend) => (minuend) => minuend - subtrahend;
            internal static int Substract((int, int) factors) => Substract()(factors.Item1)(factors.Item2);
            internal static Minus Minus(int subtrahend) => Substract()(subtrahend);
            internal static int MinusOne(int subtrahend) => Minus(One)(subtrahend);
            internal static int MinusTwo(int subtrahend) => Minus(Two)(subtrahend);
            internal static int MinusThree(int subtrahend) => Minus(Three)(subtrahend);
        }
    }
}
