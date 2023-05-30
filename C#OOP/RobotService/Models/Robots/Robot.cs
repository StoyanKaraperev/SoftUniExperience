namespace RobotService.Models.Robots;

using RobotService.Models.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Text;

public abstract class Robot : IRobot
{
    private string model;
    private int batteryCapacity;
    private int batteryLevel;
    private int convertionCapacityIndex;
    private readonly List<int> interfaceStandards;

    public Robot(string model, int batteryCapacity, int convertionCapacityIndex)
    {
        this.Model = model;
        this.BatteryCapacity = batteryCapacity;
        this.ConvertionCapacityIndex = convertionCapacityIndex;
        this.BatteryLevel = batteryCapacity;
        this.interfaceStandards = new List<int>();
    }

    public string Model
    {
        get => model;
        private set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException(String.Format(ExceptionMessages.ModelNullOrWhitespace));
            }

            model = value;
        }
    }

    public int BatteryCapacity
    {
        get => batteryCapacity;
        private set
        {
            if (value < 0)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.BatteryCapacityBelowZero));
            }

            batteryCapacity = value;
        }
    }

    public int BatteryLevel { get; private set; }

    public int ConvertionCapacityIndex
    {
        get => convertionCapacityIndex;
        private set
        {
            convertionCapacityIndex = value;
        }
    }

    public IReadOnlyCollection<int> InterfaceStandards => this.interfaceStandards.AsReadOnly();

    public void Eating(int minutes)
    {
        for (int i = 1; i <= minutes; i++)
        {
            if (BatteryCapacity == BatteryLevel)
            {
                break;
            }
            else
            {
                BatteryLevel = ConvertionCapacityIndex * i;
            }
        }
    }

    public void InstallSupplement(ISupplement supplement)
    {
        this.BatteryCapacity -= supplement.BatteryUsage;
        this.BatteryLevel -= supplement.BatteryUsage;
        this.interfaceStandards.Add(supplement.InterfaceStandard);
    }

    public bool ExecuteService(int consumedEnergy)
    {
        if (this.BatteryLevel >= consumedEnergy)
        {
            this.BatteryLevel -= consumedEnergy;
            return true; 
        }
        
        return false;
    }

    public override string ToString()
    {
        var robotInformation = new StringBuilder();

        var supplement = this.interfaceStandards.Count == 0
            ? "none"
            : String.Join(" ", this.interfaceStandards);

        robotInformation
            .AppendLine($"{this.GetType().Name} {this.Model}:")
            .AppendLine($"--Maximum battery capacity: {this.BatteryCapacity}")
            .AppendLine($"--Current battery level: {this.BatteryLevel}")
            .AppendLine($"--Supplements installed: {supplement}");

        return robotInformation.ToString().TrimEnd();
    }
}
