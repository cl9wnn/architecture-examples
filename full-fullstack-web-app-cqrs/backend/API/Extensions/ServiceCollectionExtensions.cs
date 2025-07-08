using System.Reflection;
using API.Mappings;
using API.Models.Movies.Requests;
using API.Validation;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Database.Mappings;
using MediatR;
using Path = System.IO.Path;

namespace API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("http://localhost:5173", "http://localhost:3000");
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowCredentials();
            });
        });

        return services;
    }

    public static IServiceCollection AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(expression =>
        {
            expression.AddProfile<ApiMappingProfile>();
            expression.AddProfile<InfrastructureMappingProfile>();
        });
        
        return services;
    }
    
    public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services, IWebHostEnvironment env)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new() { Title = env.ApplicationName, Version = "v1" });

            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });

        return services;
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateMovieRequest>, CreateMovieValidator>();
        services.AddScoped<IValidator<UpdateMovieDescriptionRequest>, UpdateMovieDescriptionValidator>();
        
        services.AddFluentValidationAutoValidation();

        return services;
    }

    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
                cfg.RegisterServicesFromAssembly(Assembly.Load("Application")));        
        
        return services;
    }
}