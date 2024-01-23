namespace The_Final_Battle;
public interface IFightAction
{
	void Resolve(Fight fight, Messaging.Log log, Creature user, Creature target);
}
