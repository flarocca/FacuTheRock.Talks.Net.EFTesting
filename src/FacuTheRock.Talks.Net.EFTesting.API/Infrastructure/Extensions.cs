using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System.Globalization;
using System.IO;

namespace FacuTheRock.Talks.Net.EFTesting.API.Infrastructure
{
    internal static class Extensions
    {
        public static IHostBuilder UseConfiguration(this IHostBuilder builder) =>
            builder.ConfigureAppConfiguration((context, config) =>
            {
                config
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile(GetConfigurationFilePath(), optional: false, reloadOnChange: true)
                    .AddJsonFile(GetConfigurationFilePath($".{context.HostingEnvironment.EnvironmentName}"), optional: true, reloadOnChange: true)
                    .AddEnvironmentVariables();
            });

        private static string GetConfigurationFilePath(string environmentName = null)
            => string.Format(CultureInfo.InvariantCulture, "appsettings{0}.json", environmentName);
    }
}
