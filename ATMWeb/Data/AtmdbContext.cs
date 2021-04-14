using ATMWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATMWeb.Data
{
    public class AtmdbContext : DbContext
    {

        public AtmdbContext(DbContextOptions<AtmdbContext> options):base(options)
        {

        }
        public DbSet<Saque> Saques { get; set; }
        public DbSet<Notas> Notas { get; set; }

    }
}
