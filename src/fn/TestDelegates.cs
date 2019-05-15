namespace fn
{
    using System;
    using System.Diagnostics;

    public static class TestDelegates
    {
        public delegate void LoggerDelegate(string message);

        public static void Run()
        {
            LoggerDelegate consoleLoggerHandler = ConsoleLogger;
            LoggerDelegate debugLoggerHandler = DebugLogger;
            LoggerDelegate allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            consoleLoggerHandler("This goes to the console");
            debugLoggerHandler("This goes to the debug");
            allConsoleHandlers("this goes to all");
        }

        public static void ConsoleLogger(string message) => Console.WriteLine(message);

        public static void DebugLogger(string message) => Debug.WriteLine(message);
    }
}
