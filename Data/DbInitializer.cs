﻿using Bruj_Tudor_Lab2_EB.Models;
using Microsoft.EntityFrameworkCore;
namespace Bruj_Tudor_Lab2_EB.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new Bruj_Tudor_Lab2_EBContext(serviceProvider.GetRequiredService<DbContextOptions<Bruj_Tudor_Lab2_EBContext>>()))
            {
                if (context.Book.Any())
                {
                    return; // BD a fost creata anterior
                }
                context.Book.AddRange(
                new Book
                {
                    Title = "Baltagul",
                    Price=Decimal.Parse("22")},
               
                new Book
                {
                    Title = "Enigma Otiliei",
                    Price=Decimal.Parse("18")},
               
                new Book
                {
                    Title = "Maytrei",
                    Price=Decimal.Parse("27")}
               
                );

                context.Genre.AddRange(
               new Genre { Name = "Roman" },
               new Genre { Name = "Nuvela" },
               new Genre { Name = "Poezie" }
                );
                context.Customer.AddRange(
                new Customer
                {
                    Name = "Popescu Marcela",
                    Adress = "Str. Plopilor, nr. 24",
                    BirthDate = DateTime.Parse("1979-09-01")
                },
                new Customer
                {
                    Name = "Mihailescu Cornel",
                    Adress = "Str. Bucuresti, nr.45, ap. 2",BirthDate=DateTime.Parse("1969 - 07 - 08")}
                );

                context.SaveChanges();
            }
        }
    }
}
