using MarsRover.Manager;
using NUnit.Framework;
using System;

namespace MarsRover.UnitTest
{
    public class PlateauServiceTest
    {
        [TestCase("5 5")]
        [TestCase("2 2")]
        [TestCase("5 10")]
        [Description("Given the valid command to set plateau is used When Plateau service is called Then size should set")]
        public void GivenPlateauServiceHasValidCommandThenParamsAreSet (string command)
        {
            //Arrange
            PlateauService sut = new PlateauService(command);
            var expectedWidth = Convert.ToInt32(command.Split(" ")[0]);
            var expectedHeight= Convert.ToInt32(command.Split(" ")[1]);

            //Act
            var response = sut.GetPlateau();

            //Assert
            Assert.AreEqual(expectedWidth,response.Width);
            Assert.AreEqual(expectedHeight,response.Height);
        }

        [TestCase("0 0")]
        [Description("Given the invalid command is passed When Plateau service is called Then service throws exception")]
        public void GivenPlateauServiceHasInvalidCommandThenThrowsException(string command)
        {
            //Arrange,Act and Assert
            Assert.Throws<Exception>(() => new PlateauService(command));
        }
    }
}