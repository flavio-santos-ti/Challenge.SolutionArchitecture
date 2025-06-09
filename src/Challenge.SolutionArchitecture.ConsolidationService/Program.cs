using Challenge.SolutionArchitecture.ConsolidationService.Configuration;
using Challenge.SolutionArchitecture.ConsolidationService.Data;
using Challenge.SolutionArchitecture.ConsolidationService.Http.Clients;
using Challenge.SolutionArchitecture.ConsolidationService.Repositories;
using Challenge.SolutionArchitecture.ConsolidationService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddDbContext<ConsolidationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IDailyBalanceRepository, DailyBalanceRepository>();
builder.Services.AddScoped<IDailyBalanceService, DailyBalanceService>();

// Configurações externas (serviços integrados)
builder.Services.Configure<LaunchingServiceOptions>(
    builder.Configuration.GetSection("Services:LaunchingService"));

// Registro do HTTP Client para consumo do LaunchingService
builder.Services.AddHttpClient<ILaunchingServiceClient, LaunchingServiceClient>();

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
