using Microsoft.EntityFrameworkCore;
using WalletApp.BL.Configs;
using WalletApp.BL.Contexts;
using WalletApp.BL.Repositories;
using WalletApp.BL.Utils;
using WalletApp.Core.Repositories;

var builder = WebApplication.CreateBuilder(args);

DotNetEnv.Env.Load("./.env");
string? databaseUrl = System.Environment.GetEnvironmentVariable("DATABASE_URL");

if (string.IsNullOrEmpty(databaseUrl))
{
    throw new Exception("Please set environment variable 'DATABASE_URL'");
}

var mapper = MapperConfig.RegisterMapperConfig().CreateMapper();
// Add services to the container.
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(databaseUrl); });
builder.Services.AddScoped<ICardBalanceRandom, CardBalanceRandom>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICardRepository, CardRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

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