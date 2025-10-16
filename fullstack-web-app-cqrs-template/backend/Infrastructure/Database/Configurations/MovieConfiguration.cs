using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class MovieConfiguration: IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");
        
        builder.Property(m => m.Title)
            .HasMaxLength(128)
            .IsRequired();
        
        builder.Property(m => m.Description)
            .HasMaxLength(512)
            .IsRequired();
    }
}