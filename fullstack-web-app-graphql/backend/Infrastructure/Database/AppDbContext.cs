using Infrastructure.Database.Configurations;
using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;

public class AppDbContext(DbContextOptions<AppDbContext> options): DbContext(options)
{
    public DbSet<ActorEntity> Actors { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ActorConfiguration());
        modelBuilder.ApplyConfiguration(new MovieConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}