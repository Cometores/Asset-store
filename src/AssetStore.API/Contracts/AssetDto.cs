namespace AssetStore.API.Contracts;

public record AssetDto(
    Guid Id,
    string Title,
    string Description,
    decimal Price);