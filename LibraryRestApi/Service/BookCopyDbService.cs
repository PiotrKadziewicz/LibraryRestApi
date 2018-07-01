using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Service
{
    public class BookCopyDbService : IBookCopyRepository
    {
        private readonly AppDbContext _context;

        public BookCopyDbService(AppDbContext context) => _context = context;

        public async Task<ICollection<BookCopy>> GetAllBooks() =>await _context.BookCopys.Include(t => t.BookTitle).ToListAsync();

        public async Task<BookCopy> GetBookCopy(long id)
        {
            _context.BookCopys.Include(b => b.BookTitle);
            return await _context.BookCopys.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BookCopy> AddBook(BookCopy bookCopy)
        {
            await _context.BookCopys.AddAsync(bookCopy);
            await _context.SaveChangesAsync();
            return bookCopy;
        }

        public async Task<int> CountByStatusAndTitle(string status, long id) => await Task.FromResult<int>(_context.BookCopys.Count(b => b.Status == status.ToLower() && b.Id == id));

        public async Task<ICollection<BookCopy>> GetAllByStatusAndTitle(string status, long id) => await _context.BookCopys.Include(b=>b.Status==status && b.Id ==id).ToListAsync();


        public void DeleteBookCopy(long id)
        {
                var bookCoopy = _context.BookCopys.FirstOrDefault(b => b.Id == id);
                _context.Remove(bookCoopy);
                _context.SaveChanges();
        }
    }
}
