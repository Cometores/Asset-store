using AssetStore.Core.Models;

namespace AssetStore.Core.Abstractions;

public interface IAssetsRepository
{
    Task<List<Asset>> GetAsync();
    Task<Guid> Create(Asset asset);
    Task<Guid> UpdateAsync(Guid id, string title, string description, decimal price);
    Task<Guid> DeleteAsync(Guid id);
}