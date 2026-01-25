using AssetStore.API.Contracts;
using AssetStore.Application.Services;
using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace AssetStore.API.Controllers;

[ApiController]
[Route("[controller]")]
public class AssetsController : ControllerBase
{
    private readonly IAssetsService _assetsService;

    public AssetsController(IAssetsService assetsService)
    {
        _assetsService = assetsService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AssetDto>))]
    public async Task<ActionResult<List<AssetDto>>> GetAssetsAsync()
    {
        var assets = await _assetsService.GetAllAssetsAsync();

        var response = assets.Select(a => new AssetDto(a.Id, a.Title, a.Description, a.Price));
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AssetDto))]
    public async Task<ActionResult<AssetDto>> GetAssetAsync(Guid id)
    {
        var asset = await _assetsService.GetAssetByIdAsync(id);
        if (asset == null)
        {
            return NotFound();
        }

        return Ok(new AssetDto(asset.Id, asset.Title, asset.Description, asset.Price));
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAssetAsync([FromBody] AssetForCreationDto assetDto)
    {
        var (asset, error) = Asset.Create(
            Guid.NewGuid(),
            assetDto.Title,
            assetDto.Description,
            assetDto.Price);

        if (!string.IsNullOrEmpty(error))
        {
            return BadRequest(error);
        }

        var assetId = await _assetsService.CreateAssetAsync(asset);

        return Ok(assetId);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<Guid>> UpdateAssetAsync(Guid id, [FromBody] AssetForCreationDto assetDto)
    {
        await _assetsService.UpdateAssetAsync(id, assetDto.Title, assetDto.Description, assetDto.Price);

        return Ok(id);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<Guid>> DeleteAssetAsync(Guid id)
    {
        await _assetsService.DeleteAssetAsync(id);

        return Ok(id);
    }
}