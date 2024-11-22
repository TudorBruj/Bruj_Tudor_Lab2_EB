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
        public Bruj_Tudor_Lab2_EBContext (DbContextOptions<Bruj_Tudor_Lab2_EBContext> options)
            : base(options)
        {
        }

        public DbSet<Bruj_Tudor_Lab2_EB.Models.Book> Book { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Customer> Customer { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Genre> Genre { get; set; } = default!;
        public DbSet<Bruj_Tudor_Lab2_EB.Models.Authors>? Authors { get; set; }
    }
}

