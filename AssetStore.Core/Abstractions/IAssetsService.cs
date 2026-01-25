using AssetStore.Core.Models;

namespace AssetStore.Core.Abstractions;

/// <summary>
/// Defines the contract for managing assets within the system.
/// Provides methods for retrieving, creating, updating, and deleting asset entities.
/// </summary>
public interface IAssetsService
{
    /// <summary>Asynchronously retrieves a list of all assets.</summary>
    /// <returns>A task representing the asynchronous operation, containing a list of all assets.</returns>
    Task<List<Asset>> GetAllAssetsAsync();

    /// <summary>Asynchronously retrieves an asset by its unique identifier.</summary>
    /// <param name="id">The unique identifier of the asset to retrieve.</param>
    /// <returns>A task representing the asynchronous operation, containing the asset if found, or null if not found.</returns>
    Task<Asset?> GetAssetByIdAsync(Guid id);

    /// <summary>Asynchronously creates a new asset in the system.</summary>
    /// <param name="asset">The asset entity to be created, containing information such as title, description, and price.</param>
    /// <returns>A task representing the asynchronous operation, containing the unique identifier of the created asset.</returns>
    Task<Guid> CreateAssetAsync(Asset asset);

    /// <summary>Asynchronously updates an existing asset with the specified details. </summary>
    /// <param name="id">The unique identifier of the asset to update.</param>
    /// <param name="title">The new title of the asset.</param>
    /// <param name="description">The new description of the asset.</param>
    /// <param name="price">The new price of the asset.</param>
    /// <returns>A task representing the asynchronous operation, containing the unique identifier of the updated asset.</returns>
    Task<Guid> UpdateAssetAsync(Guid id, string title, string description, decimal price);

    /// <summary>Asynchronously deletes an asset by its unique identifier.</summary>
    /// <param name="id">The unique identifier of the asset to delete.</param>
    /// <returns>A task representing the asynchronous operation, containing the unique identifier of the deleted asset.</returns>
    Task<Guid> DeleteAssetAsync(Guid id);
}