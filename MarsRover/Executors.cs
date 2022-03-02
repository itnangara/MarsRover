using System;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover
{

    //from RoverSquadCommandExecuter
    public class RoverSquadCommandExecuter : CommandExecuter
    {
        /*
         *this class receives commands from 
         * the instructions file 
         * and excutes them 
         * it is assumed by the time they reach this class
         * they have been validated
         */

        private readonly IRoverSquadManager _squadManager;

        public RoverSquadCommandExecuter(IServiceProvider serviceProvider)
        {
            _squadManager = serviceProvider.GetService<IRoverSquadManager>();
        }

        public override Regex RegexCommandPattern => new Regex("^\\d+ \\d+ [NSWE]$");

        public override void ExecuteCommand(string command)
        {
            ParseCommand(command, out var x, out var y, out var direction);
            _squadManager.DeployRover(x, y, direction);
        }

        private static void ParseCommand(string command, out int x, out int y, out Direction direction)
        {
            var splitCommand = command.Split(' ');
            x = int.Parse(splitCommand[0]);
            y = int.Parse(splitCommand[1]);
            direction = Enum.Parse<Direction>(splitCommand[2]);
        }
    }

    //RoverCommandExecuter

    public class RoverCommandExecuter : CommandExecuter
    {
        private readonly IRoverSquadManager _squadManager;

        public RoverCommandExecuter(IServiceProvider serviceProvider)
        {
            _squadManager = serviceProvider.GetService<IRoverSquadManager>();
        }

        public override Regex RegexCommandPattern => new Regex("^[LMR]+$");

        public override void ExecuteCommand(string command)
        {
            if (CheckIfActiveRoverExists(out var activeRover))
                return;

            MoveRoverByCommand(command, activeRover);
            ReportLocation(activeRover);
        }

        private static void MoveRoverByCommand(string command, Rover activeRover)
        {
            foreach (var order in command)
            {
                var movement = Enum.Parse<Movement>(order.ToString());
                activeRover.Move(movement);
            }
        }

        private static void ReportLocation(Rover activeRover)
        {
            Console.WriteLine(activeRover.ToString());
        }

        private bool CheckIfActiveRoverExists(out Rover activeRover)
        {
            activeRover = _squadManager.ActiveRover;
            return activeRover == null;
        }
    }


    //from CommandExecuter
    public abstract class CommandExecuter : ICommandExecuter
    {
        public abstract Regex RegexCommandPattern { get; }

        public abstract void ExecuteCommand(string command);

        public bool MatchCommand(string command)
        {
            return RegexCommandPattern.IsMatch(command);
        }
    }

    //From Icommandexecuter
    public interface ICommandExecuter
    {
        Regex RegexCommandPattern { get; }
        void ExecuteCommand(string command);
        bool MatchCommand(string command);
    }

    public class PlataueCommandExecuter : CommandExecuter
    {
        private readonly ILandingSurface _landingSurface;

        public PlataueCommandExecuter(IServiceProvider serviceProvider)
        {
            _landingSurface = serviceProvider.GetService<ILandingSurface>();
        }

        public override Regex RegexCommandPattern => new Regex("^\\d+ \\d+$");

        public override void ExecuteCommand(string command)
        {
            ParseCommand(command, out var width, out var height);
            _landingSurface.Define(width, height);
        }

        private static void ParseCommand(string command, out int width, out int height)
        {
            var splitCommand = command.Split(' ');
            width = int.Parse(splitCommand[0]) + 1;
            height = int.Parse(splitCommand[1]) + 1;
        }
    }
}