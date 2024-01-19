namespace The_Final_Battle
{
	public class CreatureFactory
	{
		private Dictionary<string, CreatureDef> _creatureDefs;
		private Dictionary<string, SkillDef> _skillDefs;
		public Creature CreateCreature(string defName, string? name = null)
		{
			Creature creature = new Creature(_creatureDefs[defName], name);
			return creature;
		}
		public CreatureFactory()
		{
			string defName;
			_skillDefs = new();

			defName = "punch";
			AttackEffect punchAttack = new(damageDice: new(1, 1), hitChance: 1.0, null, null,
				"{0} punches {1} for {2} damage.");
			_skillDefs[defName] = new SkillDef(defName, punchAttack);

			defName = "quick shot";
			AttackEffect quickShotAttack = new(damageDice: new(3, 3), hitChance: 0.5, null, null,
				"{0} shot {1} with an arrow for {2} damage.");
			_skillDefs[defName] = new SkillDef(defName, quickShotAttack);

			defName = "bone crunch";
			AttackEffect boneCrunchAttack = new(damageDice: new(0, 1), hitChance: 1.0, null, null,
				"{0} bone crunches {1} in the bones for {2} damage.");
			_skillDefs[defName] = new SkillDef(defName, boneCrunchAttack);

			defName = "unravel";
			AttackEffect unravelAttack = new(damageDice: new(0, 2), hitChance: 1.0, null, null,
				"{0} unravels the code of {1} for {2} damage.");
			_skillDefs[defName] = new SkillDef(defName, unravelAttack);

			defName = "bandage";
			HealEffect bandageHeal = new(healDice: new(0, 4), "{0} bandaged {1} for {2} health.");
			SkillTarget healTarget = SkillTarget.AllyUnconditional;
			healTarget.Conditions.Add(new TargetIsInjured());
			_skillDefs[defName] = new SkillDef(defName, bandageHeal, healTarget);

			_creatureDefs = new();

			defName = "hero";
			_creatureDefs[defName] = new CreatureDef(defName, 25, null, 
				_skillDefs["punch"], _skillDefs["bandage"]);

			defName = "fletcher";
			_creatureDefs[defName] = new CreatureDef(defName, 15, null, 
				_skillDefs["punch"], _skillDefs["quick shot"]);

			defName = "skeleton";
			_creatureDefs[defName] = new CreatureDef(defName, 4, defaultName: "Skeleton", 
				_skillDefs["bone crunch"]);

			defName = "boss";
			_creatureDefs[defName] = new CreatureDef(defName, 25, defaultName: "The Uncoded One",
				_skillDefs["unravel"]);
		}
	}
}