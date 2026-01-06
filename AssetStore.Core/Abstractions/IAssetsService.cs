using AssetStore.Core.Models;

namespace AssetStore.Application.Services;

public interface IAssetsService
{
    Task<List<Asset>> GetAllAssetsAsync();
    Task<Guid> CreateAssetAsync(Asset asset);
    Task<Guid> UpdateAssetAsync(Guid id, string title, string description, decimal price);
    Task<Guid> DeleteAssetAsync(Guid id);
}