using Microsoft.EntityFrameworkCore;
using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoFinalAgenciaTours.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Destino> Destinos { get; set; }
        public DbSet<Tour> Tours { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
   
            builder.Entity<Destino>()
                .HasOne(c => c.Pais)
                .WithMany(e => e.Destinos)
                .HasForeignKey(c => c.PaisId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Tour>()
                .Property(t => t.Precio)
                .HasColumnType("decimal(9,2)");

            builder.Entity<Tour>()
                .Property(t => t.ITBIS)
                .HasColumnType("decimal(9,2)");

            builder.Entity<Tour>()
                .Property(t => t.TasaImpuesto)
                .HasColumnType("decimal(5,2)");

            builder.Entity<Tour>()
                .HasOne(c => c.Destino)
                .WithMany(m => m.Tour)
                .HasForeignKey(c => c.DestinoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Tour>()
                .HasOne(c => c.Pais)
                .WithMany(m => m.Tour)
                .HasForeignKey(c => c.PaisId)
                .OnDelete(DeleteBehavior.NoAction);




        }

    }
}
