using MarsRover.Model;
using System;

namespace MarsRover.Manager
{
    /// <summary>
    /// Create Plateau/Grid
    /// </summary>
    public class PlateauService :IPlateauService
    {
        private PlateauModel _plateauModel;

        public PlateauService(string command) 
        {
            if (_plateauModel == null)
                _plateauModel = new PlateauModel();

            _plateauModel.Width = Convert.ToInt32(command.Split(" ")[0]) > 0 ? Convert.ToInt32(command.Split(" ")[0]) : throw new Exception();
            _plateauModel.Height = Convert.ToInt32(command.Split(" ")[1]) > 0 ? Convert.ToInt32(command.Split(" ")[1]) : throw new Exception();
        }

        /// <summary>
        /// Get Plateau size
        /// </summary>
        /// <returns></returns>
        public PlateauModel GetPlateau()
        {
            return _plateauModel;
        }
    }
}