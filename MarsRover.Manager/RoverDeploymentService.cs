using MarsRover.Model;
using MarsRover.Model.Enums;
using System;
using System.Text.RegularExpressions;

namespace MarsRover.Manager
{
    /// <summary>
    /// Deployment for rover
    /// </summary>
    public class RoverDeploymentService : IRoverDeploymentService
    {
        private readonly IPlateauService _plateauService;

        public RoverDeploymentService(IPlateauService plateauService)
        {
            _plateauService = plateauService;
        }

        /// <summary>
        /// Start Rover deployment and return its position
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public PositionModel DeployRover(string command)
        {
            //Check if the command is correct 
            Regex directionCommandPattern = new Regex("^\\d+ \\d+ [NSWE]$");
            if (directionCommandPattern.IsMatch(command))
            {
                //Split command to read individual command
                var splitCommand = command.Split(" ");
                var xPos = Convert.ToInt32(splitCommand[0]);
                var yPos = Convert.ToInt32(splitCommand[1]);
                var plateauSize = _plateauService.GetPlateau();
                
                //throw exception if deployment is out of bound
                if (xPos > plateauSize.Width
                    || yPos > plateauSize.Height
                    || xPos < 0
                    || yPos < 0)
                    throw new Exception("Rover couldnt be deployed");

                return new PositionModel
                {
                    XPos = xPos,
                    YPos = yPos,
                    Direction = (Direction)Enum.Parse(typeof(Direction), splitCommand[2])
                };
            }
            throw new Exception("Invalid Deployment Command");
        }
    }
}