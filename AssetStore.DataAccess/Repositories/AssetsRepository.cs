using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;
using AssetStore.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace AssetStore.DataAccess.Repositories;

public class AssetsRepository : IAssetsRepository
{
    private readonly AssetStoreDbContext _dbContext;
    
    public AssetsRepository(AssetStoreDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Asset>> GetAsync()
    {
        var assetEntities = await _dbContext.Assets
            .AsNoTracking()
            .ToListAsync();
        
        var assets = assetEntities
            .Select(a => Asset.Create(a.Id, a.Title, a.Description, a.Price).Asset)
            .ToList();
        
        return assets;
    }

    public async Task<Guid> Create(Asset asset)
    {
        var assetEntity = new AssetEntity
        {
            Id = asset.Id,
            Title = asset.Title,
            Description = asset.Description,
            Price = asset.Price
        };
        
        await _dbContext.Assets.AddAsync(assetEntity);
        await _dbContext.SaveChangesAsync();
        
        return asset.Id;
    }

    public async Task<Guid> UpdateAsync(Guid id, string title, string description, decimal price)
    {
        await _dbContext.Assets
            .Where(a => a.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(a => a.Title, a => title)
                .SetProperty(a => a.Description, a => description)
                .SetProperty(a => a.Price, a => price));
        
        return id;
    }

    public async Task<Guid> DeleteAsync(Guid id)
    {
        await _dbContext.Assets
            .Where(a => a.Id == id)
            .ExecuteDeleteAsync();
        
        return id;
    }
}