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

    }
}
