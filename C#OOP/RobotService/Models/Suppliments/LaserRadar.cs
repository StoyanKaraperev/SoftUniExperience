namespace RobotService.Models.Suppliments;

public class LaserRadar : Supplement
{
    private const int interfaceStandardLaser = 20082;
    private const int batteryUsageLaser = 5000;

    public LaserRadar()
        : base(interfaceStandardLaser, batteryUsageLaser)
    {
    }
}
