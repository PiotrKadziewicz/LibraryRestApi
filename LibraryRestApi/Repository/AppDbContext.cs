using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<BookTitle> BookTitles { get; set; }
        public DbSet<BookCopy> BookCopys { get; set; }
        public DbSet<BookRental> BookRentals { get; set; }
        public DbSet<Reader> Readers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<BookCopy>()
            //    .HasMany(b => b.BookRentals)
            //    .WithOne(t => t.BookCopy)
            //    .HasForeignKey(x=>x.BookCopyId)
            //    .IsRequired(false)
            //    .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<BookCopy>()
                .HasMany(b => b.BookRentals)
                .WithOne(t => t.BookCopy)
                .OnDelete(DeleteBehavior.);
        }
    }
}
