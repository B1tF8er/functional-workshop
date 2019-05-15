namespace fn
{
    using System;
    using System.Diagnostics;

    internal static class TestActions
    {
        internal static void Run()
        {
            Action<string> consoleLoggerHandler = ConsoleLogger;
            Action<string> debugLoggerHandler = DebugLogger;
            Action<string> allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            consoleLoggerHandler("This goes to the console");
            debugLoggerHandler("This goes to the debug");
            allConsoleHandlers("this goes to all");
        }

        private static void ConsoleLogger(string message) => Console.WriteLine(message);

        private static void DebugLogger(string message) => Debug.WriteLine(message);
    }
}
