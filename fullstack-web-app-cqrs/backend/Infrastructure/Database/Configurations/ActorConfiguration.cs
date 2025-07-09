using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ActorConfiguration: IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        builder.ToTable("Actors");
        
        builder.Property(a => a.FirstName)
            .HasMaxLength(64)
            .IsRequired();
        
        builder.Property(a => a.LastName)
            .HasMaxLength(128)
            .IsRequired();
    }
}