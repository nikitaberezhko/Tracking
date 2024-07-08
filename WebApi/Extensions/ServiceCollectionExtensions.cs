using Asp.Versioning;
using FluentValidation;
using Infrastructure.Bus.Implementations.Consumers;
using Infrastructure.Settings;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Services.Mapping;
using Services.Services.Models.Request;
using Services.Validators;
using WebApi.Mapping;

namespace WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureApiVersioning(
        this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1);
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new UrlSegmentApiVersionReader(),
                new HeaderApiVersionReader("X-Api-Version"));
        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'V";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
    
    public static IServiceCollection ConfigureContext(this IServiceCollection services, 
        string connectionString)
    {
        services.AddDbContext<DataContext>(options => options.UseNpgsql(connectionString));
        services.AddScoped<DbContext, DataContext>();
        return services;
    }
    
    public static IServiceCollection ConfigureStatusValidators(
        this IServiceCollection services)
    {
        services.AddScoped<IValidator<GetStatusModel>, GetStatusValidator>();
        services.AddScoped<IValidator<GetStatusByOrderIdModel>, GetStatusByOrderIdValidator>();
        services.AddScoped<IValidator<UpdateStatusModel>, UpdateStatusValidator>();
        
        return services;
    }
    
    public static IServiceCollection ConfigureAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ServiceMappingProfile), 
            typeof(ApiMappingProfile));
        return services;
    }
    
    public static IServiceCollection ConfigureMassTransit(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rmqSettings = configuration.GetSection("RmqSettings").Get<RmqSettings>();
        
        services.AddMassTransit(options =>
        {
            options.AddConsumer<CreateOrderConsumer>();
            
            options.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rmqSettings.Host, rmqSettings.Vhost, h =>
                {
                    h.Username(rmqSettings.Username);
                    h.Password(rmqSettings.Password);
                });
                
                cfg.ReceiveEndpoint("create-order", e =>
                {
                    e.ConfigureConsumer<CreateOrderConsumer>(context);
                });
            });
        });

        return services;
    }
}