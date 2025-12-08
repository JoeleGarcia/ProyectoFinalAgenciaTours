using Microsoft.EntityFrameworkCore;
using ProyectoFinalAgenciaTours.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
   
            builder.Entity<Destino>()
                .HasOne(c => c.Pais)
                .WithMany(e => e.Destinos)
                .HasForeignKey(c => c.PaisId)
                .OnDelete(DeleteBehavior.Cascade);

        }

    }
}
