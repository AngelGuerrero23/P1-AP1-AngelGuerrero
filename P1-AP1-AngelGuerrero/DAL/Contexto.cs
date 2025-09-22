using Microsoft.EntityFrameworkCore;
using P1A_P1_AngelGuerrero.Models;

namespace P1_AP1_AngelGuerrero.DAL;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options): base(options)
    {

    }

    public DbSet<Registro> Registros {  get; set; } 
}
