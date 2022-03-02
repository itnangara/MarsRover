using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MarsRover
{
    public class CommandCenter : ICommandCenter
    {
        /*
         *this class is responsible for processing
         *all the input instructions 
         *provided for the rover navigation
         */
        private readonly IServiceProvider _serviceProvider;

        public CommandCenter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SendCommand(string command)
        {
            var commandExecuters = GetCommandExecutersInAssembly();

            var executer = commandExecuters.SingleOrDefault(x => x.MatchCommand(command));
            executer?.ExecuteCommand(command);
        }

        private IEnumerable<CommandExecuter> GetCommandExecutersInAssembly()
        {
            return Assembly.GetExecutingAssembly()
            .DefinedTypes
            .Where(type => type.IsSubclassOf(typeof(CommandExecuter)) && !type.IsAbstract)
            .Select(x => Activator.CreateInstance(x, _serviceProvider) as CommandExecuter)
            .ToList();
        }
    }

    public interface ICommandCenter
    {
        void SendCommand(string command);
    }

}