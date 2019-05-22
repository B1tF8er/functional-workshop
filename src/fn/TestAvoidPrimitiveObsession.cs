namespace fn
{
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
            Email fromlString = "test4@test.com";

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
}
