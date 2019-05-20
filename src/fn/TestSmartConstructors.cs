namespace fn
{
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
            WriteLine($"Valid person data so all goes well and we can print: {person}");
        }

        private static void InvalidName()
        {
            try
            {
                var person = new SmartPerson(null, 30);
            }
            catch (ArgumentNullException ex)
            {
                WriteLine(ex);
                WriteLine("Name was `null` so the smart constructor of person threw an exception");
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
                WriteLine(ex);
                WriteLine("Age was `not in range` so the smart constructor of person threw an exception");
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
            if (name is null)
                throw new ArgumentNullException(nameof(name));
        }

        private void GuardAge(int age)
        {
            if (age < 0 || age > 120)
                throw new ArgumentOutOfRangeException(nameof(age));
        }

        public override string ToString() => $"My name is {Name} and I am {Age} years old";
    }
}
