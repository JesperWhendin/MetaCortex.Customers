using MetaCortex.Customers.DataAccess.Entities;
using MetaCortex.Customers.DataAccess.Interfaces;
using MetaCortex.Customers.DataAccess.MessageBroker;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace MetaCortex.Customers.API.Extensions;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/customers");

        group.MapGet("", GetAllCustomersAsync);
        group.MapGet("{id}", GetCustomerByIdAsync);
        group.MapGet("{email}", GetCustomerByEmailAsync);
        group.MapGet("notifications", GetByAllowNotificationsAsync);
        group.MapPost("", AddCustomerAsync);
        group.MapPut("", UpdateCustomerAsync);
        group.MapDelete("{id}", DeleteCustomerAsync);

        return app;
    }

    public static async Task<IResult> GetAllCustomersAsync(ICustomerRepository repo, IMessageProducerService msg)
    {
        var customers = await repo.GetAllAsync();
        if (!customers.Any())
        {
            return Results.NotFound();
        }
        
        //foreach (var customer in customers)
        //{
        //    await msg.SendMessageAsync(customer, "test-queue");
        //}
        return Results.Ok(customers);
    }

    public static async Task<IResult> GetCustomerByIdAsync(ICustomerRepository repo, string id)
    {
        var customer = await repo.GetByIdAsync(id);
        if (customer is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(customer);
    }

    public static async Task<IResult> GetCustomerByEmailAsync(ICustomerRepository repo, string email)
    {
        var customer = await repo.GetByEmailAsync(email);
        if (customer is null)
        {
            return Results.NotFound();
        }
        return Results.Ok(customer);
    }

    public static async Task<IResult> GetByAllowNotificationsAsync(ICustomerRepository repo)
    {
        var customers = await repo.GetByAllowNotificationsAsync();
        if (!customers.Any())
            return Results.NotFound();
        
        return Results.Ok(customers);
    }

    public static async Task<IResult> AddCustomerAsync(ICustomerRepository repo, IMessageProducerService msg, Customer customer)
    {
        await repo.AddAsync(customer);
        return Results.Created($"/api/customer/{customer.Id}", customer);
    }

    public static async Task<IResult> UpdateCustomerAsync(ICustomerRepository repo, Customer customer)
    {
        await repo.UpdateAsync(customer);
        return Results.Ok(customer);
    }

    public static async Task<IResult> DeleteCustomerAsync(ICustomerRepository repo, string id)
    {
        await repo.DeleteAsync(id);
        return Results.Ok("Entity deleted.");
    }
}