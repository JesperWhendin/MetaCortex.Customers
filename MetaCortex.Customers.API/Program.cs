using MetaCortex.Customers.API.Extensions;
using MetaCortex.Customers.API.Interfaces;
using MetaCortex.Customers.API.Services;
using MetaCortex.Customers.DataAccess;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;
using MetaCortex.Customers.DataAccess.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;



var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient($"mongodb://{settings.Host}:{settings.Port}");
});

builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

builder.Services.Configure<RabbitMqConfiguration>(builder.Configuration.GetSection("RabbitMqConfiguration"));

builder.Services.AddSingleton(sp => 
{
    var rabbit = sp.GetRequiredService<IOptions<RabbitMqConfiguration>>().Value;

    return new RabbitMqConfiguration()
    {
        HostName = rabbit.HostName,
        UserName = rabbit.UserName,
        Password = rabbit.Password
    };
});

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IMessageProducerService, MessageProducerService>();
builder.Services.AddSingleton<IMessageConsumerService, MessageConsumerService>();

builder.Services.AddSingleton<ICheckCustomerStatusService, CheckCustomerStatusService>();
builder.Services.AddSingleton<INotifyCustomerService, NotifyCustomerService>();

builder.Services.AddHostedService<MessageConsumerHostedService>();


var app = builder.Build();
app.UseHttpsRedirection();
app.MapCustomerEndpoints();

app.Run();
