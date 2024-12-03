
using MetaCortex.Customers.API.Extensions;
using MetaCortex.Customers.DataAccess;
using MetaCortex.Customers.DataAccess.Interfaces;
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


var app = builder.Build();
app.UseHttpsRedirection();
app.MapCustomerEndpoints();

app.Run();
