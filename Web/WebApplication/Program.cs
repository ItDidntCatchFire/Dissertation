using Microsoft.AspNetCore.Blazor.Hosting;

namespace WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebAssemblyHostBuilder CreateHostBuilder(string[] args)
        {
            var host = BlazorWebAssemblyHost.CreateDefaultBuilder()
                .UseBlazorStartup<Startup>();

            return host;
        }
    }
}