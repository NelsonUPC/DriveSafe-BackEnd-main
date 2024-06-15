using System.Reflection;
using DriveSafe.Application.Publishing.CommandServices;
using DriveSafe.Application.QueryServices;
using DriveSafe.Domain.Publishing.Repositories;
using DriveSafe.Domain.Publishing.Services;
using DriveSafe.Infraestructure.Publishing.Persistence;
using DriveSafe.Infraestructure.Shared.Context;
using DriveSafe.Presentation.Mapper;
using DriveSafe.Presentation.Middleware;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

//Ad cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllPolicy", 
        policy => policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});


// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

//builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//dependency inyection
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserCommandService, UserCommandService>();
builder.Services.AddScoped<IUserQueryService, UserQueryService>();

//Automapper
builder.Services.AddAutoMapper(
    typeof(RequestToModel),
    typeof(ModelToRequest),
    typeof(ModelToResponse));

//Connect DB
var connectionString = builder.Configuration.GetConnectionString("DriveSafeDB");

builder.Services.AddDbContext<DriveSafeDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
using (var context = scope.ServiceProvider.GetService<DriveSafeDBContext>())
{
    context.Database.EnsureCreated();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowAllOrigins"); 

app.Run();