namespace AssetStore.API.Contracts;

public record AssetForCreationDto(
    string Title,
    string Description,
    decimal Price);