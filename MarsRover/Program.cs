using System;
using Microsoft.Extensions.DependencyInjection;

namespace MarsRover
{
    class Program
    {
        static void Main(string[] args)
        {

            //intialise classes
            var serviceProvider = new ServiceCollection()
                .AddSingleton<ILandingSurface, Plataeu>()
                .AddSingleton<IRoverSquadManager, RoverSquadManager>()
                .BuildServiceProvider();
            var commandCenter = new CommandCenter(serviceProvider);


            Console.WriteLine("Input Test Data");
            while (true)
            {
                Console.WriteLine("type exit to stop program");

                //deploy location 
                Console.WriteLine("Enter limits eg 5 5");
                String command = Console.ReadLine();
                if (command.Equals("exit")) break;
                else commandCenter.SendCommand(command);

                // rover coordinates and heading
                Console.WriteLine("Enter rover coordinates x and y and heading(NorthEastWestSouth) eg 5 5 N");
                command = Console.ReadLine();
                if (command.Equals("exit")) break;
                else commandCenter.SendCommand(command);

                //movement commands 
                Console.WriteLine("input movements L-> left, M->move foward R-> right e.g LMLMLM");
                command = Console.ReadLine();
                if (command.Equals("exit")) break;
                else commandCenter.SendCommand(command);

            }

        }
    }
}
