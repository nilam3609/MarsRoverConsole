using MarsRover.Model;
using System.Threading.Tasks;

namespace MarsRover.Manager
{
    public interface IRoverMovementService
    {
        Task<PositionModel> MoveRover(string command, PositionModel position, PlateauModel plateauModel);
    }
}