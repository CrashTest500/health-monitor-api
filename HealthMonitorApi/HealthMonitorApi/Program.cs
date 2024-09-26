using HealthMonitorApi.Business;
using HealthMonitorApi.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddHealthChecks();

// Custom Services
builder.Services.AddTransient<IServiceTargetContext, ServiceTargetContext>();
builder.Services.AddTransient<IServiceTargetRepository, ServiceTargetRepository>();

var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
builder.Services.AddSingleton<IConfigurationRoot>(config);

var mongoClient = new MongoClient(config["MongoDB:ConnectionString"]);
builder.Services.AddSingleton<MongoClient>(mongoClient);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapGet("/groups", async ([FromServices] IServiceTargetContext context) =>
{
    Guid transaction = Guid.NewGuid();
    return await context.GetGroups(transaction);
})
.WithName("GetGroups")
.WithOpenApi();

app.MapPost("/group", async ([FromServices] IServiceTargetContext context, [FromBody] ServiceTargetGroup group) =>
{
    Guid transaction = Guid.NewGuid();
    await context.AddGroup(transaction, group);
    return Results.Ok();
})
.WithName("AddGroup")
.WithOpenApi();

app.MapHealthChecks("/healthCheck");

app.Run();