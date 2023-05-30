namespace RobotService.Models.Suppliments;

using RobotService.Models.Contracts;

public abstract class Supplement : ISupplement
{
    private int interfaceStandard;
    private int batteryUsage; 

    protected Supplement(int interfaceStandard, int batteryUsage)
    {
        this.InterfaceStandard = interfaceStandard;
        this.BatteryUsage = batteryUsage;
    }

    public int InterfaceStandard 
    { 
        get => this.interfaceStandard; 
        private set => this.interfaceStandard = value; 
    }

    public int BatteryUsage 
    { 
        get => this.batteryUsage; 
        private set => this.batteryUsage = value; 
    }
}
