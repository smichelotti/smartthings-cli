using System;
using System.Collections.Generic;
using System.CommandLine;
using System.CommandLine.Invocation;
using Newtonsoft.Json;

namespace SmartThings.Cli
{
    public class ListDevicesCommand : Command
    {
        public ListDevicesCommand(SmartThingsClient smartThings, string name, string description = null) : base(name, description)
        {
            this.Handler = CommandHandler.Create(async() =>
            {
                var json = await smartThings.ListDevices();
                var response = JsonConvert.DeserializeObject<Response>(json);
                foreach (var item in response.items)
                {
                    Console.WriteLine($"{item.deviceId} - {item.label}");
                }
            });
        }

        private record Response(List<Device> items);
        private record Device(string deviceId, string label);
    }
}
