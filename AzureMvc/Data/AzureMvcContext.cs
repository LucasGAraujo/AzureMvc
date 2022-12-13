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

        public DbSet<Amigos> Amigos { get; set; } = default!;
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Estado> Estados { get; set; }
     
    }
}
