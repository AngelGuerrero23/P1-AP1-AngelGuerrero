using P1_AP1_AngelGuerrero.Models;
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

    private async Task AfectarExistencia(EntradasHuacalesDetalles[] detalles, TipoOperacion tipoOperacion)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        foreach (var detalle in detalles)
        {
            var huacal = await contexto.TiposHuacales.FindAsync(detalle.TipoId);
            if (tipoOperacion == TipoOperacion.Resta)
                huacal.Existencia -= detalle.Cantidad;
            else
                huacal.Existencia += detalle.Cantidad;
            
        }
        await contexto.SaveChangesAsync();
    }
    public async Task<bool>Modificar(EntradasHuacales huacales)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var huacal = await contexto.EntradasHuacales
            .Include(e => e.EntradasHuacalesDetalles)
            .AsNoTracking()
            .SingleOrDefaultAsync(e => e.IdEntrada == huacales.IdEntrada);
        if (huacal == null) return false;

        await AfectarExistencia(huacal.EntradasHuacalesDetalles.ToArray(),
            TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalles.RemoveRange(huacal.EntradasHuacalesDetalles);
        contexto.Update(huacales);

        await AfectarExistencia(huacales.EntradasHuacalesDetalles.ToArray(),
           TipoOperacion.Suma);

        return await contexto.SaveChangesAsync()>0;
    }
    public async Task<bool> Insertar(EntradasHuacales huacales)
     {
         await using var contexto = await DbFactory.CreateDbContextAsync();
         contexto.EntradasHuacales.Add(huacales);
         await AfectarExistencia(huacales.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Suma);
         return await contexto.SaveChangesAsync() > 0;

     }
    public async Task<bool>Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        var huacal = await contexto.EntradasHuacales
            .Include(o=>o.EntradasHuacalesDetalles)
            .FirstOrDefaultAsync(c => c.IdEntrada == id);
        
        if(huacal == null) return false;

        await AfectarExistencia(huacal.EntradasHuacalesDetalles.ToArray(), TipoOperacion.Resta);

        contexto.EntradasHuacalesDetalles.RemoveRange(huacal.EntradasHuacalesDetalles);
        contexto.EntradasHuacales.Remove(huacal);
        var cantidad = await contexto.SaveChangesAsync();
        return cantidad > 0;
    }

    public async Task<EntradasHuacales?>Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.EntradasHuacales
            .Include(d=>d.EntradasHuacalesDetalles).FirstOrDefaultAsync(o=>o.IdEntrada == id);
    
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

    public async Task<List<TiposHuacales>>ListarTipo()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.TiposHuacales
            .Where(h=>h.TipoId>0)
            .AsNoTracking()
            .ToListAsync();

    }

}

public enum TipoOperacion
    {
        Suma =1,
        Resta =2
    }



