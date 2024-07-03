using Infrastructure.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Services.Mapping;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Implementations;
using WebApi.Extensions;
using WebApi.Mapping;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var services = builder.Services;

        services.AddControllers();
        
        // Extensions
        services.ConfigureApiVersioning();
        services.ConfigureContext(builder.Configuration.GetConnectionString(
            "DefaultConnectionString")!);
        services.AddScoped<DbContext, DataContext>();
        services.ConfigureStatusValidators();

        // ExceptionHandlerMiddleware
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(typeof(ServiceMappingProfile), 
            typeof(ApiMappingProfile));
        
        // ExceptionHandlerMiddleware
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        // Репозитории
        services.AddScoped<IStatusRepository, StatusRepository>();
        
        // Сервисы
        services.AddScoped<IStatusService, StatusService>();

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlerMiddleware>();
        
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();

        app.MapControllers();
        
        app.Run();
    }
}