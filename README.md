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
```

## Actions
```csharp
using System;
using System.Diagnostics;

public static class TestActions
{
    public Action<string> LoggerAction(string message);

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