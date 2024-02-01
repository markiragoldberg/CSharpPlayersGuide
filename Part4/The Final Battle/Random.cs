namespace The_Final_Battle
{
	public static class RNG
	{
		public readonly static System.Random rng = new();

		public static int Roll(int min, int max, int bonus) =>
			rng.Next(min, max + 1) + bonus;

		public static bool PercentChance(double chance) =>
			rng.NextDouble() < chance;

		public static T RandomElement<T>(this IList<T> list)
		{
			return list.ElementAt(rng.Next(0, list.Count));
		}
	}

	public interface IDice<T>
	{
		public T Roll();
	}

	public class Dice : IDice<int>
	{
		public int MinDamage { get; }
		public int MaxDamage { get; }
		public int BonusDamage { get; }
		public Dice(int minDamage, int maxDamage, int bonusDamage = 0)
		{
			MinDamage = minDamage;
			MaxDamage = maxDamage;
			BonusDamage = bonusDamage;
		}
		public int Roll() => RNG.Roll(MinDamage, MaxDamage, BonusDamage);
	}
}
