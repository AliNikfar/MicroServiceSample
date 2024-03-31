using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOcelot();

IConfiguration config = builder.Configuration;
ConfigurationManager configuration = builder.Configuration;
IHostBuilder hostBuilder = Host.CreateDefaultBuilder(args);

hostBuilder.ConfigureLogging((hostingContext,loggingbuilder) =>
{
    loggingbuilder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
    loggingbuilder.AddConsole();
    loggingbuilder.AddDebug();

});
hostBuilder.ConfigureAppConfiguration((hostingContext,config) =>
{
    config.AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json",true,true);
});



configuration.AddConfiguration(config.GetSection("Logging"));


var app = builder.Build();
await app.UseOcelot();

app.MapGet("/", () => "Hello World!");

app.Run();
