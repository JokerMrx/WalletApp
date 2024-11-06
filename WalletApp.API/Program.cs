using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Contexts;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load("./.env");
string? databaseUrl = System.Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("Please set environment variable 'DATABASE_URL'");
}
// Add services to the container.
Console.WriteLine($"Database URL: {databaseUrl}");
builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(databaseUrl); });

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();