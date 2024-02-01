using The_Final_Battle.Items;

namespace The_Final_Battle
{
	public class ThingFactory
	{
		private Dictionary<string, CreatureDef> _creatureDefs;
		private Dictionary<string, AbilityDef> _skillDefs;
		private Dictionary<string, ItemDef> _itemDefs;
		public Creature CreateCreature(string defName, string? name = null)
		{
			return new Creature(_creatureDefs[defName], name);
		}
		public Item CreateItem(string defName, int charges = 1)
		{
			return new Item(_itemDefs[defName], charges);
		}
		public ThingFactory()
		{
			string defName;
			_skillDefs = new();

			defName = "punch";
			AttackEffect punchAttack = new(damageDice: new(1, 1), hitChance: 0.9, null, null,
				"{0} punches {1} for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, punchAttack);

			defName = "quick shot";
			AttackEffect quickShotAttack = new(damageDice: new(3, 3), hitChance: 0.5, null, null,
				"{0} shot {1} with an arrow for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, quickShotAttack);

			defName = "bone crunch";
			AttackEffect boneCrunchAttack = new(damageDice: new(1, 1), hitChance: 0.5, null, null,
				"{0} bone crunches {1} in the bones for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, boneCrunchAttack);

			defName = "hammer";
			AttackEffect hammerAttack = new(damageDice: new(6, 6), hitChance: 0.9, null, null,
				"{0} smashes {1} with a hammer for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, hammerAttack);

			defName = "unravel";
			AttackEffect unravelAttack = new(damageDice: new(0, 2), hitChance: 1.0, null, null,
				"{0} unravels the code of {1} for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, unravelAttack);

			defName = "healing cantrip";
			HealEffect healingCantrip = new(healDice: new(1, 2), "{0} healed {1} with a cantrip for {2} health.");
			SkillTarget healTarget = SkillTarget.AllyUnconditional;
			healTarget.Conditions.Add(new TargetIsInjured());
			_skillDefs[defName] = new AbilityDef(defName, healingCantrip, healTarget);

			_creatureDefs = new();

			defName = "hero";
			_creatureDefs[defName] = new CreatureDef(defName, 25, null, 
				_skillDefs["punch"], _skillDefs["healing cantrip"]);

			defName = "fletcher";
			_creatureDefs[defName] = new CreatureDef(defName, 15, null, 
				_skillDefs["punch"], _skillDefs["quick shot"]);

			defName = "skeleton";
			_creatureDefs[defName] = new CreatureDef(defName, 4, defaultName: "Skeleton", 
				_skillDefs["bone crunch"]);

			defName = "skeleton hammerer";
			_creatureDefs[defName] = new CreatureDef(defName, 4, defaultName: "Skeleton Hammerer",
				_skillDefs["hammer"]);

			defName = "skeleton armored";
			_creatureDefs[defName] = new CreatureDef(defName, 9, defaultName: "Armored Skeleton",
				_skillDefs["bone crunch"]);

			defName = "skeleton healer";
			_creatureDefs[defName] = new CreatureDef(defName, 4, defaultName: "Skeleton Healer",
				_skillDefs["bone crunch"], _skillDefs["healing cantrip"]);

			defName = "boss";
			_creatureDefs[defName] = new CreatureDef(defName, 25, defaultName: "The Uncoded One",
				_skillDefs["unravel"]);

			_itemDefs = new();

			defName = "bandage";
			HealEffect healingBandage = new(healDice: new(3, 4), "{0} bandaged {1} for {2} health.");
			AbilityDef bandageSkill = new(defName, healingBandage, healTarget);
			_itemDefs[defName] = new ItemDef(defName, skillWhenUsed: bandageSkill);
		}
	}
}