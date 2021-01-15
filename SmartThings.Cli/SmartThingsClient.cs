using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SmartThings.Cli
{
    public class SmartThingsClient
    {
        private HttpClient http;

        public SmartThingsClient(HttpClient http) => this.http = http;

        public async Task<string> ListDevices() => await this.http.GetStringAsync($"devices");

        public async Task ToggleLight(string deviceId)
        {
            var json = await this.http.GetStringAsync($"devices/{deviceId}/status");
            dynamic response = JObject.Parse(json);
            string status = response.components.main.light.@switch.value;
            Console.WriteLine($"Attempting to change {deviceId} from {status} to {GetToggledStatus(status)}");
            var request = CreateRequest(status);
            await this.http.PostAsync($"devices/{deviceId}/commands", ToStringContent(request));
        }

        private static CmdRequest CreateRequest(string currStatus) =>
            new CmdRequest(new List<Cmd> { new Cmd("main", "switch", GetToggledStatus(currStatus)) });

        private static string GetToggledStatus(string current) => current == "on" ? "off" : "on";

        private static StringContent ToStringContent<T>(T item) =>
            new StringContent(JsonConvert.SerializeObject(item), Encoding.UTF8, "application/json");

        private record CmdRequest(List<Cmd> commands);
        private record Cmd(string component, string capability, string command);
    }
}
