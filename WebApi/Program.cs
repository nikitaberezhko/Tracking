using Infrastructure.Repositories.Implementations;
using Infrastructure.Settings;
using Services.Repositories.Abstractions;
using Services.Services.Abstractions;
using Services.Services.Implementations;
using WebApi.Extensions;
using WebApi.Middlewares;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var services = builder.Services;

        services.AddControllers();
        
        services.Configure<RmqSettings>(builder.Configuration.GetSection("RmqSettings"));
        
        // Extensions
        services.ConfigureApiVersioning();
        services.ConfigureContext(builder.Configuration.GetConnectionString(
            "DefaultConnectionString")!);
        services.ConfigureStatusValidators();
        services.ConfigureAutoMapper();
        services.ConfigureMassTransit(builder.Configuration);

        // ExceptionHandlerMiddleware
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        // ExceptionHandlerMiddleware
        services.AddTransient<ExceptionHandlerMiddleware>();
        
        // Repositories
        services.AddScoped<IStatusRepository, StatusRepository>();
        
        // Services
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