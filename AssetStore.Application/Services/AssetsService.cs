using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;

namespace AssetStore.Application.Services;

public class AssetsService : IAssetsService
{
    private readonly IAssetsRepository _assetsRepository;

    public AssetsService(IAssetsRepository assetsRepository)
    {
        _assetsRepository = assetsRepository;
    }

    public async Task<List<Asset>> GetAllAssetsAsync() => await _assetsRepository.GetAsync();

    public async Task<Guid> CreateAssetAsync(Asset asset) => await _assetsRepository.Create(asset);

    public async Task<Guid> UpdateAssetAsync(Guid id, string title, string description, decimal price) =>
        await _assetsRepository.UpdateAsync(id, title, description, price);

    public async Task<Guid> DeleteAssetAsync(Guid id) => await _assetsRepository.DeleteAsync(id);
}