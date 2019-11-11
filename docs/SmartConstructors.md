# Smart Constructors #

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
