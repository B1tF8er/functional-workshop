# Arrow Notation #

Arrow notations are the preferred syntax to read the signature of a function
in Functional Programming lingo and they are right associative,

`(int) -> ((int) -> (int))` **is equal to** `int -> int -> int`

That can be read as a function that takes two integers as arguments
and returns another integer. In C# we have three ways to create it,

`Delegates = private delegate int ExampleDelegate(int left, int right)`
left and right are the first two ints in the signature
and the return type is the third int.

```csharp
using System;

internal class TestDelegates
{
    // int -> int -> int
    private delegate int MyFirstSumDelegate(int x, int y);

    // int -> int -> int -> ()
    private delegate void MySecondSumDelegate(int x, int y, int z);

    internal static void Run()
    {
        MyFirstSumDelegate myFirstSumDelegate = SumTwo;

        var delegateResult = myFirstSumDelegate(1, 2);

        Console.WriteLine(delegateResult);

        MySecondSumDelegate mySecondSumDelegate = SumThreeWithLog;
        mySecondSumDelegate(1, 2, 3);
    }

    private static int SumTwo(int a, int b) => a + b;

    private static void SumThreeWithLog(int a, int b, int c)
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

internal class TestFuncs
{
    // int -> int -> int
    private delegate int MySumDelegate(int x, int y);

    internal static void Run()
    {
        MySumDelegate mySumDelegate = Sum;
        // int -> int -> int
        Func<int, int, int> mySumFunc = Sum;

        var delegateResult = mySumDelegate(1, 2);
        var funcResult = mySumFunc(1, 2);

        Console.WriteLine(delegateResult);
        Console.WriteLine(funcResult);
    }

    private static int Sum(int a, int b) => a + b
}
```

`Actions = private Action<int, int, int> ExampleAction`
are a special case, they read as Funcs but
they always return void. It is read as a function
that takes 3 ints as arguments and returns void.

```csharp
using System;

internal class TestActions
{
    // int -> int -> int -> ()
    private delegate void MySumDelegate(int x, int y, int z);

    internal static void Run()
    {
        MySumDelegate mySumDelegate = SumWithLog;
        // int -> int -> int -> ()
        Action<int, int, int> mySumAction = SumWithLog;

        mySumDelegate(1, 2, 3);
        mySumAction(1, 2, 3);
    }

    private static void SumWithLog(int a, int b, int c)
    {
        var result = a + b + c;
        Console.WriteLine(result);
    }
}
```
