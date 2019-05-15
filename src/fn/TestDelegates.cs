namespace fn
{
    using System;
    using System.Diagnostics;

    internal static class TestDelegates
    {
        private delegate void LoggerDelegate(string message);
        private delegate string LoggerWithResponseDelegate(string message);

        internal static void Run()
        {
            RunVoidDelegates();
            RunResponseDelegates();
        }

        private static void RunVoidDelegates()
        {
            LoggerDelegate consoleLoggerHandler = ConsoleLogger;
            LoggerDelegate debugLoggerHandler = DebugLogger;
            LoggerDelegate allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            consoleLoggerHandler("This goes to the console");
            debugLoggerHandler("This goes to the debug");
            allConsoleHandlers("this goes to all");
        }

        private static void RunResponseDelegates()
        {
            LoggerWithResponseDelegate consoleLoggerHandler = ConsoleLoggerWithResponse;
            LoggerWithResponseDelegate debugLoggerHandler = DebugLoggerWithResponse;
            LoggerWithResponseDelegate allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            var consoleResponse = consoleLoggerHandler("This goes to the console");
            var debugResponse = debugLoggerHandler("This goes to the debug");
            var lastResponse = allConsoleHandlers("this goes to all");
        }

        private static void ConsoleLogger(string message) => Console.WriteLine(message);

        private static void DebugLogger(string message) => Debug.WriteLine(message);

        private static string ConsoleLoggerWithResponse(string message)
        {
            Console.WriteLine(message);
            return "Logged to console";
        }

        private static string DebugLoggerWithResponse(string message)
        {
            Debug.WriteLine(message);
            return "Logged to debug";
        }
    }
}
