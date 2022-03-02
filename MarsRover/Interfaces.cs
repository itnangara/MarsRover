using System.Collections.Generic;

namespace MarsRover
{
    /*
     *this file contains the interfaces 
     * that will be implemented 
     * in across the Rover classes
     */

    public interface ILandingSurface
    {
        SurfaceSize Size { get; }

        void Define(int width, int height);
    }


    public interface IRoverSquadManager
    {
        List<Rover> Rovers { get; }

        Rover ActiveRover { get; }

        ILandingSurface LandingSurface { get; }

        void DeployRover(int x, int y, Direction direction);
    }


    public interface IVehicle
    {
        int X { get; }

        int Y { get; }

        ILandingSurface LandingSurface { get; }

        Direction Direction { get; }

        void Move(Movement movement);
    }
}