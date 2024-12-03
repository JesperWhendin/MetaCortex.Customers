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

builder.Services.AddSingleton(sp => new RabbitMqConfiguration()
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
});

builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddSingleton<IMessageProducerService, MessageProducerService>();
builder.Services.AddSingleton<IMessageConsumerService, MessageConsumerService>();
builder.Services.AddHostedService<MessageConsumerHostedService>();


var app = builder.Build();
app.UseHttpsRedirection();
app.MapCustomerEndpoints();

app.Run();
