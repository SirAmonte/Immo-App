using AAD.ImmoWin.Business.Classes;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAD.ImmoWin.WPF.DBContext
{
    public class ImmoWinDBContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source=(localdb)\MSSQLLocalDB;initial catalog=testKlant2");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Woning>()
            .HasOne(w => w.Adres)
            .WithOne()
            .HasForeignKey<Woning>()
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Klant>()
                .HasOne(k => k.Adres)
                .WithOne()
                .HasForeignKey<Klant>()
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Klant> Klanten { get; set; }
        public DbSet<Woning> Woningen { get; set; }
        public DbSet<Adres> Adressen { get; set; }
        public DbSet<Huis> Huizen { get; set; }
        public DbSet<Appartement> Appartementen { get; set; }
    }
}
