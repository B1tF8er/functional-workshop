# Functional concepts applied using C#

### How to build and run using `dotnet cli`
```
dotnet build src/fn -c Release
dotnet run -p src/fn -c Release
``` 

### How to build and run using `docker cli`
```
# build image and tag it
docker build -t fn-image .
# run image using tag ^^
docker run --rm fn-image
```

## Arrow notation
```
Arrow notations, this is right assosiative
(int) -> ((int) -> (int)) EQUALS int -> int -> int

That can be read as a function that takes 2 integers as arguments
and returns another integer e.g:

// left and right are the first two ints in the signature
// and the return type is the third int
private delegate int ExampleDelegate(int left, int right);

// in the case of Func, it can be read as in the example the first two are
// the arguments, and the last one is the return type
private Func<int, int, int> ExampleFunc;
```

## Delegates
```csharp
using System;
using System.Diagnostics;

internal static class TestDelegates
{
    // string -> ()
    private delegate void LoggerDelegate(string message);
    // string -> string
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
```

## Actions
```csharp
using System;
using System.Diagnostics;

internal static class TestActions
{
    internal static void Run()
    {
        // string -> ()
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
```

## Funcs
```csharp
using System;
using System.Diagnostics;

internal static class TestFuncs
{
    internal static void Run()
    {
        // string -> string
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
```
