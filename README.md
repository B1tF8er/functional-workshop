# Functional concepts applied using C#

### How to build and run
See the [scripts](https://github.com/B1tF8er/functional-workshop/tree/master/scripts)

## Arrow notation
Arrow notations are the preferred syntax to read the signature of a function
in Functional Programming lingo and they are right associative:

`(int) -> ((int) -> (int))` **EQUALS** `int -> int -> int`

That can be read as a function that takes 2 integers as arguments
and returns another integer. In C# we have three ways to create that
e.g:

`Delegates = private delegate int ExampleDelegate(int left, int right)`
left and right are the first two ints in the signature
and the return type is the third int.

`Funcs = private Func<int, int, int> ExampleFunc`
it can be read as in the example the first two are
the arguments, and the last one is the return type.

`Actions = private Action<int, int, int> ExampleAction`
are a special case, it is read as Funcs but
they always return void, so this is read as a function
that takes 3 ints as arguments and returns void.

## Delegates

Delegates are the most basic form to use functions
as first-class citizens in C#.

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

Actions are an extension of the delegates that the .NET framework
gives us. These never return a value therefore are always `void`, and can
take up to sixteen generic parameters as input.

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

Funcs are another extension of the delegates that the .NET framework
gives us. But these do have a `generic` return type, and can
take up to sixteen generic parameters as input. The key difference
between `Actions` and `Funcs` is that the last parameter of a `Func` is
the return type.

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

Named after mathematician Haskell Curry, currying is the process of transforming
an n-ary function f that takes arguments t1, t2, ..., tn
into a unary function that takes t1 and yields a new function that takes t2,
and so on, ultimately returning the same result as f once the arguments
have all been given. In other words, an n-ary function with signature 

`(T1, T2, ..., Tn) → R`

when curried, has signature

`(T1) (T2) ... (Tn) → R `

By itself this technique is pretty useless, but it does enable the use
of partial application.

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

Providing a function with fewer arguments than it expects is called
Partial Application of functions. In order to produce more reusable functions
and create better abstractions we need a mechanism
to preset some of the arguments of a function. Partial application is the technique
used to transform functions of higher arity (functions that take multiple arguments)
into multiple functions that take less arguments.

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

## Lazy Evaluation / Deferred Execution

Laziness in computing means deferring a computation until
its result is needed. This is beneficial when the computation
is expensive and its result may not be needed.

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

## Extension Methods

Extension methods enable you to "add" methods to existing types
without creating a new derived type, recompiling, or otherwise
modifying the original type.

- They are just static methods in a static class
- Enable Open/Close principle
- Enable method chaining

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

## Smart Constructors

Smart constructors are just functions that build values
of the required type, but perform some extra checks
when the value is constructed. In this case it is a
good practice to set the constructor of a Reference Type
as private and expose a function to allow its instantiation.

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
        var person = SmartPerson.Create("George", 30);
        WriteLine($"Valid person data so all goes well and we can print {person}");
    }

    private static void InvalidName()
    {
        try
        {
            var person = SmartPerson.Create(null, 30);
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
            var person = SmartPerson.Create("George", -1);
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

    private SmartPerson(string name, int age)
    {
        Name = name;
        Age = age;
    }

    internal static SmartPerson Create(string name, int age)
    {
        GuardName(name);
        GuardAge(age);

        return new SmartPerson(name, age);
    }

    private static void GuardName(string name)
    {
        Func<bool> isNull = () => name is null;
        Func<bool> isEmpty = () => string.IsNullOrEmpty(name);
        Func<bool> isWhiteSpace = () => string.IsNullOrWhiteSpace(name);

        if (isNull() || isEmpty() || isWhiteSpace())
            throw new ArgumentNullException(nameof(name), "Name can't be null, empty or white spaces");
    }

    private static void GuardAge(int age)
    {
        if (age < 0 || age > 120)
            throw new ArgumentOutOfRangeException(nameof(age), "Age is not in valid range");
    }

    public override string ToString() => $"My name is {Name} and I am {Age} years old";
}
```

## Avoid Primitive Obsession

Primitive obsession is the use of base types like:
`string, int, bool, double, datetime` etc;
to represent complex types like a person's Age, Name, Email.

To avoid this we should create small objects to
represent them and some of the benefits we get from that are:

- Validation in one single place (the small class smart constructor)
- Immutability
- Self Validation
- Value Equality

```csharp
using System;
using System.Text.RegularExpressions;
using static System.Console;

internal static class TestAvoidPrimitiveObsession
{
    internal static void Run()
    {
        HappyPathEmail();
        InvalidEmail();
        NullEmail();

        HappyPathAge(); 
        InvalidAge();
    }

    internal static void HappyPathEmail()
    {
        var emailOne = Email.Create("test1@test.com");
        var emailTwo = Email.Create("test2@test.com");
        var emailThree = Email.Create("test1@test.com");
        string fromEmail = Email.Create("test3@test.com");
        Email fromString = "test4@test.com";

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

    private static void HappyPathAge()
    {
        var ageOne = Age.Create(30);
        var ageTwo =  Age.Create(25);
        var ageThree = Age.Create(30);
        int fromAge = Age.Create(20);
        Age fromInt = 30;

        WriteLine($"{ageOne} == {ageTwo} ? {(ageOne.Equals(ageTwo))}");
        WriteLine($"{ageOne} == {ageThree} ? {(ageOne.Equals(ageThree))}");
    }

    private static void InvalidAge()
    {
        try
        {
            var age = Age.Create(130);
        }
        catch (ArgumentOutOfRangeException ex)
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

        public static implicit operator Email(string email) => Create(email);

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

    private class Age
    {
        private readonly int value;

        private Age(int value) => this.value = value;

        internal static Age Create(int age)
        {
            GuardAge(age);
            return new Age(age);
        }

        private static void GuardAge(int age)
        {
            if (age < 0 || age > 120)
                throw new ArgumentOutOfRangeException(nameof(age), "Age is not in valid range");
        }

        public static implicit operator int(Age age) => age.value;

        public static implicit operator Age(int age) => Create(age);

        public override bool Equals(object obj)
        {
            var age = obj as Age;

            if (ReferenceEquals(age, null))
                return false;

            return this.value == age.value;
        }

        public override int GetHashCode() => value.GetHashCode();

        public override string ToString() => $"{value}";
    }

    private static class Constants
    {
        internal static string EmailPattern => @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";
    }
}
``` 

## Generics

A generic allows you to define a class with placeholders
for the type of its fields, methods, parameters, etc.
Generics replace these placeholders with some specific
type at compile time.

- Used to maximize code reuse, type safety, and performance
- Its most common use is to create collection classes
- Constraints work to limit the scope of the types

```csharp
using System;
using System.Collections.Generic;
using static System.Console;

internal static class TestGenerics
{
    internal static void Run() => CreateGenerics();

    private static void CreateGenerics()
    {
        new List<object>
        {
            Generic<int>.Create(42),
            Generic<long>.Create(42L),
            Generic<float>.Create(42.5F),
            Generic<double>.Create(42D),
            Generic<decimal>.Create(42.5M),
            Generic<bool>.Create(false),
            Generic<DateTime>.Create(DateTime.Now),
            Generic<byte>.Create(0x0042),
            Generic<char>.Create('A'),
            Generic<string>.Create("George"),
            Generic<Func<int, int>>.Create(x => x + x),
            Generic<Action<int, int>>.Create((x, y) => WriteLine(x + y)),
            Generic<dynamic>.Create(7.5F + 35L),
            Generic<object>.Create(new { a = 42 })
        }
        .ForEach(WriteLine);      
    }

    private class Generic<T>
    {
        private T GenericReadOnlyProperty { get; }

        private Generic(T genericArgument) => GenericReadOnlyProperty = genericArgument;

        internal static Generic<T> Create(T genericValue) => new Generic<T>(genericValue);

        public override string ToString() => $"{GenericReadOnlyProperty} - {typeof(T)}";
    }
}
```

## Useful links

1. Delegates
    - https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/delegate
    - https://www.geeksforgeeks.org/c-sharp-delegates/
    - https://www.tutorialspoint.com/csharp/csharp_delegates.htm
2. Funcs
    - https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=netframework-4.8
    - https://www.geeksforgeeks.org/c-sharp-func-delegate/
3. Actions
    - https://docs.microsoft.com/en-us/dotnet/api/system.action?view=netframework-4.8
    - https://www.geeksforgeeks.org/c-sharp-action-delegate/
4. Curried Functions
    - https://codeblog.jonskeet.uk/2012/01/30/currying-vs-partial-function-application/
    - https://www.geeksforgeeks.org/higher-order-functions-currying/
    - https://weblogs.asp.net/dixin/functional-csharp-higher-order-function-currying-and-first-class-function
5. Partial Application
    - https://marcclifton.wordpress.com/2017/06/23/partial-application-and-currying-in-c-clearing-the-fog/
    - http://mikehadlow.blogspot.com/2015/09/partial-application-in-c.html
6. Lazy Evaluation / Deferred Execution
    - https://blogs.msdn.microsoft.com/pedram/2007/06/02/lazy-evaluation-in-c/
    - https://zohaib.me/lazy-evaluation-in-c/
    - https://xosfaere.wordpress.com/2010/03/21/lazy-evaluation-in-csharp/
7. Extension Methods
    - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods
    - https://www.geeksforgeeks.org/extension-method-in-c-sharp/
8. Smart Constructors
    - https://davemateer.com/2019/03/12/Functional-Programming-in-C-Sharp-Expressions-Options-Either
    - https://markkarpov.com/post/smart-constructors-that-cannot-fail.html
    - https://news.ycombinator.com/item?id=10295487
9. Avoid primitive obsession
    - https://refactoring.guru/smells/primitive-obsession
    - https://enterprisecraftsmanship.com/2015/03/07/functional-c-primitive-obsession/
10. Generics
    - https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
    - https://www.tutorialsteacher.com/csharp/csharp-generics
    - https://www.geeksforgeeks.org/c-sharp-generics-introduction/
