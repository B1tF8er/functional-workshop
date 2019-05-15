namespace fn
{
    using System;
    using System.Diagnostics;

    public static class TestDelegates
    {
        public delegate void LoggerDelegate(string message);
        public delegate string LoggerWithResponseDelegate(string message);

        public static void Run()
        {
            RunVoidDelegates();
            RunResponseDelegates();
        }

        public static void RunVoidDelegates()
        {
            LoggerDelegate consoleLoggerHandler = ConsoleLogger;
            LoggerDelegate debugLoggerHandler = DebugLogger;
            LoggerDelegate allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            consoleLoggerHandler("This goes to the console");
            debugLoggerHandler("This goes to the debug");
            allConsoleHandlers("this goes to all");
        }

        public static void RunResponseDelegates()
        {
            LoggerWithResponseDelegate consoleLoggerHandler = ConsoleLoggerWithResponse;
            LoggerWithResponseDelegate debugLoggerHandler = DebugLoggerWithResponse;
            LoggerWithResponseDelegate allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            var consoleResponse = consoleLoggerHandler("This goes to the console");
            var debugResponse = debugLoggerHandler("This goes to the debug");
            var lastResponse = allConsoleHandlers("this goes to all");
        }

        public static void ConsoleLogger(string message) => Console.WriteLine(message);

        public static void DebugLogger(string message) => Debug.WriteLine(message);

        public static string ConsoleLoggerWithResponse(string message)
        {
            Console.WriteLine(message);
            return "Logged to console";
        }

        public static string DebugLoggerWithResponse(string message)
        {
            Debug.WriteLine(message);
            return "Logged to debug";
        }
    }
}
