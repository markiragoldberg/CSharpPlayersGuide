using ConsoleIO;

public interface IFightAction
{
    void Resolve(Fighter user, Fighter target, Fight fight);
}

public class DoNothingFightAction : IFightAction
{
    public readonly string messageFormat = "{0} does nothing.";

    public void Resolve(Fighter user, Fighter target, Fight fight)
    {
        ColoredConsole.WriteLine(
            String.Format(messageFormat, user.Name), ConsoleColor.Yellow);
        Thread.Sleep(500);
    }
}

public class AttackAction(string messageFormat) : IFightAction
{
    public string MessageFormat { get; } = messageFormat;

    public void Resolve(Fighter user, Fighter target, Fight fight)
    {
        ColoredConsole.WriteLine(
            String.Format(MessageFormat, user.Name, target.Name), ConsoleColor.Yellow);
        Thread.Sleep(500);
    }
}