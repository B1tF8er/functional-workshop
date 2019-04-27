namespace fn
{
    using System;
    using static MathExtensions.Examples;
    using static TryCatchExceptions;

    class Program
    {
        static void Main(string[] args) => TryOrFailFast(RunMath);
    }
}
