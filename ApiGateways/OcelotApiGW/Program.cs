using Microsoft.Extensions.Hosting;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

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
    s.AddOcelot()
    .AddCacheManager(x => x.WithDictionaryHandle()); ;
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

