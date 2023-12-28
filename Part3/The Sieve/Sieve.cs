namespace TheSieve
{
    public class Sieve
    {
        public Predicate<int> Filter { get; set; }

        public Sieve(Predicate<int> filter)
        {
            Filter = filter;
        }
        public bool IsGood(int number)
        {
            return Filter(number);
        }

        public static bool IsEven(int number)
        {
            return number % 2 == 0;
        }
        public static bool IsPositive(int number)
        {
            return number > 0;
        }
        public static bool IsMultipleOfTen(int number)
        {
            return number % 10 == 0;
        }
    }
}
