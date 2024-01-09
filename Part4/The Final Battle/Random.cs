namespace The_Final_Battle
{
    internal static class RNG
    {
        internal readonly static System.Random rng = new();

        public static int Roll(int min, int max, int bonus) =>
            rng.Next(min, max+1) + bonus;
    }
}
