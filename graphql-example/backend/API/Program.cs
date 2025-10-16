using API.Extensions;
using Application.Services;
using Domain.Abstractions.Repositories;
using Domain.Abstractions.Services;
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

app.UseRequestResponseLogging();
app.UseCors();
app.MapGraphQL(); // "/graphql"

app.Services.ApplyMigrations();

app.Run();