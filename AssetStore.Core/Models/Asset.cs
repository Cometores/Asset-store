namespace AssetStore.Core.Models;

public class Asset
{
    public const int MAX_TITLE_LENGTH = 100;
    
    public Guid Id { get; }
    public string Title { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public decimal Price { get; }

    private Asset(Guid id, string title, string description, decimal price)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
    }
    
    public static (Asset Asset, string Error) Create(Guid id, string title, string description, decimal price)
    {
        var error = string.Empty;

        if (string.IsNullOrEmpty(title) || title.Length > MAX_TITLE_LENGTH)
        {
            error += "Title is required.";
        }
        
        var asset = new Asset(id, title, description, price);
        
        return (asset, error);
    }
}