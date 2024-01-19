namespace The_Final_Battle;
public interface IFightAction
{
	void Resolve(Fight fight, Creature user, Creature target);
}
