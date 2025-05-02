using AspireTesting.ApiService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// This gets a connection string called "mydatabase" from config
// If using the Aspire host orchestration, adding "addSql" and "adddatabase" will create this config
// If not, having a connectionstring called "mydatabase" will be picked up
builder.AddSqlServerDbContext<MyDbContext>("mydatabase");

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

app.MapGet("/api/data", async (MyDbContext dbContext) =>
{
    await dbContext.Database.MigrateAsync();

    var history = dbContext.TheHistory.ToList();

    var random = new Random();
    var randomNumber = random.Next();

    var historyItem = new MyHistory { RandomNumber = randomNumber.ToString() };
    dbContext.TheHistory.Add(historyItem);
    dbContext.SaveChanges();

    return new { randomNumber, history };
}).WithName("Data");

// Omitting static file serving

app.MapDefaultEndpoints();

app.Run();