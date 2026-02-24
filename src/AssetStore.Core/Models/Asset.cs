namespace AssetStore.Core.Models;

/// <summary>
/// Represents an asset entity for the business layer.
/// Provides functionality to create an asset instance with basic validation.
/// </summary>
public class Asset
{
    public const int MAX_TITLE_LENGTH = 100;

    public Guid Id { get; }

    public string Title { get; }

    public string Description { get; }

    public decimal Price { get; }

    private Asset(Guid id, string title, string description, decimal price)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }

    /// <summary>
    /// Creates a new asset with the specified parameters. Validates the input data and returns
    /// the created asset instance along with any validation errors encountered during the creation process.
    /// </summary>
    /// <param name="id">The unique identifier for the asset.</param>
    /// <param name="title">The title of the asset. It must not be null or exceed the maximum allowed length.</param>
    /// <param name="description">The description of the asset providing additional details.</param>
    /// <param name="price">The price of the asset in decimal format.</param>
    /// <returns>
    /// A tuple containing the created <see cref="Asset"/> instance and a validation error message as a string.
    /// If there are no validation errors, the error string will be empty.
    /// </returns>
    public static (Asset Asset, string Error) Create(Guid id, string title, string description, decimal price)
    {
        string error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            error += "Title is required.";
        }

        var asset = new Asset(id, title, description, price);

        return (asset, error);
    }
}