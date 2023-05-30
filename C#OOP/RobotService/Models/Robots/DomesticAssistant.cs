namespace RobotService.Models.Robots;

public class DomesticAssistant : Robot
{
    private const int batteryCapacityDomestic = 20000;
    private const int convertionCapacityIndexDomestic = 2000;

    public DomesticAssistant(string model) 
        : base(model, batteryCapacityDomestic, convertionCapacityIndexDomestic)
    {
    }
}
