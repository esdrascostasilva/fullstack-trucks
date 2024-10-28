using Backend.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.API.Data;

public class CaminhaoDbContext : DbContext
{
    public CaminhaoDbContext(DbContextOptions<CaminhaoDbContext> options) : base(options)
    {

    }

    public DbSet<Caminhao> Caminhoes { get; set; }
    public DbSet<Modelo> Modelos { get; set; }
    public DbSet<Planta> Plantas { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Caminhao>()
            .HasOne(c => c.Modelo)
            .WithMany(m => m.Caminhoes)
            .HasForeignKey(c => c.ModeloId);

        builder.Entity<Caminhao>()
            .HasOne(c => c.Planta)
            .WithMany(p => p.Caminhoes)
            .HasForeignKey(c => c.PlantaId);

        builder.Entity<Modelo>().HasData(
            new Modelo { Id = 1, Nome = "FH" },
            new Modelo { Id = 2, Nome = "FM" },
            new Modelo { Id = 3, Nome = "VM" }
        );

        builder.Entity<Planta>().HasData(
            new Planta { Id = 1, Nome = "Brasil" },
            new Planta { Id = 2, Nome = "Suécia" },
            new Planta { Id = 3, Nome = "Estados Unidos" },
            new Planta { Id = 4, Nome = "França" }
        );
    }
}
