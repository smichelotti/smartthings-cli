using System;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace SmartThings.Cli
{
    public class ToggleLightCommand : Command
    {
        public ToggleLightCommand(SmartThingsClient smartThings, string name, string description = null) : base(name, description)
        {
            this.Handler = CommandHandler.Create<string>(async(deviceId) =>
            {
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    // TODO: use Colorful.Console NuGet package
                    var defaultColor = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Device ID option (-d or --deviceId) is required!");
                    Console.ForegroundColor = defaultColor;
                    return;
                }

                await smartThings.ToggleLight(deviceId);
            });
        }
    }
}
