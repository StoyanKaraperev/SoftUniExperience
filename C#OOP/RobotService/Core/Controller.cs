namespace RobotService.Core;

using RobotService.Core.Contracts;
using RobotService.Models.Contracts;
using RobotService.Models.Robots;
using RobotService.Models.Suppliments;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;
using System;
using System.Linq;
using System.Text;

public class Controller : IController
{
    private readonly IRepository<ISupplement> supplements;
    private readonly IRepository<IRobot> robots;

    public Controller()
    {
        this.supplements = new SupplementRepository(); 
        this.robots = new RobotRepository();
    }

    public string CreateRobot(string model, string typeName)
    {
        IRobot robot = typeName switch
        {
            nameof(DomesticAssistant) => new DomesticAssistant(model),
            nameof(IndustrialAssistant) => new IndustrialAssistant(model),
            _ => throw new InvalidOperationException(String.Format(OutputMessages.RobotCannotBeCreated, typeName))
        };

        this.robots.AddNew(robot);

        return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
    }

    public string CreateSupplement(string typeName)
    {
        ISupplement supplement = typeName switch
        {
            nameof(LaserRadar) => new LaserRadar(),
            nameof(SpecializedArm) => new SpecializedArm(),
            _ => throw new InvalidOperationException(String.Format(OutputMessages.SupplementCannotBeCreated, typeName))
        };

        this.supplements.AddNew(supplement);

        return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
    }

    public string UpgradeRobot(string model, string supplementTypeName)
    {
        ISupplement suppliment =
            this.supplements.Models()
            .FirstOrDefault(s => s.GetType().Name == supplementTypeName);

        var robotModel = this.robots.Models()
            .Where(r => r.Model == model);

        var robotNotUpgraded = 
            robotModel.Where (r => r.InterfaceStandards.All(rm => rm != suppliment.InterfaceStandard));

        var robotForUpgrade = robotNotUpgraded.FirstOrDefault();

        if (robotForUpgrade == null)
        {
            return String.Format(OutputMessages.AllModelsUpgraded, model);
        }

        robotForUpgrade.InstallSupplement(suppliment);
        this.supplements.RemoveByName(supplementTypeName);

        return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);
    }

    public string PerformService(string serviceName, int intefaceStandard, int totalPowerNeeded)
    {
        var selectRobotModels = this.robots.Models()
            .Where(r => r.InterfaceStandards.Any(i => i == intefaceStandard))
            .OrderByDescending(i => i.BatteryLevel);

        if(selectRobotModels.Count() == 0)
        {
            return $"Unable to perform service, {intefaceStandard} not supported!";
        }

        var totalPowerBatteryLevelSum = selectRobotModels.Sum(r => r.BatteryLevel);

        if (totalPowerBatteryLevelSum < totalPowerNeeded)
        {
            return $"{serviceName} cannot be executed! {totalPowerNeeded - totalPowerBatteryLevelSum} more power needed.";
        }

        int usedRobotsCount = 0;

        foreach (var robot in selectRobotModels)
        {
            usedRobotsCount++;

            if (totalPowerNeeded <= robot.BatteryLevel)
            {
                robot.ExecuteService(totalPowerNeeded);
                break;
            }

            else
            {
                totalPowerNeeded -= robot.BatteryLevel;
                robot.ExecuteService(robot.BatteryLevel);
            }
        }

        return $"{serviceName} is performed successfully with {usedRobotsCount} robots.";
    }

    public string RobotRecovery(string model, int minutes)
    {
        var robotModels = this.robots.Models()
            .Where(r => r.Model == model &&  r.BatteryLevel * 2 < r.BatteryCapacity);

        var fedCount = 0; 

        foreach (var robot in robotModels)
        {
            robot.Eating(minutes);
            fedCount++;
        }

        return $"Robots fed: {fedCount}";
    }

    public string Report()
    {
        var sb = new StringBuilder();

        var robotsInformation = this.robots.Models()
            .OrderByDescending(r => r.BatteryLevel)
            .ThenBy(r => r.BatteryCapacity)
            .ToList();

        foreach (var robot in robotsInformation)
        {
            sb
                .AppendLine(robot.ToString());
        }

        return sb.ToString().TrimEnd();
    }
}
