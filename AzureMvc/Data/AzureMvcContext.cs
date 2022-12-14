using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AzureMvc.Models;
using System.Runtime.ConstrainedExecution;

namespace AzureMvc.Data
{
    public class AzureMvcContext : DbContext
    {
        public AzureMvcContext(DbContextOptions<AzureMvcContext> options)
            : base(options)
        {
        }

        public DbSet<Amigos> Amigos { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //seed categories
            modelBuilder.Entity<Pais>().HasData(new Pais { PaisId = 1, Nome = "Brasil" });
            modelBuilder.Entity<Pais>().HasData(new Pais { PaisId = 2, Nome = "Argentina" });
            modelBuilder.Entity<Pais>().HasData(new Pais { PaisId = 3, Nome = "Croacia" });

            //seed cars
            modelBuilder.Entity<Estado>().HasData(new Estado
            {
                EstadoId = 1,
                Nome = "Rio De Janeiro",
                PaisId = 1,
                ImagemEstado = "/assets/porsche/riodejaneiro.png",

            });

            modelBuilder.Entity<Estado>().HasData(new Estado
            {
                EstadoId = 2,
                Nome = "Zagrebe",
                PaisId = 3,
                ImagemEstado = "/assets/porsche/Zagrebe.jpeg",

            });
            modelBuilder.Entity<Estado>().HasData(new Estado
            {
                EstadoId = 3,
                Nome = "Buenos Aires",
                PaisId = 2,
                ImagemEstado = "/assets/porsche/buenosaires.png",

            });

        }
    }
}
