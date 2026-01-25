using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;
using AssetStore.DataAccess.Postgres.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetStore.DataAccess.Postgres.Repositories;

/// <summary>
/// Represents a repository for managing asset data in the PostgreSQL database.
/// Provides methods to perform CRUD (Create, Read, Update, Delete) operations on assets.
/// </summary>
public class AssetsRepository(AssetStoreDbContext dbContext) : IAssetsRepository
{
    /// <inheritdoc />
    public async Task<List<Asset>> GetAsync()
    {
        var assetEntities = await dbContext.Assets
            .AsNoTracking()
            .ToListAsync();

        var assets = assetEntities
            .Select(a => Asset.Create(a.Id, a.Title, a.Description, a.Price).Asset)
            .ToList();

        return assets;
    }

    /// <inheritdoc />
    public async Task<Asset?> GetByIdAsync(Guid id)
    {
        var assetEntity = await dbContext.Assets
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return assetEntity != null
            ? Asset.Create(assetEntity.Id, assetEntity.Title, assetEntity.Description, assetEntity.Price).Asset
            : null;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Asset asset)
    {
        var assetEntity = new AssetEntity
        {
            Id = asset.Id, Title = asset.Title, Description = asset.Description, Price = asset.Price,
        };

        await dbContext.Assets.AddAsync(assetEntity);
        await dbContext.SaveChangesAsync();

        return asset.Id;
    }

    /// <inheritdoc />
    public async Task<Guid> UpdateAsync(Guid id, string title, string description, decimal price)
    {
        await dbContext.Assets
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Title, a => title)
                .SetProperty(a => a.Description, a => description)
                .SetProperty(a => a.Price, a => price));

        return id;
    }

    /// <inheritdoc />
    public async Task<Guid> DeleteAsync(Guid id)
    {
        await dbContext.Assets
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();

        return id;
    }
}