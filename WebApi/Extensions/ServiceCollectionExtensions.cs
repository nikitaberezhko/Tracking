using Asp.Versioning;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityFramework;
using Services.Services.Models.Request;
using Services.Validators;

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
}