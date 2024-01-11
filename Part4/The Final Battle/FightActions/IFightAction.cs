namespace The_Final_Battle;
public interface IFightAction
{
	string Name { get; }
	void Resolve(Fighter user, Fighter target, Fight fight);
}
