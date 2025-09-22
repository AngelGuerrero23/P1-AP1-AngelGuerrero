using Microsoft.EntityFrameworkCore;
using P1_AP1_AngelGuerrero.Components;
using P1_AP1_AngelGuerrero.DAL;

namespace P1_AP1_AngelGuerrero;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        //Obtener el ConStr para usarlo en el contexto

        var ConStr = builder.Configuration.GetConnectionString("ConStr");

        //Agregamos el contexto al builder con el ConStr
        builder.Services.AddDbContextFactory<Contexto>(o=>o.UseSqlite(ConStr));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
