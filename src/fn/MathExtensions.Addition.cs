namespace fn
{
    using static Constants.Numbers;

    using Add = System.Func<int, System.Func<int, int>>;
    using Plus = System.Func<int, int>;

    internal static partial class MathExtensions
    {
        internal static class Addition
        {
            private static Add Add() => (augend) => (addend) => augend + addend;
            internal static int Add((int, int) factors) => Add()(factors.Item1)(factors.Item2);
            internal static Plus Plus(int augend) => Add()(augend);
            internal static int PlusOne(int addend) => Plus(One)(addend);
            internal static int PlusTwo(int addend) => Plus(Two)(addend);
            internal static int PlusThree(int addend) => Plus(Three)(addend);
        }
    }
}
