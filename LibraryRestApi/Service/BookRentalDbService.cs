using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Dtos;
using LibraryRestApi.Helpers;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Service
{
    public class BookRentalDbService : IBookRentalRepository
    {
        private readonly AppDbContext _context;
        public BookRentalDbService(AppDbContext context) => _context = context;

        public async Task<ICollection<BookRental>> GetAll() => await _context.BookRentals.Include(r => r.BookCopy).Include(t => t.Reader).ToListAsync();


        public async Task<BookRental> AddRental(AddBookRentalDto addBookRentalDto)
        {
            var bookTitle = await _context.BookTitles.FirstOrDefaultAsync(t => t.Author == addBookRentalDto.Author && t.Title == addBookRentalDto.Title);

            if (bookTitle != null)
            {
                ISet<BookCopy> freeBooks = _context.BookCopys.Where(c => c.BookTitle.Id == bookTitle.Id && c.Status == "Free").ToHashSet();
                var reader = await _context.Readers.FirstOrDefaultAsync(r => r.Id == addBookRentalDto.UserId);
                if (freeBooks.Any() && reader.Account >= 3)
                {
                    var copy = freeBooks.First();
                    copy.Status = "Borrowed";
                    var bookRental = new BookRental(DateTime.Now, copy, reader);
                    await _context.BookRentals.AddAsync(bookRental);
                    await _context.SaveChangesAsync();
                    return bookRental;
                }
            }
            throw new NotFoundException("Book or user not found");
        }

        public async Task<BookRental> ReturnByBookAndUser(long bookId, long readerId)
        {
            var bookRental = await _context.BookRentals.FirstOrDefaultAsync(r => r.BookCopy.Id == bookId && r.Reader.Id == readerId);
            bookRental.ReturnDate = DateTime.Now;
            var diff = bookRental.RentDate.Subtract(bookRental.ReturnDate).TotalDays;
            var reader = await _context.Readers.FirstOrDefaultAsync(r => r.Id == readerId);
            if (diff > 60 && reader != null)
            {
                reader.Account -= (decimal)3.0;
            }
            bookRental.BookCopy.Status = "Free";
            await _context.SaveChangesAsync();
            return bookRental;
        }
    }
}
