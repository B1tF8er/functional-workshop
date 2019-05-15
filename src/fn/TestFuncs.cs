namespace fn
{
    using System;
    using System.Diagnostics;

    public static class TestFuncs
    {
        public static void Run()
        {
            Func<string, string> consoleLoggerHandler = ConsoleLogger;
            Func<string, string> debugLoggerHandler = DebugLogger;
            Func<string, string> allConsoleHandlers = consoleLoggerHandler + debugLoggerHandler;

            var consoleResponse = consoleLoggerHandler("This goes to the console");
            var debugResponse = debugLoggerHandler("This goes to the debug");
            var lastResponse = allConsoleHandlers("this goes to all");
        }

        public static string ConsoleLogger(string message)
        {
            Console.WriteLine(message);
            return "Logged to console";
        }

        public static string DebugLogger(string message)
        {
            Debug.WriteLine(message);
            return "Logged to debug";
        }
    }
}
