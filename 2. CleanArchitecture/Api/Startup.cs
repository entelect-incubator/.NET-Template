namespace Api;

using Api.StartupApp.App;
using Api.StartupApp.Services;
using Domain;
using Infrastructure;

public class Startup
{
    private const string DatabaseName = "GDEDB";

    public Startup(IConfiguration configuration)
    {
        this.ConfigRoot = configuration;
        this.ConfigRoot.Bind("Settings", new Settings());
    }

    public IConfiguration ConfigRoot
    {
        get;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddCommon();
        services.AddSecurity();
        services.AddInfrastructure();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.AddCommon();
        app.AddSecurity();
    }
}