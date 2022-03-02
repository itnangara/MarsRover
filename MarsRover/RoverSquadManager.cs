using System;
using System.Collections.Generic;

namespace MarsRover
{
    public class RoverSquadManager : IRoverSquadManager
    {
        /*
         * this class confirms the validity
         * of the input data being provided 
         * for the rover navigation
         */
        public List<Rover> Rovers { get; } = new List<Rover>();

        public ILandingSurface LandingSurface { get; }

        public Rover ActiveRover { get; private set; }

        public RoverSquadManager(ILandingSurface landingSurface)
        {
            LandingSurface = landingSurface;
        }

        public void DeployRover(int x, int y, Direction direction)
        {
            /*this method deploys the rover 
             * in the given coordinates 
             * after confirming that the coordinates 
             * are valid
             */
            CheckIfLocationToDeployIsValid(x, y);
            var rover = new Rover(x, y, direction, LandingSurface);
            Rovers.Add(rover);
            ActiveRover = rover;
        }

        private void CheckIfLocationToDeployIsValid(int x, int y)
        {
            /*
             *this method takes in the x and y 
             * coordinate provided as input 
             * and evaluates if they're valid and 
             * in range
             */
            if (!IsAppropriateLocationToDeployRover(x, y))
                throw new Exception("Rover outside of bounds");
        }

        private bool IsAppropriateLocationToDeployRover(int x, int y)
        {
            /*
             *this method confirms if the
             * the location to deploy the rover is appropiate
             */
            return x >= 0 && x < LandingSurface.Size.Width &&
                   y >= 0 && y < LandingSurface.Size.Height;
        }
    }
}