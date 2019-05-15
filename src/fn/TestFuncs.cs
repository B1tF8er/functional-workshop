namespace fn
{
    using System;
    using System.Diagnostics;

    internal static class TestFuncs
    {
        internal static void Run()
        {
            Func<string, string> consoleLoggerHandler = ConsoleLogger;
            Func<string, string> debugLoggerHandler = DebugLogger;
            Func<string, string> allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            var consoleResponse = consoleLoggerHandler("This goes to the console");
            var debugResponse = debugLoggerHandler("This goes to the debug");
            var lastResponse = allConsoleHandlers("this goes to all");
        }

        private static string ConsoleLogger(string message)
        {
            Console.WriteLine(message);
            return "Logged to console";
        }

        private static string DebugLogger(string message)
        {
            Debug.WriteLine(message);
            return "Logged to debug";
        }
    }
}
