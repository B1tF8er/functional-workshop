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
Arrow notations, this is right assosiative
(int) -> ((int) -> (int)) `EQUALS` int -> int -> int

That can be read as a function that takes 2 integers as arguments
and returns another integer in C# we have three ways to create that
e.g :

`Delegates = private delegate int ExampleDelegate(int left, int right)`
left and right are the first two ints in the signature
and the return type is the third int

`Funcs = private Func<int, int, int> ExampleFunc`
it can be read as in the example the first two are
the arguments, and the last one is the return type

`Actions = private Action<int, int, int> ExampleAction`
are a special case, it is read as Funcs but
they always return void, so this is read as a function
that takes 3 ints as arguments and returns void

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

## Curried Functions
```csharp
using System;
using static System.Console;
					
internal class TestCurriedFunctions
{
    internal static void Run()
    {
        WriteLine(Greeter("Hello")("World"));
        WriteLine(Sum(5)(18));
    }

    private static Func<string, Func<string, string>> Greeter =>
        (firstName) => (lastName) => $"{firstName} {lastName}";

    private static Func<int, Func<int, int>> Sum =>
        (lhs) => (rhs) => lhs + rhs; 
}
```

## Partial Application
```csharp
using System;
using static System.Console;
					
internal class TestPartialApplication
{
    internal static void Run()
    {
        WriteLine(MaleFormalGreeter("World"));
        WriteLine(FemaleFormalGreeter("World"));
        WriteLine(PlusFive(5));
        WriteLine(PlusFive(18));
    }

    private static Func<string, Func<string, string>> Greeter =>
        (firstName) => (lastName) => $"{firstName} {lastName}";

    private static Func<string, string> MaleFormalGreeter =>
        (lastName) => Greeter("Mr.")(lastName);

    private static Func<string, string> FemaleFormalGreeter =>
        (lastName) => Greeter("Ms.")(lastName);

    private static Func<int, Func<int, int>> Sum =>
        (lhs) => (rhs) => lhs + rhs; 

    private static Func<int, int> PlusFive =>
        (factor) => Sum(factor)(5);
}
```

### Lazy Evaluation / Deferred Execution 
```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

internal class TestLazyEvaluation
{
    private static IEnumerable<int> LazyNumbers
    {
        get
        {
            yield return 1;
            yield return 2;
            yield return 3;
            yield return 4;
            yield return 5;
            yield return 6;
            yield return 7;
            yield return 8;
            yield return 9;
            yield return 10;
        }
    } 

    internal static void Run()
    {
        LazinessWithFunctions();
        LazinessWithEnumerators();
    }

    private static void LazinessWithFunctions()
    {
        var random = new Random();
        Func<bool> isLessThanZeroPointFive = () => random.NextDouble() < 0.5;
        Func<int> left = () => 23;
        Func<int> right = () => 42;
        Func<int> lazyExecuted = () => isLessThanZeroPointFive()
            ? left()
            : right();

        // all the previous code is evaluated
        // until we call lazyExecuted function
        WriteLine(lazyExecuted());
    }

    private static void LazinessWithEnumerators()
    {
        var skipFour = LazyNumbers.Skip(4);
        var takeFour = skipFour.Take(4);

        // all the previous code is evaluated
        // until we enumerate the collection in a foreach
        foreach (var number in takeFour)
            WriteLine(number);
    }
}
```

### Extension Methods
```csharp
using System;
using static System.Console;

internal class TestExtensionMethods
{
    internal static void Run()
    {
        var person = new Person("George", 30);
        WriteLine(person);
        WriteLine($"Days lived from person object {person.DaysLived()}");
        WriteLine($"Days lived from Age property {person.Age.DaysLived()}");
    }
}

internal class Person
{
    internal string Name { get; }
    internal int Age { get; }

    internal Person(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public override string ToString() => $"My name is {Name} and I am {Age} years old";
}

internal static class PersonExtensions
{
    internal static int DaysLived(this Person person) =>
        person.Age * 365;

    internal static int DaysLived(this int age) => age * 365;
}
```

### Smart Constructors
```csharp
using System;
using static System.Console;

internal class TestSmartConstructors
{
    internal static void Run()
    {
        HappyPath();
        InvalidName();
        InvalidAge();
    }

    private static void HappyPath()
    {
        var person = new SmartPerson("George", 30);
        WriteLine($"Valid person data so all goes well and we can print {person}");
    }

    private static void InvalidName()
    {
        try
        {
            var person = new SmartPerson(null, 30);
        }
        catch (ArgumentNullException ex)
        {
            WriteLine($"{ex.Message}");
        }
    }

    private static void InvalidAge()
    {
        try
        {
            var person = new SmartPerson("George", -1);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            WriteLine($"{ex.Message}");
        }
    }
}

internal class SmartPerson
{
    internal string Name { get; }
    internal int Age { get; }

    internal SmartPerson(string name, int age)
    {
        GuardName(name);
        GuardAge(age);

        Name = name;
        Age = age;
    }

    private void GuardName(string name)
    {
        Func<bool> isNull = () => name is null;
        Func<bool> isEmpty = () => string.IsNullOrEmpty(name);
        Func<bool> isWhiteSpace = () => string.IsNullOrWhiteSpace(name);

        if (isNull() || isEmpty() || isWhiteSpace())
            throw new ArgumentNullException(nameof(name), "Name can't be null, empty or white spaces");
    }

    private void GuardAge(int age)
    {
        if (age < 0 || age > 120)
            throw new ArgumentOutOfRangeException(nameof(age), "Age is not in valid range");
    }

    public override string ToString() => $"My name is {Name} and I am {Age} years old";
}
```

### Avoid Primitive Obsession
```csharp
using System;
using System.Text.RegularExpressions;
using static System.Console;

internal static class TestAvoidPrimitiveObsession
{
    internal static void Run()
    {
        HappyPath();
        InvalidEmail();
        NullEmail();
    }

    internal static void HappyPath()
    {
        var emailOne = Email.Create("test1@test.com");
        var emailTwo = Email.Create("test2@test.com");
        var emailThree = Email.Create("test1@test.com");
        WriteLine(emailOne);
        WriteLine(emailTwo);
        WriteLine(emailThree);
        WriteLine($"{emailOne} == {emailTwo} ? {(emailOne.Equals(emailTwo))}");
        WriteLine($"{emailOne} == {emailThree} ? {(emailOne.Equals(emailThree))}");
    }

    private static void InvalidEmail()
    {
        try
        {
            var email = Email.Create("test1@test");
        }
        catch (ArgumentException ex)
        {
            WriteLine(ex.Message);
        }
    }

    private static void NullEmail()
    {
        try
        {
            var email = Email.Create(null);
        }
        catch (ArgumentNullException ex)
        {
            WriteLine(ex.Message);
        }
    }

    private class Email
    {
        private readonly string value;

        private Email(string value) => this.value = value;

        internal static Email Create(string email)
        {
            GuardEmail(email);
            return new Email(email);
        }

        private static void GuardEmail(string email)
        {
            if (email is null)
                throw new ArgumentNullException(nameof(email), "Email address cannot be null");

            var match = Regex.Match(
                email,
                Constants.EmailPattern,
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            if (!match.Success)
                throw new ArgumentException("Invalid Email address format", nameof(email));
        }

        public static implicit operator string(Email email) => email.value;

        public override bool Equals(object obj)
        {
            var email = obj as Email;

            if (ReferenceEquals(email, null))
                return false;

            return this.value == email.value;
        }

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => value;
    }

    private static class Constants
    {
        internal static string EmailPattern => @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    }
}
``` 
