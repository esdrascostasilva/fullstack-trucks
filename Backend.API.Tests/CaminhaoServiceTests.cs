using Backend.API.Data;
using Backend.API.Models;
using Backend.API.Services;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Tests;

public class CaminhaoServiceTests
{
    private CaminhaoDbContext CreateContext()
    {
        var options = new DbContextOptionsBuilder<CaminhaoDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new CaminhaoDbContext(options);

        context.Modelos.Add(new Modelo { Id = 1, Nome = "FH" });
        context.Plantas.Add(new Planta { Id = 1, Nome = "Brasil" });
        context.SaveChanges();

        return context;
    }

    [Fact]
    public async Task GetAllAsync_DeveRetornarTodosCaminhoes()
    {
        var context = CreateContext();
        var service = new CaminhaoService(context);

        context.Caminhoes.Add(new Caminhao
        {
            Id = 1,
            AnoFabricacao = 2024,
            CodigoChassi = "123rdgc56cy7",
            Cor = "Branco",
            ModeloId = 1,
            PlantaId = 1
        });
        await context.SaveChangesAsync();

        var result = await service.GetAllAsync();

        Assert.Single(result);
        Assert.Equal("123rdgc56cy7", result.First().CodigoChassi);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarCaminhaoPeloId_SeExistir()
    {
        var context = CreateContext();
        var service = new CaminhaoService(context);

        var caminhao = new Caminhao
        {
            Id = 1,
            AnoFabricacao = 2024,
            CodigoChassi = "123rdgc56cy7",
            Cor = "Branco",
            ModeloId = 1,
            PlantaId = 1
        };

        context.Caminhoes.Add(caminhao);
        await context.SaveChangesAsync();

        var result = await service.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("123rdgc56cy7", result.CodigoChassi);
    }

    [Fact]
    public async Task GetByIdAsync_DeveLancarExcecao_QuandoNaoExistir()
    {
        var context = CreateContext();
        var service = new CaminhaoService(context);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            service.GetByIdAsync(1));
    }

    [Fact]
    public async Task CreateAsync_DeveCriarCaminhao()
    {
        var context = CreateContext();
        var service = new CaminhaoService(context);

        var novoCaminhao = new Caminhao
        {
            AnoFabricacao = 2024,
            CodigoChassi = "123rdgc56cy7",
            Cor = "Branco",
            ModeloId = 1,
            PlantaId = 1
        };

        var result = await service.PostAsync(novoCaminhao);

        Assert.NotNull(result);
        Assert.Equal("123rdgc56cy7", result.CodigoChassi);
        Assert.NotEqual(0, result.Id);
    }

    [Fact]
    public async Task UpdateAsync_DeveAtualizarCaminhao_QuandoExistir()
    {
        // Arrange
        var context = CreateContext();
        var service = new CaminhaoService(context);

        var caminhao = new Caminhao
        {
            Id = 1,
            AnoFabricacao = 2024,
            CodigoChassi = "123rdgc56cy7",
            Cor = "Branco",
            ModeloId = 1,
            PlantaId = 1
        };

        context.Caminhoes.Add(caminhao);
        await context.SaveChangesAsync();

        caminhao.Cor = "Vermelho";

        await service.PutAsync(1, caminhao);
        var updatedCaminhao = await context.Caminhoes.FindAsync(1);

        Assert.Equal("Vermelho", updatedCaminhao.Cor);
    }

    [Fact]
    public async Task DeleteAsync_DeveDeletarCaminhao_QuandoExistir()
    {
        var context = CreateContext();
        var service = new CaminhaoService(context);

        var caminhao = new Caminhao
        {
            Id = 1,
            AnoFabricacao = 2024,
            CodigoChassi = "123rdgc56cy7",
            Cor = "Branco",
            ModeloId = 1,
            PlantaId = 1
        };

        context.Caminhoes.Add(caminhao);
        await context.SaveChangesAsync();

        await service.DeleteAsync(1);

        Assert.Empty(context.Caminhoes);
    }
}