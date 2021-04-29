using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RossQuotes.Models;

namespace RossQuotes.Data
{
    public class QuoteContext : DbContext
    {
        public QuoteContext (DbContextOptions<QuoteContext> options)
            : base(options)
        {
        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Title> Titles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>().ToTable(nameof(Author));
            modelBuilder.Entity<Publisher>().ToTable(nameof(Publisher));
            modelBuilder.Entity<Quote>().ToTable(nameof(Quote));
            modelBuilder.Entity<Tag>().ToTable(nameof(Tag));
            modelBuilder.Entity<Title>().ToTable(nameof(Title));
        }
    }
}
