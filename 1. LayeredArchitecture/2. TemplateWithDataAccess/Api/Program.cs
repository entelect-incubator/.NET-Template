namespace Api;

using Common.Logging.Static;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

public class Program
{
    public static void Main(string[] args)
    {
        Log.Logger = LoggerSetup.ConfigureLogging().CreateLogger();
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(b =>
            {
                b.UseKestrel(opt => opt.AddServerHeader = false);
                b.UseStartup<Startup>();
            }).UseSerilog();
}
