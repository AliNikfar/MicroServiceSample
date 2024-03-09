using Basket.Api.GrpsServices;
using Basket.Api.Repositories;
using Discount.Grpc.Protos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


IConfiguration configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IBasketRepository,BasketRepository>();
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(
    options=> 
    options.Address = new Uri(configuration["GrpcSetting:DiscountUrl"])
    );
builder.Services.AddStackExchangeRedisCache(option=>
{
    option.Configuration = builder.Configuration.GetValue<string>("CachSettings:ConnectionString");
});
builder.Services.AddScoped<DiscountGrpcService>();
builder.Services.AddMassTransit(config =>
{
    config.UsingRabbitMq((ctx, conf) =>
    {
        conf.Host(configuration.GetValue<string>("EventBusSettings:HostAddress")
        // the ip address is 15672 but according to rabbitmq config we should remove the first character "1" 
        // set connection string for RabbitMQ
    });
});
builder.Services.AddMassTransitHostedService(); // using on MassTransit version 7.1.6 to 7.1.6
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
