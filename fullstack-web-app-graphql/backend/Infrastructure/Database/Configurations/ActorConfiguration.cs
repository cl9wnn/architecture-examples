using Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database.Configurations;

public class ActorConfiguration: IEntityTypeConfiguration<ActorEntity>
{
    public void Configure(EntityTypeBuilder<ActorEntity> builder)
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