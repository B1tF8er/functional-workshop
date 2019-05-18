namespace fn
{
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
}