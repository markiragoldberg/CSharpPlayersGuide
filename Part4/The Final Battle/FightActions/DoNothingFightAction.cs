﻿namespace The_Final_Battle;
public class DoNothingFightAction : IFightAction
{
    public readonly string messageFormat = "{0} does nothing.";
    public string Name { get =>  "NOTHING"; }

    public void Resolve(Fighter user, Fighter target, Fight fight)
    {
        fight.Display.AddMessage(
            String.Format(messageFormat, user.Name), MessageCategory.Info);
    }
}