namespace BetterRandom
{
    public static class RandomExtensions
    {
        public static double NextDouble(this Random self, double maximum)
        {
            return self.NextDouble() * maximum;
        }

        public static string? NextString(this Random self, params string[] options)
        {
            if (options.Length > 0)
                return options[self.Next(options.Length)];
            return null;
        }

        public static bool CoinFlip(this Random self, double trueFrequency = 0.5)
        {
            return self.NextDouble() < trueFrequency;
        }
    }
}
