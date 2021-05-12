using Microsoft.EntityFrameworkCore;
using NLayerProject.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Data
{
    public class NLayerContext :DbContext
    {
        public NLayerContext(DbContextOptions<NLayerContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Category
            modelBuilder.Entity<Category>().HasKey(x => x.Id);
            modelBuilder.Entity<Category>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Category>().Property(x => x.Name).IsRequired().HasMaxLength(50);
            //modelBuilder.Entity<Category>().ToTable("Categories");

            //Product
            modelBuilder.Entity<Product>().HasKey(x => x.Id);
            modelBuilder.Entity<Product>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Product>().Property(x => x.Name).IsRequired().HasMaxLength(200);
            modelBuilder.Entity<Product>().Property(x => x.Stock).IsRequired();
            modelBuilder.Entity<Product>().Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(x => x.InnerBarcode).HasMaxLength(50);
            //modelBuilder.Entity<Product>().ToTable("Products");

            //Person
            modelBuilder.Entity<Person>().HasKey(x => x.Id);
            modelBuilder.Entity<Person>().Property(x => x.Id).UseIdentityColumn();
            modelBuilder.Entity<Person>().Property(x => x.Name).HasMaxLength(100);
            modelBuilder.Entity<Person>().Property(x => x.SurName).HasMaxLength(100);
           //modelBuilder.Entity<Product>().ToTable("Persons");
        }

    }
}
