namespace fn
{
    using System;
    using System.Diagnostics;

    public static class TestActions
    {
        public static void Run()
        {
            Action<string> consoleLoggerHandler = ConsoleLogger;
            Action<string> debugLoggerHandler = DebugLogger;
            Action<string> allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            consoleLoggerHandler("This goes to the console");
            debugLoggerHandler("This goes to the debug");
            allConsoleHandlers("this goes to all");
        }

        public static void ConsoleLogger(string message) => Console.WriteLine(message);

        public static void DebugLogger(string message) => Debug.WriteLine(message);
    }
}
