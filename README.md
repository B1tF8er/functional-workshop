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
(int) -> ((int) -> (int))
int -> int -> int
```

## Delegates
```csharp
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
```

## Actions
```csharp
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
```

## Funcs
```csharp
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
```
