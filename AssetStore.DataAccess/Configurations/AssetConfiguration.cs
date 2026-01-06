using AssetStore.Core.Models;
using AssetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AssetStore.DataAccess.Configurations;

public class AssetConfiguration : IEntityTypeConfiguration<AssetEntity>
{
    public void Configure(EntityTypeBuilder<AssetEntity> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Title)
            .HasMaxLength(Asset.MAX_TITLE_LENGTH)
            .IsRequired();

        builder.Property(x => x.Description)
            .IsRequired();
        
        builder.Property(x => x.Price)
            .IsRequired();
    }
}