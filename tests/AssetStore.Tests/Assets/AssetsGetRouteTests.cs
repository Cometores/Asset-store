using AssetStore.API.Contracts;
using AssetStore.API.Controllers;
using AssetStore.Core.Abstractions;
using AssetStore.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AssetStore.Tests.Assets;

public class AssetsGetRouteTests
{
    [Test]
    public async Task GetAssetsAsync_ReturnsOk_WithMappedDtos()
    {
        // Arrange
        var assets = new List<Asset>
        {
            Asset.Create(Guid.NewGuid(), "Title 1", "Desc 1", 10.5m).Asset,
            Asset.Create(Guid.NewGuid(), "Title 2", "Desc 2", 20m).Asset,
        };

        var assetServiceMock = new Mock<IAssetsService>();
        assetServiceMock
            .Setup(s => s.GetAllAssetsAsync())
            .ReturnsAsync(assets);

        var controller = new AssetsController(assetServiceMock.Object);

        // Act
        var result = await controller.GetAssetsAsync();

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var ok = (OkObjectResult)result.Result!;
        Assert.That(ok.Value, Is.Not.Null);

        var dtos = (ok.Value as IEnumerable<AssetDto>)!.ToList();
        Assert.That(dtos, Has.Count.EqualTo(2));

        // Check if asset values are the same
        for (int i = 0; i < assets.Count; i++)
        {
            Assert.Multiple(() =>
            {
                Assert.That(dtos[i].Id, Is.EqualTo(assets[i].Id));
                Assert.That(dtos[i].Title, Is.EqualTo(assets[i].Title));
                Assert.That(dtos[i].Description, Is.EqualTo(assets[i].Description));
                Assert.That(dtos[i].Price, Is.EqualTo(assets[i].Price));
            });
        }

        // Asset service was used only once
        assetServiceMock.Verify(s => s.GetAllAssetsAsync(), Times.Once);
    }

    [Test]
    public async Task GetAssetAsync_ReturnsOk_WhenFound()
    {
        // Arrange
        var id = Guid.NewGuid();
        var asset = Asset.Create(id, "Title X", "Desc X", 99.99m).Asset;

        var serviceMock = new Mock<IAssetsService>();
        serviceMock.Setup(s => s.GetAssetByIdAsync(id))
            .ReturnsAsync(asset);

        var controller = new AssetsController(serviceMock.Object);

        // Act
        var result = await controller.GetAssetAsync(id);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<OkObjectResult>());
        var ok = (OkObjectResult)result.Result!;
        var dto = (AssetDto)ok.Value!;

        // Check if asset values are the same
        Assert.Multiple(() =>
        {
            Assert.That(dto.Id, Is.EqualTo(asset.Id));
            Assert.That(dto.Title, Is.EqualTo(asset.Title));
            Assert.That(dto.Description, Is.EqualTo(asset.Description));
            Assert.That(dto.Price, Is.EqualTo(asset.Price));
        });

        // Asset service was used only once
        serviceMock.Verify(s => s.GetAssetByIdAsync(id), Times.Once);
    }

    [Test]
    public async Task GetAssetAsync_ReturnsNotFound_WhenMissing()
    {
        // Arrange
        var id = Guid.NewGuid();
        var serviceMock = new Mock<IAssetsService>();
        serviceMock.Setup(s => s.GetAssetByIdAsync(id))
            .ReturnsAsync((Asset?)null);

        var controller = new AssetsController(serviceMock.Object);

        // Act
        var result = await controller.GetAssetAsync(id);

        // Assert
        Assert.That(result.Result, Is.InstanceOf<NotFoundResult>());

        // Asset service was used only once
        serviceMock.Verify(s => s.GetAssetByIdAsync(id), Times.Once);
    }
}