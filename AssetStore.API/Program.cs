using AssetStore.Application.Services;
using AssetStore.Core.Abstractions;
using AssetStore.DataAccess;
using AssetStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AssetStoreDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AssetStoreDB"));
});

builder.Services.AddScoped<IAssetsService, AssetsService>();
builder.Services.AddScoped<IAssetsRepository, AssetsRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
// TODO: Check DB connection or response about connection status