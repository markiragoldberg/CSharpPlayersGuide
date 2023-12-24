Sword basic = Sword.CreateBasic();
Sword gladius = basic with { Length = 60f, CrossguardWidth = 7f };
Sword wizsword = basic with { Material = Material.Bronze, Gemstone = Gemstone.Emerald };
Console.WriteLine(basic);
Console.WriteLine(gladius);
Console.WriteLine(wizsword);
// -----------------------------------

public record Sword(Material Material, Gemstone? Gemstone, float Length, float CrossguardWidth)
{
    public static Sword CreateBasic() => new(Material.Iron, null, 80f, 16f);
}

public enum Material { Wood, Bronze, Iron, Steel, Binarium }
public enum Gemstone { Emerald, Amber, Sapphire, Diamond, Bitstone }
