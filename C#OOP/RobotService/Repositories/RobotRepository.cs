namespace RobotService.Repositories;

using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System.Collections.Generic;
using System.Linq;

public class RobotRepository : IRepository<IRobot>
{
    private List<IRobot> robots;

    public RobotRepository()
    {
        this.robots = new List<IRobot>();
    }

    public ICollection<IRobot> Robots 
        => this.robots.AsReadOnly();

    public IReadOnlyCollection<IRobot> Models() 
        => this.robots.AsReadOnly();

    public void AddNew(IRobot model) 
        => this.robots.Add(model);

    public bool RemoveByName(string robotModel) 
        => this.robots.Remove(this.robots.FirstOrDefault(s => s.Model == robotModel));

    public IRobot FindByStandard(int interfaceStandard) 
        => this.robots.FirstOrDefault(r => r.InterfaceStandards.Any(i => i == interfaceStandard));
}
