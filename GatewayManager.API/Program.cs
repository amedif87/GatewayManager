using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using GatewayManager.Domain.IRepositories;
using GatewayManager.Domain.IServices;
using GatewayManager.Infrastructure.Persistance;
using GatewayManager.Infrastructure.Persistance.Database;
using GatewayManager.Infrastructure.Services;


var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var url = Environment.GetEnvironmentVariable("LOCALRUN") ?? $"http://0.0.0.0:{port}";

// Add services to the container.
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

var connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION");

builder.Services.AddDbContext<DbContext, GatewayContext>(options => 
    options.UseSqlServer(connectionString, b => b.MigrationsAssembly("GatewayManager.Migrations"))
           .UseLazyLoadingProxies());

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    // Include 'SecurityScheme' to use JWT Authentication
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddTransient<IGatewayService, GatewayService>();
builder.Services.AddTransient<IGatewayRepository, GatewayRepository>();
builder.Services.AddTransient<IPeripheralDeviceService, PeripheralDeviceService>();
builder.Services.AddTransient<IPeripheralDeviceRepository, PeripheralDeviceRepository>();


//builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddAuthorization();

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
