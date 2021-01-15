using System;
using System.CommandLine;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace SmartThings.Cli
{
    class Program
    {
        private static ServiceProvider serviceProvider = ServiceProviderFactory.GetServiceProvider();

        static async Task<int> Main(string[] args)
        {
            Console.WriteLine("ARGS:");
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine($"arg: {args[i]}");
            }
            var smartThingsClient = serviceProvider.GetService<SmartThingsClient>();

            var rootCmd = new RootCommand()
            {
                new ListDevicesCommand(smartThingsClient, "list-devices", "Lists all device names and ids"),
                new ToggleLightCommand(smartThingsClient, "toggle", "Toggles light between on/off.")
                    { new Option<string>(new[] { "-d", "--deviceId" }, "Device ID") }
            };
            var r = await rootCmd.InvokeAsync(args);
            //Console.ReadLine();
            return r;
        }
    }
}
