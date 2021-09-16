using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FacuTheRock.Talks.Net.EFTesting.API.Infrastructure;

namespace TMenos3.NetCore.EntityFramework.Testing.API
{
    public class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .Build()
                .Run();

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseConfiguration()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
