using MarsRover.Manager;
using MarsRover.Model;
using Moq;
using NUnit.Framework;
using System;

namespace MarsRover.UnitTest
{
    public class RoverDeploymentServiceTest
    {
        private IPlateauService plateauService;
        [SetUp]
        public void Setup()
        {
            var fix = new PlateauModel
            { 
                Height = 5,
                Width = 5
            };
            var mockPlateauService = new Mock<IPlateauService>();
            mockPlateauService.Setup(x => x.GetPlateau()).Returns(fix);
            plateauService = mockPlateauService.Object;
        }

        [TestCase("1 2 N")]
        [TestCase("3 3 E")]
        [Description("Given the valid command is passed to deploy rover When RoverDeployment service is called Then it returns correct position")]
        public void GivenCommandIsValidWhenRoverIsDeployedThenReturnsCorrectPosition(string command)
        {
            //Arrange
            var splitCommand = command.Split(" ");
            var xPos = Convert.ToInt32(splitCommand[0]);
            var yPos = Convert.ToInt32(splitCommand[1]);
            var direction = splitCommand[2];
            var _roverDeploymentService = new RoverDeploymentService(plateauService);

            //Act
            var response = _roverDeploymentService.DeployRover(command);

            //Assert
            Assert.AreEqual(xPos, response.XPos);
            Assert.AreEqual(yPos, response.YPos);
            Assert.AreEqual(direction, response.Direction.ToString());
        }

        [TestCase("-1 2 N")]
        [Description("Given the invalid command is passed to deploy rover When RoverDeployment service is called Then it throws an exception")]
        public void GivenCommandIsInvalidWhenRoverIsDeployedThenThrowsException(string command)
        {
            //Arrange
            var splitCommand = command.Split(" ");
            var xPos = Convert.ToInt32(splitCommand[0]);
            var yPos = Convert.ToInt32(splitCommand[1]);
            var direction = splitCommand[2];
            var _roverDeploymentService = new RoverDeploymentService(plateauService);

            //Act and Assert
            var ex = Assert.Throws<Exception>(() => _roverDeploymentService.DeployRover(command));
            Assert.AreEqual("Invalid Deployment Command", ex.Message);
        }
    }
}