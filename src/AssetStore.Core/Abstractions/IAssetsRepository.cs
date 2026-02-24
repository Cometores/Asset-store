using AssetStore.Core.Models;

namespace AssetStore.Core.Abstractions;

/// <summary>
/// Represents a repository for managing asset entities in a database.
/// Provides methods for retrieving, creating, updating, and deleting assets.
/// </summary>
public interface IAssetsRepository
{
    /// <summary> Retrieves a list of assets from a database asynchronously. </summary>
    /// <returns>
    /// A task representing the asynchronous operation, which upon completion contains a list of <see cref="Asset"/> objects.
    /// </returns>
    Task<List<Asset>> GetAsync();

    /// <summary>Retrieves an asset from a database by its unique identifier asynchronously.</summary>
    /// <param name="id">The unique identifier of the asset to retrieve.</param>
    /// <returns>
    /// A task representing the asynchronous operation, which upon completion contains
    /// the <see cref="Asset"/> object if found, or null if no asset corresponds to the provided identifier.
    /// </returns>
    Task<Asset?> GetByIdAsync(Guid id);

    /// <summary>Creates a new asset in a database asynchronously.</summary>
    /// <param name="asset">The <see cref="Asset"/> object representing the asset to be created.</param>
    /// <returns>
    /// A task representing the asynchronous operation, which upon completion contains the unique identifier of the created asset.
    /// </returns>
    Task<Guid> CreateAsync(Asset asset);

    /// <summary>Updates the specified properties of an asset in a database asynchronously.</summary>
    /// <param name="id">The unique identifier of the asset to update.</param>
    /// <param name="title">The new title for the asset.</param>
    /// <param name="description">The new description for the asset.</param>
    /// <param name="price">The new price for the asset.</param>
    /// <returns>
    /// A task representing the asynchronous operation, which upon completion contains the unique identifier of the updated asset.
    /// </returns>
    Task<Guid> UpdateAsync(Guid id, string title, string description, decimal price);

    /// <summary>Deletes an asset from a database asynchronously.</summary>
    /// <param name="id">The unique identifier of the asset to delete.</param>
    /// <returns>
    /// A task representing the asynchronous operation, which upon completion contains the unique identifier of the deleted asset.
    /// </returns>
    Task<Guid> DeleteAsync(Guid id);
}