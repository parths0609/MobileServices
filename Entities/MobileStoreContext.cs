using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileServices.Entities
{
    public class MobileStoreContext : DbContext
    {

        public MobileStoreContext(DbContextOptions<MobileStoreContext> options) : base(options)
        {

        }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<Sales> Sales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
