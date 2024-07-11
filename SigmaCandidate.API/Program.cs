using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SigmaCandidate.Infrastructure.Data;
using SigmaCandidate.Infrastructure.Repositories.Implementations;
using SigmaCandidate.Infrastructure.Repositories.Interfaces;
using SigmaCandidate.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
                .AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SigmaCandidateDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // options.UseMySql(builder.Configuration.GetConnectionString("MySqlConnection")); // Exemplu pentru MySQL
    // options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSqlConnection")); // Exemplu pentru PostgreSQL
});

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>))
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddMemoryCache()
                .AddScoped<ICacheService, MemoryCacheService>(); ;

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