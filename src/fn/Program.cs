namespace fn
{
    using System;
    using static Examples;
    using static TryCatchExceptions;

    class Program
    {
        static void Main(string[] args) => TryOrFailFast(Run);
    }
}
