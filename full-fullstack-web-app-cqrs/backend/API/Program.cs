using API.Extensions;
using Domain.Abstractions.Repositories;
using Infrastructure.Database.Repositories;
using Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieRepository, MovieRepository>();

builder.ConfigureSerilog();
builder.Services.AddControllers();
builder.Services.AddCustomCors();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation(builder.Environment);
builder.Services.AddPostgresDb(builder.Configuration);
builder.Services.AddAutoMapper();
builder.Services.AddValidation();
builder.Services.AddMediatr();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwaggerDocumentation(builder.Environment);
}

app.UseRequestResponseLogging();
app.UseCors();
app.MapControllers();

app.Services.ApplyMigrations();

app.Run();