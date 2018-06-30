using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Service
{
    public class BookRentalDbService : IBookRentalRepository
    {
        private readonly AppDbContext _context;
        public BookRentalDbService(AppDbContext context) => _context = context;

        public async Task<ICollection<BookRental>> GetAll()
        {
            return await _context.BookRentals.ToListAsync();
        }

        public async Task<BookRental> AddRental(BookRental bookRental)
        {
            await _context.BookRentals.AddAsync(bookRental);
            await _context.SaveChangesAsync();
            return bookRental;
        }

        public async Task<BookRental> GetById(long id)
        {
            return await _context.BookRentals.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<BookRental> GetByBookAndUser(long bookId, long readerId)
        {
            return await _context.BookRentals.FirstOrDefaultAsync(r =>
                r.BookCopy.Id == bookId && r.Reader.Id == readerId);
        }
    }
}
