using P1A_P1_AngelGuerrero.Models;
using System.Linq.Expressions;
using P1_AP1_AngelGuerrero.DAL;
using Microsoft.EntityFrameworkCore;

namespace P1_AP1_AngelGuerrero.Services;

public class RegistrosService(IDbContextFactory<Contexto>DbFactory)
{

    public async Task<List<Registro>>Listar(Expression<Func<Registro, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Registros
            .Where(criterio)
            .AsNoTracking()
            .ToListAsync();
    }
}
