using MarsRover.Manager;
using MarsRover.Model;
using Moq;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace MarsRover.UnitTest
{
    public class RoverMovementServiceTest
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

        [TestCase("LML")]
        [TestCase("MMLMLMM")]
        public async Task GivenTheCommandIsValidWhenMoveRoverIsCalledThenRoverPositionIsChanged(string command)
        {
            //Arrange
            var arrangedPosition = new PositionModel
            {
                XPos = 3,
                YPos = 3,
                Direction = Model.Enums.Direction.N
            };
            var plateauModel = new PlateauModel
            {
                Height = 5,
                Width = 5
            };
            var expectedPosition = new PositionModel
            {
                XPos = 2,
                YPos = 3,
                Direction = Model.Enums.Direction.S
            };
            var roverMovementService = new RoverMovementService(plateauService);

            //Act
            var response = await roverMovementService.MoveRover(command, arrangedPosition, plateauModel);

            //Assert
            Assert.AreEqual(expectedPosition.XPos, response.XPos);
            Assert.AreEqual(expectedPosition.YPos, response.YPos);
            Assert.AreEqual(expectedPosition.Direction, response.Direction);
        }

        [Test]
        public async Task GivenTheCommandIsInvalidWhenMoveRoverIsCalledThenThrowsException()
        {
            //Arrange
            string invalidCommand = "PPP";
            var arrangedPosition = new PositionModel
            {
                XPos = 3,
                YPos = 3,
                Direction = Model.Enums.Direction.N
            };
            var plateauModel = new PlateauModel
            {
                Height = 5,
                Width = 5
            };
            var roverMovementService = new RoverMovementService(plateauService);

            //Act and Assert
            var response = Assert.ThrowsAsync<Exception>(async() => await roverMovementService.MoveRover(invalidCommand, arrangedPosition, plateauModel));
            Assert.AreEqual("Invalid Movement Command", response.Message);
        }

        [Test]
        public async Task GivenTheCommandIsValidWhenMoveRoverIsCalledAndRoverIsMovingOutOfBoundThenThrowsException()
        {
            //Arrange
            string command = "MLL";
            var arrangedPosition = new PositionModel
            {
                XPos = 0,
                YPos = 5,
                Direction = Model.Enums.Direction.N
            };
            var plateauModel = new PlateauModel
            {
                Height = 5,
                Width = 5
            };
            var roverMovementService = new RoverMovementService(plateauService);

            //Act and Assert
            var response = Assert.ThrowsAsync<Exception>(async () => await roverMovementService.MoveRover(command, arrangedPosition, plateauModel));
            Assert.AreEqual("Rover is waiting for rescue at: 0 6 N", response.Message);
        }
    }
}