namespace fn
{
    using System;
    using System.Collections.Generic;
    using static Constants.Separators;
    using static System.Console;

    internal static class Examples
    {
        private static IDictionary<string, Action> Samples = new Dictionary<string, Action>()
        {
            { " Math                       ", Math.Run },
            { " Delegates                  ", TestDelegates.Run },
            { " Actions                    ", TestActions.Run },
            { " Funcs                      ", TestFuncs.Run },
            { " Curried Functions          ", TestCurriedFunctions.Run },
            { " Partial Application        ", TestPartialApplication.Run },
            { " Lazy Evaluation            ", TestLazyEvaluation.Run },
            { " Extension Methods          ", TestExtensionMethods.Run },
            { " Smart Constructors         ", TestSmartConstructors.Run },
            { " Avoid Primitive Obsession  ", TestAvoidPrimitiveObsession.Run },
            { " Generics                   ", TestGenerics.Run }
        };

        internal static void Run()
        {
            foreach (var sample in Samples)
            {
                WriteLine($"{Dashes}{sample.Key}{Dashes}");
                sample.Value();
            }
        }
    }
}
