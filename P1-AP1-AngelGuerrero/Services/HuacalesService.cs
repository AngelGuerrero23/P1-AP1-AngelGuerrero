using P1A_P1_AngelGuerrero.Models;
using System.Linq.Expressions;
using P1_AP1_AngelGuerrero.DAL;
using Microsoft.EntityFrameworkCore;

namespace P1_AP1_AngelGuerrero.Services;

public class HuacalesService(IDbContextFactory<Contexto>DbFactory)
{

    public async Task<bool> Guardar(EntradasHuacales huacales)
    {
        if (!await Existe(huacales.IdEntrada))
        {
            return await Insertar(huacales);
        }
        else
        {
            return await Modificar(huacales);
        }
       
    }

    public async Task<bool>Modificar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        contexto.Update(huacales);
        return await contexto.SaveChangesAsync()>0;
    }
    public async Task<bool> Insertar(EntradasHuacales huacales)
     {
         await using var contexto = await DbFactory.CreateDbContextAsync();
         contexto.Add(huacales);
         return await contexto.SaveChangesAsync() > 0;

     }
    public async Task<bool>Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Where(o=>o.IdEntrada == id)
            .ExecuteDeleteAsync()>0;       
    }

    public async Task<EntradasHuacales?>Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales.FirstOrDefaultAsync(o=>o.IdEntrada == id);
    
    }

    public async Task<bool>Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .AnyAsync(o=>o.IdEntrada==id);
    }

    public async Task<List<EntradasHuacales>>Listar(Expression<Func<EntradasHuacales, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}

    

