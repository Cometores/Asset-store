using AssetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetStore.DataAccess;

public class AssetStoreDbContext : DbContext
{
    public DbSet<AssetEntity> Assets { get; set; }

    public AssetStoreDbContext(DbContextOptions<AssetStoreDbContext> options) : base(options)
    {
    }
}