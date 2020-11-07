using MarsRover.Model;

namespace MarsRover.Manager
{
    public interface IRoverDeploymentService
    {
        PositionModel DeployRover(string command);
    }
}