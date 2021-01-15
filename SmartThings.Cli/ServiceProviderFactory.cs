using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace SmartThings.Cli
{
    public class ServiceProviderFactory
    {
        public static ServiceProvider GetServiceProvider()
        {
            IConfiguration configuration = new ConfigurationBuilder().AddUserSecrets<ServiceProviderFactory>().Build();
            var services = new ServiceCollection();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpClient<SmartThingsClient>(c =>
            {
                c.BaseAddress = new Uri("https://api.smartthings.com/v1/");
                c.DefaultRequestHeaders.Add("Authorization", $"Bearer {configuration["st-api-key"]}");
            });

            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider;
        }
    }
}
