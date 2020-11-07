using MarsRover.Model;
using MarsRover.Model.Enums;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MarsRover.Manager
{
    /// <summary>
    /// Rover Movement 
    /// </summary>
    public class RoverMovementService : IRoverMovementService
    {
        private readonly IPlateauService _plateauService;

        public RoverMovementService(IPlateauService plateauService)
        {
            _plateauService = plateauService;
        }

        /// <summary>
        /// Move Rover based on current position and requested position
        /// </summary>
        /// <param name="command">Command to move rover</param>
        /// <param name="position">Current deployed position of the rover</param>
        /// <param name="plateauModel">Size of plateau</param>
        /// <returns></returns>
        public async Task<PositionModel> MoveRover(string command, PositionModel position, PlateauModel plateauModel)
        {
            try
            {
                //Check if command is valid 
                Regex commandPattern = new Regex("^[LMR]+$");
                if (commandPattern.IsMatch(command))
                {
                    //Split command
                    foreach (var c in command.ToCharArray())
                    {
                        int xPos = position.XPos;
                        int yPos = position.YPos;
                        //if command to move 
                        if (c == 'M')
                        {
                            Move(position.Direction, ref xPos, ref yPos);
                        }
                        //if command to turn left
                        else if (c == 'L')
                        {
                            if (position.Direction == Direction.N)
                                position.Direction = Direction.W;
                            else if (position.Direction == Direction.E)
                                position.Direction = Direction.N;
                            else if (position.Direction == Direction.S)
                                position.Direction = Direction.E;
                            else if (position.Direction == Direction.W)
                                position.Direction = Direction.S;
                        }
                        //if command to turn right
                        else if (c == 'R')
                        {
                            if (position.Direction == Direction.N)
                                position.Direction = Direction.E;
                            else if (position.Direction == Direction.E)
                                position.Direction = Direction.S;
                            else if (position.Direction == Direction.S)
                                position.Direction = Direction.W;
                            else if (position.Direction == Direction.W)
                                position.Direction = Direction.N;
                        }
                        position.XPos = xPos;
                        position.YPos = yPos;
                    }
                    //return current direction and position
                    return position;
                }
                throw new Exception("Invalid Movement Command");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void Move(Direction direction, ref int xPos, ref int yPos)
        {
            //Get size of plateau
            var plateauSize = _plateauService.GetPlateau();

            //movement position doesnt respect boundary
            if (xPos > plateauSize.Width || yPos > plateauSize.Height)
                throw ThrowException(xPos,yPos,default);

            switch (direction)
            {
                case Direction.N:
                    yPos += 1;
                    if (yPos < 0 || yPos > plateauSize.Height)
                        throw ThrowException(xPos,yPos, Direction.N);
                    break;

                case Direction.E:
                    xPos += 1;
                    if (xPos < 0 || xPos > plateauSize.Width)
                        throw ThrowException(xPos,yPos, Direction.E);
                    break;

                case Direction.S:
                    yPos -= 1;
                    if (yPos < 0)
                        throw ThrowException(xPos,yPos, Direction.S);
                    break;

                case Direction.W:
                    xPos -= 1;
                    if (xPos < 0)
                        throw ThrowException(xPos,yPos, Direction.W);
                    break;
            }
        }

        public Exception ThrowException(int x, int y, Direction d)
        {
            return new Exception("Rover crashed and is waiting for rescue at location: " + x +" "+ y + " " + d);
        }

    }
}