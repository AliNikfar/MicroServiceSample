using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

IConfiguration config = builder.Configuration;
ConfigurationManager configuration = builder.Configuration;
IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureLogging((loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(config.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();

});

configuration.AddConfiguration(config.GetSection("Logging"));


var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
