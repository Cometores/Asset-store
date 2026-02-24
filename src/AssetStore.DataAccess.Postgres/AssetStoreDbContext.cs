using AssetStore.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetStore.DataAccess.Postgres;

/// <summary>
/// Represents the database context for the Asset Store application.
/// Provides access to the database entities and configurations using Entity Framework Core.
/// </summary>
public class AssetStoreDbContext(DbContextOptions<AssetStoreDbContext> options) : DbContext(options)
{
    public DbSet<AssetEntity> Assets { get; set; }
}