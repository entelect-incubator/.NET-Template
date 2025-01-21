using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Api>("Api");

builder.Build().Run();
