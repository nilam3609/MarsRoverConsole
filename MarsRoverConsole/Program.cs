using MarsRover.Manager;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MarsRoverConsole
{
    internal class Program
    {
        /// <summary>
        /// Main method
        /// </summary>
        /// <param name="args"></param>
        private static void Main(string[] args)
        {
            string[] cardanoCommand = new string[]
            {
                "5 5",
                "1 2 N",
                "LML",
                "3 3 E",
                "MMR",
                "0 2 S",
                "MMR"
            };

            //Setup services DI
            var provider = SetupService(cardanoCommand[0]);
            var plateauService = provider.GetService<IPlateauService>();
            var roverDeploymentService = provider.GetService<IRoverDeploymentService>();
            var roverMovementService = provider.GetService<IRoverMovementService>();

            //Initialize
            RoverConsole mission = new RoverConsole(roverDeploymentService, roverMovementService, plateauService);

            //Start executing mission
            mission.ExecuteMission(cardanoCommand);
            Console.ReadKey();
        }

        /// <summary>
        /// Dependency Injection
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static ServiceProvider SetupService(string command)
        {
            var serviceProvider = new ServiceCollection();
            serviceProvider.AddSingleton<IPlateauService>(s => new PlateauService(command));
            serviceProvider.AddScoped<IRoverDeploymentService, RoverDeploymentService>();
            serviceProvider.AddScoped<IRoverMovementService, RoverMovementService>();
            return serviceProvider.BuildServiceProvider();
        }
    }
}