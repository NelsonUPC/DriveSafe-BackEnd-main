using _1.API.Mapper;
using _2.Domain;
using _3.Data;
using _3.Data.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserData, UserData>();
builder.Services.AddScoped<IUserDomain, UserDomain>();

builder.Services.AddScoped<IVehicleData, VehicleData>();
builder.Services.AddScoped<IVehicleDomain, VehicleDomain>();

builder.Services.AddScoped<IRentData, RentData>();
builder.Services.AddScoped<IRentDomain, RentDomain>();

builder.Services.AddScoped<IMaintenanceData, MaintenanceData>();
builder.Services.AddScoped<IMaintenanceDomain, MaintenanceDomain>();

builder.Services.AddAutoMapper(typeof(RequestToModel)
    ,typeof(ModelToRequest)
    ,typeof(ModelToResponse));

// Connect DB
var connectionString = builder.Configuration.GetConnectionString("DriveSafeDB");

builder.Services.AddDbContext<DriveSafeDBContext>(
    dbContextOptions =>
    {
        dbContextOptions.UseMySql(connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    });

var app = builder.Build();

//EF
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

app.Run();