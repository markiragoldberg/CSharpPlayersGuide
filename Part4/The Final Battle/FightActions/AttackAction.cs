//namespace The_Final_Battle;
//public class AttackAction(SkillDef def) : IFightAction
//{
//	public string Name { get => def.DefName; }
//	public void Resolve(Creature user, Creature target)
//    {
//        Fight? fight = user.FightTeam?.Fight;
//        if (fight == null) throw new ArgumentException("AttackAction.Resolve has null fight");
//        if(!RNG.PercentChance(def.HitChance))
//		{
//			fight.Display.AddMessage(
//				$"{user.Name} used {def.DefName.ToUpper()} but missed!", MessageCategory.Warning);
//            return;
//		}
//        string messageFormat = def.MessageFormat ?? $"{{0}} used {def.DefName.ToUpper()} on {{1}}.";

//		fight.Display.AddMessage(
//            String.Format(messageFormat, user.Name, target.Name), MessageCategory.Warning);
//        int damage = def.DamageDice.Roll();
//        target.TakeDamage(damage);
//        fight.Display.AddMessage(
//            $"{target.Name} was hit for {damage} damage.", MessageCategory.Warning);
//        fight.Display.AddMessage(
//            $"{target.Name} is now at {target.Health}/{target.MaxHealth} HP.", MessageCategory.Warning);
//        target.FightTeam?.RemoveDead(fight);
//    }
//}