namespace RobotService.Models.Suppliments;

public class SpecializedArm : Supplement
{
    private const int interfaceStandardArm = 10045;
    private const int batteryUsageArm = 10000;

    public SpecializedArm()
        : base(interfaceStandardArm, batteryUsageArm)
    {
    }
}
