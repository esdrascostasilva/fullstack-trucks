
using Microsoft.EntityFrameworkCore;

namespace Backend.API;

public class CaminhaoService : ICaminhaoService
{
    private readonly CaminhaoDbContext _dbContext;

    public CaminhaoService(CaminhaoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Caminhao>> GetAllAsync()
    {
        return await _dbContext.Caminhoes
            .Include(c => c.Modelo)
            .Include(c => c.Planta)
            .ToListAsync();
    }

    public async Task<Caminhao> GetByIdAsync(int id)
    {
        var caminhao = await _dbContext.Caminhoes
            .Include(c => c.Modelo)
            .Include(c => c.Planta)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (caminhao == null)
            throw new Exception($"Caminhao com o Id {id} nao encontrado");

        return caminhao;
    }

    public async Task<Caminhao> PostAsync(Caminhao caminhao)
    {
        _dbContext.Caminhoes.Add(caminhao);
        await _dbContext.SaveChangesAsync();

        return caminhao;
    }

    public async Task PutAsync(int id, Caminhao novoCaminhao)
    {
        if (id != novoCaminhao.Id)
            throw new Exception($"Id {id} - {novoCaminhao.Id} divergentes");

        var caminhao = await _dbContext.Caminhoes.FindAsync(id);

        if (caminhao == null)
            throw new Exception($"Caminhao com o Id {id} não encontrado");

        _dbContext.Entry(caminhao).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var caminhao = await _dbContext.Caminhoes.FindAsync(id);

        if (caminhao == null)
            throw new Exception($"Caminhao com o Id {id} não encontrado");

        _dbContext.Caminhoes.Remove(caminhao);
        await _dbContext.SaveChangesAsync();
    }
}
