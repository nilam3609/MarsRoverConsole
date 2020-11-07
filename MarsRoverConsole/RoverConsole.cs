using MarsRover.Manager;
using System;
using System.Threading.Tasks;

namespace MarsRoverConsole
{
    public class RoverConsole
    {
        private readonly IRoverDeploymentService _roverDeploymentService;
        private readonly IRoverMovementService _roverMovementService;
        private readonly IPlateauService _plateauService;

        public RoverConsole(IRoverDeploymentService roverDeploymentService,
            IRoverMovementService roverMovementService,
            IPlateauService plateauService)
        {
            _roverDeploymentService = roverDeploymentService;
            _roverMovementService = roverMovementService;
            _plateauService = plateauService;
        }

        /// <summary>
        /// Executes all the commands
        /// </summary>
        /// <param name="cardanoCommand"></param>
        /// <returns></returns>
        public async Task ExecuteMission(string[] cardanoCommand)
        {
            //Start reading commands line by line
            for (int i = 1; i < cardanoCommand.Length; i++)
            {
                try
                {
                    //Call deploy method for specific cordinate
                    Console.WriteLine("Rover is ready for deployment");
                    var deployRover = _roverDeploymentService.DeployRover(cardanoCommand[i]);

                    //Call movement method for specific movements
                    Console.WriteLine("Rover deployed, moving to specified cordinates");
                    var position = await _roverMovementService.MoveRover(cardanoCommand[i + 1], deployRover, _plateauService.GetPlateau());
                    
                    Console.WriteLine("Rover is at current position: "+ position.XPos + " " + position.YPos + " " + position.Direction);
                    Console.WriteLine("---------------------------------");
                }
                catch (Exception ex)
                {
                    //Catch exception if deployment or movement fails
                    Console.WriteLine(ex.Message);
                    Console.WriteLine("---------------------------------");
                }
                finally
                {
                    i++;
                }
            }
        }
    }
}