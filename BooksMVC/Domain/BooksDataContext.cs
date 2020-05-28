using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksMVC.Domain
{
    public class BooksDataContext : DbContext
    {
        public BooksDataContext(DbContextOptions<BooksDataContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Walden", Author = "Thoreau", InInventory = true, NumberOfPages = 212 },
                new Book { Id = 2, Title = "Nature", Author = "Emerson", InInventory = true, NumberOfPages = 253 },
                new Book { Id = 3, Title = "Memories, Dreams, Reflections", Author = "C.G. Jung", InInventory = false, NumberOfPages = 332 }
                );
        }
    }
}
