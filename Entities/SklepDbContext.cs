using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApi.Entities
{
    public class SklepDbContext : DbContext
    {
        private string _connectionString =
           "Server=(localdb)\\mssqllocaldb;Database=ShopDb;Trusted_Connection=True;";
        public DbSet<Artykul> Artykuly { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            modelBuilder.Entity<User>()
                .Property(u => u.Email)
                .IsRequired();

            modelBuilder.Entity<Role>()
                .Property(u => u.Name)
                .IsRequired();

            modelBuilder.Entity<Artykul>()
                .Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<Artykul>()
                .Property(r => r.Description)
                .HasMaxLength(100);

            modelBuilder.Entity<Artykul>()
                .Property(r => r.Price)
                .IsRequired();


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
