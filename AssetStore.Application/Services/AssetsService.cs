using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;

namespace AssetStore.Application.Services;

/// <summary>
/// Provides services for managing asset models within the system.
/// This class acts as a bridge between business logic and data access layers,
/// leveraging the <see cref="IAssetsRepository"/> to perform operations such as
/// retrieving, creating, updating, and deleting asset entities in a database.
/// </summary>
public class AssetsService(IAssetsRepository assetsRepository) : IAssetsService
{
    /// <inheritdoc />
    public async Task<List<Asset>> GetAllAssetsAsync() => await assetsRepository.GetAsync();

    /// <inheritdoc />
    public async Task<Asset?> GetAssetByIdAsync(Guid id) => await assetsRepository.GetByIdAsync(id);

    /// <inheritdoc />
    public async Task<Guid> CreateAssetAsync(Asset asset) => await assetsRepository.CreateAsync(asset);

    /// <inheritdoc />
    public async Task<Guid> UpdateAssetAsync(Guid id, string title, string description, decimal price) =>
        await assetsRepository.UpdateAsync(id, title, description, price);

    /// <inheritdoc />
    public async Task<Guid> DeleteAssetAsync(Guid id) => await assetsRepository.DeleteAsync(id);
}