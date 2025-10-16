using System.Reflection;
using API.GraphQL.Movies;
using API.GraphQL.Movies.Inputs;
using API.GraphQL.Movies.Validators;
using API.Mappings;
using FluentValidation;
using Infrastructure.Database.Mappings;
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
    
    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddScoped<IValidator<CreateMovieInput>, CreateMovieValidator>();
        
        return services;
    }
    
    public static IServiceCollection AddGraphQl(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<MovieQueries>()
            .AddMutationType<MovieMutations>()
            .AddProjections()
            .AddFiltering()
            .AddSorting();
        
        return services;
    }
}