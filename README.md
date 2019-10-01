# Functional Programming Concepts Applied Using C# #

You have been curious about all the fuzz in recent years
about functional programming? but have not found how
to apply it in your day to day work. Search no more.
This post aims to give you a cheatsheet with functional concepts
and techniques used in the .NET framework.

## How To Build And Run ##

See the [scripts](https://github.com/B1tF8er/functional-workshop/tree/master/scripts)

## Arrow Notation ##

Arrow notations are the preferred syntax to read the signature of a function
in Functional Programming lingo and they are right associative,

`(int) -> ((int) -> (int))` **EQUALS** `int -> int -> int`

That can be read as a function that takes two integers as arguments
and returns another integer. In C# we have three ways to create it,

`Delegates = private delegate int ExampleDelegate(int left, int right)`
left and right are the first two ints in the signature
and the return type is the third int.

```csharp
using System;

public class TestDelegates
{
    // int -> int -> int
    public delegate int MyFirstSumDelegate(int x, int y);

    // int -> int -> int -> ()
    public delegate void MySecondSumDelegate(int x, int y, int z);

    public static void Main()
    {
        MyFirstSumDelegate myFirstSumDelegate = SumTwo;

        var delegateResult = myFirstSumDelegate(1, 2);

        Console.WriteLine(delegateResult);

        MySecondSumDelegate mySecondSumDelegate = SumThree;
        mySecondSumDelegate(1, 2, 3);
    }

    public static int SumTwo(int a, int b)
    {
        return a + b;
    }

    public static void SumThree(int a, int b, int c)
    {
        var result = a + b + c;

        Console.WriteLine(result);
    }
}
```

`Funcs = private Func<int, int, int> ExampleFunc`
it can be read as in the example, the first two ints
are the arguments, and the last one, is the return type.

```csharp
using System;

public class TestFuncs
{
    // int -> int -> int
    public delegate int MySumDelegate(int x, int y);

    public static void Main()
    {
        MySumDelegate mySumDelegate = Sum;
        // int -> int -> int
        Func<int, int, int> mySumFunc = Sum;

        var delegateResult = mySumDelegate(1, 2);
        var funcResult = mySumFunc(1, 2);

        Console.WriteLine(delegateResult);
        Console.WriteLine(funcResult);
    }

    public static int Sum(int a, int b)
    {
        return a + b;
    }
}
```

`Actions = private Action<int, int, int> ExampleAction`
are a special case, they read as Funcs but
they always return void. It is read as a function
that takes 3 ints as arguments and returns void.

```csharp
using System;

public class TestActions
{
    // int -> int -> int -> ()
    public delegate void MySumDelegate(int x, int y, int z);

    public static void Main()
    {
        MySumDelegate mySumDelegate = Sum;
        // int -> int -> int -> ()
        Action<int, int, int> mySumAction = Sum;

        mySumDelegate(1, 2, 3);
        mySumAction(1, 2, 3);
    }

    public static void Sum(int a, int b, int c)
    {
        var result = a + b + c;

        Console.WriteLine(result);
    }
}
```

## First Class Functions ##

When functions become first class citizens in a language. Means they
can,

- Be used as parameters in other functions
- Be used as the return type of a function
- They can be assigned to variables
- Be stored in a collection

```csharp
using System;
using System.Collections.Generic;
using static System.Console;
// you could use an alias to simplify types
// int -> int -> int
using Adder = System.Func<int, int, int>;

public class TestFirstClassFunctions
{
    // Assigned to a variable
    private readonly static Adder adder = (x, y) => x + y;

    public static void Run()
    {
        // Stored in a collection
        var functions = new Dictionary<string, Adder>
        {
            { "One", adder },
            { "Two", Generator(adder) },
        };

        var index = 1;

        foreach (var f in functions)
            WriteLine($"{f.Key} => {f.Value.Invoke(index, ++index)}");
    }

    // As return type and as parameter
    private static Adder Generator(Adder adder)
        => adder;
}
```

## Higher Order Functions ##

Higher Order Functions or `HOF`s are functions that take
other functions as inputs or return a function as output, or both.

```csharp
using System;
using static System.Console;
using Subtractor = System.Func<int, int, int>;

public class TestHigherOrderFunctions
{
    private readonly static Subtractor subtractor = (x, y) => x - y;

    public static void Run() =>
        WriteLine(Generator(subtractor)(44, 2));

    // This is a HOF
    // takes a functions as argument
    // and returns a function
    private static Subtractor Generator(Subtractor subtractor)
        => subtractor;
}
```

## Pure Functions ##

It is said that a function is pure when its output
depends entirely on the input parameters. And cause
no side effects. Therefore they improve,

- Testability
- Correctness
- Readability

With no side effects there are no surprises. our code
does what it says, no more. and the cognitive load
to understand the code is less

```csharp
using static System.Console;

public class TestPureFunctions
{
    public static void Run()
    {
        var ab = Join("a", "b");
        var abab = Join(ab, ab);

        WriteLine(ab);
        WriteLine(abab);
    }

    private static string Join(string lhs, string rhs)
        => $"{lhs}{rhs}";
}
```

## Side Effects ##

To clarify this definition, we must define exactly
what a side effect is. A function is said to have side effects
if it does any of the following,

- Mutates global state — “Global” here means any state
    that’s visible outside of the function’s scope.
    For example, a private instance field is considered global
    because it’s visible from all methods within the class.
- Mutates its input arguments
- Throws exceptions
- Performs any I/O operation — This includes any interaction
    between the program and the external world,
    including reading from or writing to the console,
    the filesystem, or a database, and interacting with
    any process outside the application’s boundary.

```csharp
using static System.Console;

public class TestSideEffects
{
    public static void Run()
    {
        var ab = JoinWithLogs("a", "b");
        var abab = JoinWithLogs(ab, ab);

        WriteLine(ab);
        WriteLine(abab);
    }

    private static string JoinWithLogs(string lhs, string rhs)
    {
        // Here we have the side effect of writing to console
        // but it could be any I/O operation.
        WriteLine($"lhs before: {lhs}");
        WriteLine($"rhs before: {rhs}");

        var result = $"{lhs}{rhs}";

        WriteLine($"result: {result}");
        return result;
    }
}
```

## Immutability ##

In `object-oriented` and `functional` programming,
an immutable object (unchangeable object) is an object whose state
cannot be modified after it is created. In C# we can use
this [Nuget](https://www.nuget.org/packages/System.Collections.Immutable/) package to enforce it.
And we can also follow some practices that I will show you next

```csharp
using System;
using System.Collections.Generic;
using System.Collections.Immutable; // Nuget package
using System.Linq;
using static System.Console;

public static class TestImmutability
{
    // readonly fields are immutable
    // once they are initialized. at run-time
    private static readonly string dashes = new string('-', 50);
    private static readonly IEnumerable<int> numbers = Enumerable.Range(1, int.MaxValue);

    // const fields are immutable as well. at compile-time
    private const int Ten = 10;
    private const int Six = 6;
    private const int Three = 3;

    public static void Run()
    {
        List();
        Stack();
        Queue();
    }

    private static void List()
    {
        Separator("Immutable List");

        var list = ImmutableList<int>.Empty;
        Action<int> listAdder = number => list = list.Add(number * Ten);

        numbers
            .Skip(100)
            .Take(10)
            .AddTo(listAdder);

        list.PrintToConsole();
    }

    private static void Stack()
    {
        Separator("Immutable Stack");

        var stack = ImmutableStack<int>.Empty;
        Action<int> stackAdder = number => stack = stack.Push(number * Six);

        numbers
            .SkipWhile(n => n < 200)
            .TakeWhile(n => n < 220)
            .Where(n => n % 3 == 0)
            .AddTo(stackAdder);

        stack.PrintToConsole();
    }

    private static void Queue()
    {
        Separator("Immutable Queue");

        var queue = ImmutableQueue<int>.Empty;
        Action<int> queueAdder = number => queue = queue.Enqueue(number * Three);

        numbers
            .SkipWhile(n => n < 1000)
            .TakeWhile(n => n < 1011)
            .Where(n => n % 2 == 0)
            .AddTo(queueAdder);

        queue.PrintToConsole();
    }

    private static void AddTo<T>(this IEnumerable<T> items, Action<T> addToOtherCollection)
    {
        foreach (var item in items)
            addToOtherCollection(item);
    }

    private static void PrintToConsole<T>(this IEnumerable<T> items)
    {
        foreach (var item in items)
            WriteLine(item);
    }

    private static void Separator(this string text)
    {
        WriteLine(dashes);
        WriteLine(text);
        WriteLine(dashes);
    }
}
```

## Arity Of Functions ##

In logic, mathematics, and computer science, the arity of a function or operation
is the number of arguments or operands that the function takes,

- `Nullary`, takes no arguments
- `Unary`, takes one argument
- `Binary`, takes two arguments
- `Ternary`, takes three arguments
- `n-ary`, takes *n* arguments

```csharp
using System.Linq;
using static System.Console;

public class TestArity
{
    public static void Run()
    {
        WriteLine(Nullary());
        WriteLine(Unary(1));
        WriteLine(Binary(1, 2));
        WriteLine(Ternary(1, 2, 3));
        WriteLine(N_Ary(1, 2, 3, 4));
        WriteLine(N_Ary(Enumerable.Range(1, 16).ToArray()));
        WriteLine(N_Ary(Enumerable.Range(1, 100).ToArray()));
    }

    private static string Nullary()
        => "No arguments";

    private static string Unary(int one)
        => $"One argument: {one}";

    private static string Binary(int one, int two)
        => $"Two arguments: {one} and {two}";

    private static string Ternary(int one, int two, int three)
        => $"Three arguments: {one}, {two} and {three}";

    private static string N_Ary(params int[] arguments)
        => $"N arguments: {arguments.Length}";
}
```

## Referential Transparency ##

In functional programming, referential transparency is generally
defined as the fact that an expression, in a program, may be replaced
by its value (or anything having the same value) without changing the result
of the program. This implies that methods should always return the same value
for a given argument (purity of functions), without having any other effect.

This functional programming concept also applies to imperative programming, though,
and can help you make your code clearer.

```csharp
using static System.Console;

public class TestReferentialTransparency
{
    private const int Two = 2;
    private const int Nine = 9;
    private const int Sixteen = 16;
    private const int Eighteen = 18;

    public static void Run()
    {
        OperationsWithReferentialTransparency();
        OperationsWithoutReferentialTransparency();
    }

    private static void OperationsWithReferentialTransparency()
    {
        // YES - Sum(16, 2) == 18 and Mul(9, 2) == 18
        // and the result of the program will be the same
        var sum = Sum(Sixteen, Two) == Eighteen;
        var mul = Mul(Nine, Two) == Eighteen;
    }

    private static void OperationsWithoutReferentialTransparency()
    {
        // NO - Sum(16, 2) == 18 and Mul(9, 2) == 18
        // but WE ARE NOT logging to the console the result
        var sum = SumLog(Sixteen, Two) == Eighteen;
        var mul = MulLog(Nine, Two) == Eighteen;
    }

    private static int Sum(int a, int b) => a + b;

    private static int Mul(int a, int b) => a * b;

    private static int SumLog(int a, int b)
    {
        var result = a + b;
        WriteLine($"Returning {result}");
        return result;
    }

    private static int MulLog(int a, int b)
    {
        var result = a * b;
        WriteLine($"Returning {result}");
        return result;
    }
}
```

## Delegates ##

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

## Actions ##

Actions are sugar syntax to create delegates that the .NET framework
gives us. These never return a value therefore are always `void`, and can
take up to sixteen generic parameters as input.

- The `void` return type in FP is called `unit`
- `unit` is expressed as `()` in arrow notation

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

## Funcs ##

Funcs are another sugar syntax to create delegates that the .NET framework
gives us. But these do have a `generic` return type, and can
take up to sixteen generic parameters as input. The key difference
between `Actions` and `Funcs` is that the last parameter of a `Func`
which is the return type.

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

## Curried Functions ##

Named after mathematician Haskell Curry, currying is the process of transforming
an `n-ary` function f that takes arguments t1, t2, ..., tn
into a `unary` function that takes t1 and yields a new function that takes t2,
and so on, ultimately returning the same result as f once the arguments
have all been given. In other words, an n-ary function with signature

`(T1, T2, ..., Tn) → R`

when curried has signature

`(T1) (T2) ... (Tn) → R`

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

## Partial Application ##

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

## Lazy Evaluation / Deferred Execution ##

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

## Extension Methods ##

Extension methods enable you to "add" methods to existing types
without creating a new derived type, recompiling, or otherwise
modifying the original type.

- They are just static methods in a static class
- Enable the Open/Close principle
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

## Smart Constructors ##

Smart constructors are just functions that build values
of the required type. Yet perform some extra checks
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

    private SmartPerson(string name, int age) =>
        (Name, Age) = (name, age);

    internal static SmartPerson Create(string name, int age)
    {
        GuardName(name);
        GuardAge(age);

        return new SmartPerson(name, age);
    }

    private static void GuardName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
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

## Avoid Primitive Obsession ##

Primitive obsession is the use of base types like:
`string, int, bool, double, datetime` etc
to represent complex types like a person's Age, Name or Email.

To avoid this we should create small objects to
represent them and some of the benefits we get from that are,

- Validation in one single place (the small class smart constructor)
- Immutability
- Self-Validation
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

        // Implicit conversion using operators
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

        // Implicit conversion using operators
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

## Generics ##

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
    internal static void Run()
    {
         CreateGenerics();
         CreateGenericsWithConstraints();
    }

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

    private static void CreateGenericsWithConstraints()
    {
        new List<object>
        {
            GenericWithConstraints<int, object>.Create(42, new { A = 42 }),
            GenericWithConstraints<long, object>.Create(42L, new { B = 42L }),
            GenericWithConstraints<float, object>.Create(42F, new { C = 42F }),
            GenericWithConstraints<double, object>.Create(42D, new { D = 42D }),
            GenericWithConstraints<decimal, object>.Create(42M, new { E = 42M }),
            GenericWithConstraints<bool, object>.Create(true, new { F = true }),
            GenericWithConstraints<DateTime, object>.Create(DateTime.Now, new { G = DateTime.Now }),
            GenericWithConstraints<byte, object>.Create(0x0042, new { H = 0x0042 }),
            GenericWithConstraints<char, object>.Create('J', new { I = 'J' })
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

    private class GenericWithConstraints<T, U>
        where T : struct
        where U : new()
    {
        private T @Struct { get; }

        private U @Object { get; }

        private GenericWithConstraints(T @struct, U @object) =>
            (@Struct, @Object) = (@struct, @object);

        internal static GenericWithConstraints<T, U> Create(T @struct, U @object) =>
            new GenericWithConstraints<T, U>(@struct, @object);

        public override string ToString() =>
            $"{@Struct} - {typeof(T)} && {@Object} - {typeof(U)} ";
    }
}
```

## LINQ ##

LINQ stands for `L`anguage `IN`tegrated `Q`uery. It is a functional library
and offers implementations for many common operations on lists
or, more generally, on “sequences.” As instances of IEnumerable should technically be called,
the most common of which are mapping, sorting, and filtering

- Makes extensive use of higher-order functions as arguments
- Uses method syntax or query syntax

```csharp
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

internal static class TestLINQ
{
    private static readonly string Dashes = new string('-', 40);

    private static readonly IEnumerable<char> Alphabet = Enumerable
        .Range('a', 'z' - 'a' + 1)
        .Select(i => (Char)i);

    private static readonly IEnumerable<int> LazyNumbers = Enumerable.Range(0, 50);

    private static IEnumerable<IEnumerable<int>> NestedLazyNumbers
    {
        get
        {
            yield return LazyNumbers;
            yield return LazyNumbers;
        }
    }

    internal static void Run()
    {
        Map();
        Filter();
        Zip();
        Take();
        Skip();
        TakeWhile();
        SkipWhile();
        OrderBy();
        OrderByDescending();
        FoldRight();
        FoldLeft();
        Bind();
    }

    private static void Map()
    {
        LazyNumbers
            .Select(number => number * 10)
            .Print(nameof(Map));
    }

    private static void Filter()
    {
        LazyNumbers
            .Where(number => number > 5)
            .Print(nameof(Filter));
    }

    private static void Zip()
    {
        LazyNumbers
            .Zip(Alphabet, (number, letter) => $"{number} --> {letter}")
            .Print(nameof(Zip));
    }

    private static void Take()
    {
        LazyNumbers
            .Take(10)
            .Print(nameof(Take));
    }

    private static void Skip()
    {
        LazyNumbers
            .Skip(40)
            .Print(nameof(Skip));
    }

    private static void TakeWhile()
    {
        LazyNumbers
            .TakeWhile(number => number < 10)
            .Print(nameof(TakeWhile));
    }

    private static void SkipWhile()
    {
        LazyNumbers
            .SkipWhile(number => number < 40)
            .Print(nameof(SkipWhile));
    }

    private static void OrderBy()
    {
        LazyNumbers
            .OrderBy(number => number)
            .Print(nameof(OrderBy));
    }

    private static void OrderByDescending()
    {
        LazyNumbers
            .OrderByDescending(number => number)
            .Print(nameof(OrderByDescending));
    }

    private static void FoldRight()
    {
        LazyNumbers
            .Take(10)
            .Aggregate((accumulator, next) => accumulator + next)
            .ToEnumerable()
            .Print(nameof(FoldRight));
    }

    private static void FoldLeft()
    {
        LazyNumbers
            .Skip(40)
            .Reverse()
            .Aggregate((accumulator, next) => accumulator - next)
            .ToEnumerable()
            .Print(nameof(FoldLeft));
    }

    private static void Bind()
    {
        NestedLazyNumbers
            .SelectMany(innerEnumerable => innerEnumerable)
            .OrderBy(number => number)
            .Take(20)
            .Print(nameof(Bind));
    }

    private static void Print<TItem>(this IEnumerable<TItem> items, string message)
    {
        var spaces = new string(' ', 31 - message.Length);
        WriteLine($"{Dashes} {message}{spaces}{Dashes}");

        foreach (var item in items)
            WriteLine(item);
    }

    private static IEnumerable<TItem> ToEnumerable<TItem>(this TItem item)
    {
        yield return item;
    }
}
```

## Conclusion ##

I hope that this post has helped you to understand
more about the Functional Programming paradigm and how can we use it
in our projects, to improve `readability`, `testability` and `correctness`
of our software. Remember that FP and OOP are orthogonal and you can use
the best of both worlds.

If you have any feedback or questions contact me at: [ofeth@hotmail.com](mailto:ofeth@hotmail.com)

Happy coding!

## Useful links ##

1. Delegates
    - <https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/delegate>
    - <https://www.geeksforgeeks.org/c-sharp-delegates/>
    - <https://www.tutorialspoint.com/csharp/csharp_delegates.htm>
2. Funcs
    - <https://docs.microsoft.com/en-us/dotnet/api/system.func-1?view=netframework-4.8>
    - <https://www.geeksforgeeks.org/c-sharp-func-delegate/>
3. Actions
    - <https://docs.microsoft.com/en-us/dotnet/api/system.action?view=netframework-4.8>
    - <https://www.geeksforgeeks.org/c-sharp-action-delegate/>
4. Curried Functions
    - <https://codeblog.jonskeet.uk/2012/01/30/currying-vs-partial-function-application/>
    - <https://www.geeksforgeeks.org/higher-order-functions-currying/>
    - <https://weblogs.asp.net/dixin/functional-csharp-higher-order-function-currying-and-first-class-function>
5. Partial Application
    - <https://marcclifton.wordpress.com/2017/06/23/partial-application-and-currying-in-c-clearing-the-fog/>
    - <http://mikehadlow.blogspot.com/2015/09/partial-application-in-c.html>
6. Lazy Evaluation / Deferred Execution
    - <https://blogs.msdn.microsoft.com/pedram/2007/06/02/lazy-evaluation-in-c/>
    - <https://zohaib.me/lazy-evaluation-in-c/>
    - <https://xosfaere.wordpress.com/2010/03/21/lazy-evaluation-in-csharp/>
7. Extension Methods
    - <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/extension-methods>
    - <https://www.geeksforgeeks.org/extension-method-in-c-sharp/>
8. Smart Constructors
    - <https://davemateer.com/2019/03/12/Functional-Programming-in-C-Sharp-Expressions-Options-Either>
    - <https://markkarpov.com/post/smart-constructors-that-cannot-fail.html>
    - <https://news.ycombinator.com/item?id=10295487>
9. Avoid primitive obsession
    - <https://refactoring.guru/smells/primitive-obsession>
    - <https://enterprisecraftsmanship.com/2015/03/07/functional-c-primitive-obsession/>
10. Generics
    - <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/>
    - <https://www.tutorialsteacher.com/csharp/csharp-generics>
    - <https://www.geeksforgeeks.org/c-sharp-generics-introduction/>
11. LINQ
    - <https://www.tutorialspoint.com/linq/linq_overview.htm>
    - <https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/>
    - <https://code.msdn.microsoft.com/101-LINQ-Samples-3fb9811b>
