using System.Diagnostics;
using API.Extensions;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
using HotChocolate.AspNetCore;
using Infrastructure.Database.Repositories;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddGraphQl();
builder.Services.AddValidation();

var app = builder.Build();

// Автогенерация схемы GraphQl при запуске с отладчиком
if (Debugger.IsAttached)
{
    await app.SaveGraphQlSchemaAsync();
}

app.UseRequestResponseLogging();
app.UseCors();

app.MapGraphQL().WithOptions(new GraphQLServerOptions
{
    Tool =
    {
        Enable = app.Environment.IsDevelopment(),
        ServeMode = GraphQLToolServeMode.Embedded
    },
    EnableGetRequests = false
});

app.Services.ApplyMigrations();

app.Run();