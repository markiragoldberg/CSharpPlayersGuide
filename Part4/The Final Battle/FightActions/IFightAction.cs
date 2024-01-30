namespace The_Final_Battle;
public interface IFightAction
{
	public bool IsAttack { get; }
	public AbilityDef Def { get; }
	void Resolve(Fight fight, Messaging.Log log, Creature user, Creature target);
	public bool Usable(Fight fight, Creature user);
}
