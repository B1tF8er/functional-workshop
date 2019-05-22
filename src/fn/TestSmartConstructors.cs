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
}
