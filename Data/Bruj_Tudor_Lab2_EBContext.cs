using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Bruj_Tudor_Lab2_EB.Models;

namespace Bruj_Tudor_Lab2_EB.Data
{
    public class Bruj_Tudor_Lab2_EBContext : DbContext
    {
        public Bruj_Tudor_Lab2_EBContext(DbContextOptions<Bruj_Tudor_Lab2_EBContext> options)
            : base(options)
        {
        }

        public DbSet<Bruj_Tudor_Lab2_EB.Models.Book> Book { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Authors>? Authors { get; set; }
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Order>? Order { get; set; }

        public DbSet<Publisher> Publisher { get; set; } = default!;
        public DbSet<PublishedBook> PublishedBooks { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author) 
                .WithMany(a => a.Books) 
                .HasForeignKey(b => b.AuthorID); 

            modelBuilder.Entity<Genre>()
                .HasMany(g => g.Books)
                .WithOne(b => b.Genre)
                .HasForeignKey(b => b.GenreID);
        }
    }
}
