namespace RobotService.Models.Robots;

public class IndustrialAssistant : Robot
{
    private const int batteryCapacityIndustrial = 40000;
    private const int convertionCapacityIndexIndustrial = 5000;
    public IndustrialAssistant(string model) 
        : base(model, batteryCapacityIndustrial, convertionCapacityIndexIndustrial)
    {
    }
}
