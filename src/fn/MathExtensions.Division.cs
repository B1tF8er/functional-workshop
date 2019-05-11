namespace fn
{
    using static Constants.Numbers;

    using UnaryF = System.Func<int, int>;
    using BinaryF = System.Func<int, System.Func<int, int>>;

    internal static partial class MathExtensions
    {
        internal static class Division
        {
            private static BinaryF Divide() => (divisor) => (dividend) => dividend / divisor;
            internal static int Divide((int, int) factors) => Divide()(factors.Item1)(factors.Item2);
            internal static UnaryF DivideyBy(int divisor) => Divide()(divisor);
            internal static int ByOne(int dividend) => DivideyBy(One)(dividend);
            internal static int ByTwo(int dividend) => DivideyBy(Two)(dividend);
            internal static int ByThree(int dividend) => DivideyBy(Three)(dividend);
        }
    }
}