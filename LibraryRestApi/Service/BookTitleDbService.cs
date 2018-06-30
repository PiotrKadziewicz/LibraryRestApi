using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Service
{
    public class BookTitleDbService :IBooktTitleRepository
    {
        private readonly AppDbContext _context;

        public BookTitleDbService(AppDbContext context) => _context = context;

        public async Task<ICollection<BookTitle>> GetAll()
        {
            return await _context.BookTitles.ToListAsync();
        }

        public async Task<BookTitle> GetById(long id)
        {
            return await _context.BookTitles.FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<BookTitle> AddBookTitle(BookTitle bookTitle)
        {
            await _context.AddAsync(bookTitle);
            await _context.SaveChangesAsync();
            return bookTitle;
        }


        public void DeleteReader(long id)
        {
            var bookTitle = _context.BookTitles.FirstOrDefault(b => b.Id == id);
            _context.BookTitles.Remove(bookTitle);
            _context.SaveChanges();
        }

        public async Task<BookTitle> GetBookTitleByAuthorAndTitle(string author, string title)
        {
            return await _context.BookTitles.FirstOrDefaultAsync(t => t.Title == title && t.Author == author);
        }
    }
}
