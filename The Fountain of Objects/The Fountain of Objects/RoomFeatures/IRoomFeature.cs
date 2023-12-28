namespace CavernOfObjects.RoomFeatures;

public interface IRoomFeature
{
    public Point Location { get; }
    public bool ShootWithArrow() { return false; }
    public void StepOn(Player player) { }
    public void Sense(int distance) { }
}

public interface IEnablable
{
    public bool Enabled { get; }
    public bool Enable();
}

public interface IMovable
{
    public Point Location { get; set; }
}
