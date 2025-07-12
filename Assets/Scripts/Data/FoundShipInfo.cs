public class FoundShipInfo
{
    public FoundShipInfo(SHIP_TYPE shipType, float direction, float course, int speed, int range)
    {
        this.ShipType = shipType;
        this.Direction = direction;
        this.Course = course;
        this.Speed = speed;
        this.Range = range;
    }

    internal SHIP_TYPE ShipType { get; set; }
    internal float Direction { get; set; }
    internal float Course { get; set; }
    internal int Speed { get; set; }
    internal int Range { get; set; }
}