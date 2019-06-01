namespace fn
{
    using static Constants.Numbers;

    using Multiply = System.Func<int, System.Func<int, int>>;
    using MultiplyBy = System.Func<int, int>;

    internal static partial class MathExtensions
    {
        internal static class Multiplication
        {
            private static Multiply Multiply() => (multiplicand) => (multiplier) => multiplicand * multiplier;
            internal static int Multiply((int, int) factors) => Multiply()(factors.Item1)(factors.Item2);
            internal static MultiplyBy MultiplyBy(int multiplicand) => Multiply()(multiplicand);
            internal static int Single(int multiplier) => MultiplyBy(One)(multiplier);
            internal static int Double(int multiplier) => MultiplyBy(Two)(multiplier);
            internal static int Triple(int multiplier) => MultiplyBy(Three)(multiplier);
        }
    }
}
