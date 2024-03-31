using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

new WebHostBuilder()
.UseKestrel()
.UseContentRoot(Directory.GetCurrentDirectory())
.ConfigureAppConfiguration((hostingContext, config) =>
{
    config
        .SetBasePath(hostingContext.HostingEnvironment.ContentRootPath)
        .AddJsonFile($"ocelot.{hostingContext.HostingEnvironment.EnvironmentName}.json", true, true)
        .AddJsonFile("ocelot.json")
        .AddEnvironmentVariables();
})
.ConfigureServices(s => {
    s.AddOcelot();
})
.ConfigureLogging((hostingContext, logging) =>
{
    logging.AddDebug();
    //logging.AddConsole(); //Got Error when Activated
    logging.AddConfiguration(configuration.GetSection("Logging"));
})
.UseIISIntegration()
.Configure(app =>
{
    app.UseOcelot().Wait();

})
.Build()
.Run();

