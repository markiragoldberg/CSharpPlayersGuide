using The_Final_Battle.Defs;
using The_Final_Battle.Items;

namespace The_Final_Battle
{
	public class ThingFactory
	{
		private Dictionary<string, CreatureDef> _creatureDefs;
		private Dictionary<string, AbilityDef> _skillDefs;
		private Dictionary<string, ConsumableDef> _itemDefs;
		private Dictionary<string, WeaponDef> _weaponDefs;

		public Creature CreateCreature(string defName, Weapon? weapon = null, string? name = null)
		{
			return new Creature(_creatureDefs[defName], weapon, name);
		}
		public Item CreateItem(string defName, int charges = 1)
		{
			return new Item(_itemDefs[defName], charges);
		}
		public Weapon CreateWeapon(string defName)
		{
			return new Weapon(_weaponDefs[defName]);
		}
		public ThingFactory()
		{
			string defName;
			_skillDefs = new();

			defName = "punch";
			AttackEffect punchAttack = new(damageDice: new(1, 1), hitChance: 0.9, null, null,
				"{0} punches {1} for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, defName, [punchAttack]);

			defName = "bone crunch";
			AttackEffect boneCrunchAttack = new(damageDice: new(1, 1), hitChance: 0.5, null, null,
				"{0} bone crunches {1} in the bones for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, defName, [boneCrunchAttack]);

			defName = "unravel";
			AttackEffect unravelAttack = new(damageDice: new(0, 2), hitChance: 1.0, null, null,
				"{0} unravels the code of {1} for {2} damage.");
			_skillDefs[defName] = new AbilityDef(defName, defName, [unravelAttack]);

			defName = "healing cantrip";
			HealEffect healingCantrip = new(healDice: new(1, 2), "{0} healed {1} with a cantrip for {2} health.");
			SkillTarget healTarget = SkillTarget.AllyUnconditional;
			healTarget.Conditions.Add(new TargetIsInjured());
			_skillDefs[defName] = new AbilityDef(defName, defName, [healingCantrip], healTarget);

			_creatureDefs = new();

			defName = "hero";
			_creatureDefs[defName] = new CreatureDef(defName, "true programmer", 25,
				_skillDefs["punch"], _skillDefs["healing cantrip"]);

			defName = "fletcher";
			_creatureDefs[defName] = new CreatureDef(defName, "fletcher", 15,
				_skillDefs["punch"]);

			defName = "skeleton";
			_creatureDefs[defName] = new CreatureDef(defName, label: "skeleton", 4,
				_skillDefs["bone crunch"]);

			defName = "skeleton hammerer";
			_creatureDefs[defName] = new CreatureDef(defName, label: "skeleton hammerer", 4,
				_skillDefs["bone crunch"]);

			defName = "skeleton armored";
			_creatureDefs[defName] = new CreatureDef(defName, label: "armored skeleton", 9,
				_skillDefs["bone crunch"]);

			defName = "skeleton healer";
			_creatureDefs[defName] = new CreatureDef(defName, label: "skeleton priest", 4,
				_skillDefs["bone crunch"], _skillDefs["healing cantrip"]);

			defName = "boss";
			_creatureDefs[defName] = new CreatureDef(defName, label: "evil coder", 25,
				_skillDefs["unravel"]);

			_itemDefs = new();

			defName = "bandage";
			HealEffect healingBandage = new(healDice: new(3, 4), "{0} bandaged {1} for {2} health.");
			AbilityDef bandageSkill = new(defName, "apply bandage", [healingBandage], healTarget);
			_itemDefs[defName] = new ConsumableDef(defName, defName, skillWhenUsed: bandageSkill);

			_weaponDefs = new();

			defName = "sword";
			AttackEffect swordSlashEffect = new(damageDice: new(2, 2, 0), hitChance: 0.9, null, null,
				"{0} slashed {1} with a sword for {2} damage.", "{0} slashed at {1} but missed.");
			AbilityDef swordSlashSkill = new("swordSlash", "sword slash", [swordSlashEffect], SkillTarget.EnemyUnconditional);
			AttackEffect swordStabEffect = new(damageDice: new(1, 3, 0), hitChance: 0.9, null, null,
				"{0} stabbed {1} with a sword for {2} damage.", "{0} stabbed at {1} but missed.");
			AbilityDef swordStabSkill = new("swordStab", "sword stab", [swordStabEffect], SkillTarget.EnemyUnconditional);
			_weaponDefs[defName] = new(defName, defName, [swordSlashSkill, swordStabSkill]);

			defName = "dagger";
			AttackEffect daggerSlashEffect = new(damageDice: new(1, 2, 0), hitChance: 0.6, null, null,
				"{0} slashed {1} with a dagger for {2} damage.", "{0} slashed at {1} but missed.");
			AbilityDef daggerSlashSkill = new("slash", "dagger slash", [daggerSlashEffect], SkillTarget.EnemyUnconditional);
			AttackEffect daggerStabEffect = new(damageDice: new(1, 1, 0), hitChance: 0.9, null, null,
				"{0} stabbed {1} with a dagger for {2} damage.", "{0} stabbed at {1} but missed.");
			AbilityDef daggerStabSkill = new("daggerStab", "dagger stab", [daggerStabEffect], SkillTarget.EnemyUnconditional);
			_weaponDefs[defName] = new(defName, defName, [daggerSlashSkill, daggerStabSkill]);

			defName = "hammer";
			AttackEffect hammerAttack = new(damageDice: new(6, 6), hitChance: 0.35, null, null,
				"{0} smashes {1} with a hammer for {2} damage.", "{0} missed with a wide, slow hammer swing.");
			AbilityDef hammerSmash = new AbilityDef(defName, defName, [hammerAttack]);
			_weaponDefs[defName] = new(defName, defName, [hammerSmash]);

			defName = "fine bow";
			AttackEffect quickShotAttack = new(damageDice: new(3, 3), hitChance: 0.5, null, null,
				"{0} shot {1} with an arrow for {2} damage.");
			AbilityDef quickShot = new AbilityDef("quickShot", "quick shot", [quickShotAttack]);
			_weaponDefs[defName] = new(defName, defName, [quickShot]);
		}
	}
}
