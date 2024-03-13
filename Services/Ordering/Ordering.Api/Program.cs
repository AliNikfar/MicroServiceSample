using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.Extensions;
using MassTransit;
using EventBus.Messages.Common;
using Ordering.Api.EventBusConsumer;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
IConfiguration configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServicees(builder.Configuration);
builder.Services.AddSwaggerGen();

var serviceProvider = builder.Services.BuildServiceProvider();
var logger = serviceProvider.GetService<ILogger<OrderContext>>();
builder.Services.AddMassTransit(config =>
{
    //add consumer configs
    config.AddConsumer<BasketCheckoutConsumer>();
    config.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(configuration.GetValue<string>("EventBusSettings:HostAddress"));
        // the ip address is 15672 but according to rabbitmq config we should remove the first character "1" 
        // set connection string for RabbitMQ

        //add consumer configs
        conf.ReceiveEndpoint(EventBusConstants.BasketCheckoutQueue,c=>
        {
            c.ConfigureConsumer<BasketCheckoutConsumer>(ctx);
        });

    });
});
builder.Services.AddMassTransitHostedService(); // using on MassTransit version 7.1.6 to 7.1.6
builder.Services.AddSingleton(typeof(ILogger), logger);
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
})
.Run();
