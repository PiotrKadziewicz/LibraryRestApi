using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using LibraryRestApi.Service;
using LibraryRestApi.Dtos;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace LibraryRestApiTestSuite
{
    public class BookRentalDbServiceTestSuite
    {
        private readonly AppDbContext context;
        private readonly BookRentalDbService service;

        public BookRentalDbServiceTestSuite()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            this.context = new AppDbContext(options);
            this.service = new BookRentalDbService(context);
        }

        [Fact]
        public async Task GetAllTest()
        {
            //Arrange
            var reader = new Reader() { Account = 10, LastName = "Test Lastname", Name = "Test name" };
            context.Readers.Add(reader);
            var booktitle = new BookTitle() { Author = "Author", PublicationYear = 2010, Title = "Test Title" };
            context.BookTitles.Add(booktitle);
            var bookCopy = new BookCopy() { BookTitle = booktitle, Status = "Free" };
            context.BookCopys.Add(bookCopy);
            var bookRental = new BookRental(){BookCopy = bookCopy,Reader = reader,RentDate = DateTime.Now};
            context.BookRentals.Add(bookRental);
            context.SaveChanges();

            //Act
            var bookRentals = await service.GetAll();

            //Assert
            Assert.Equal(1,bookRentals.Count);

            //CleanUp
            context.BookRentals.Remove(bookRental);
            context.Readers.Remove(reader);
            context.BookCopys.Remove(bookCopy);
            context.BookTitles.Remove(booktitle); 
            context.SaveChanges();
        }

        [Fact]
        public async Task AddRentalTest()
        {
            //Arrange
            var reader = new Reader() { Account = 10, LastName = "Test Lastname", Name = "Test name" };
            context.Readers.Add(reader);
            var booktitle = new BookTitle() { Author = "Author", PublicationYear = 2010, Title = "Test Title" };
            context.BookTitles.Add(booktitle);
            var bookCopy = new BookCopy() { BookTitle = booktitle, Status = "Free" };
            context.BookCopys.Add(bookCopy);
            context.SaveChanges();

            var addBook = new AddBookRentalDto("Author","Test Title",reader.Id);

            //Act
            var bookRental = await service.AddRental(addBook);

            //Assert
            Assert.Equal("Author",bookRental.BookCopy.BookTitle.Author);
            Assert.Equal("Test name",bookRental.Reader.Name);

            //CleanUp
            context.BookRentals.Remove(bookRental);
            context.Readers.Remove(reader);
            context.BookCopys.Remove(bookCopy);
            context.BookTitles.Remove(booktitle);
            context.SaveChanges();
        }

        [Fact]
        public async Task ReturnByBookAndUserTest()
        {
            //Arrange
            var reader = new Reader() { Account = 10, LastName = "Test Lastname", Name = "Test name" };
            context.Readers.Add(reader);
            var booktitle = new BookTitle() { Author = "Author", PublicationYear = 2010, Title = "Test Title" };
            context.BookTitles.Add(booktitle);
            var bookCopy = new BookCopy() { BookTitle = booktitle, Status = "Free" };
            context.BookCopys.Add(bookCopy);
            context.SaveChanges();

            var addBook = new AddBookRentalDto("Author", "Test Title", reader.Id);
            var bookRental = await service.AddRental(addBook);
            
            //Act
            var returnBook = await service.ReturnByBookAndUser(bookCopy.Id, reader.Id);

            //Asert
            Assert.Equal(DateTime.Now.Date,returnBook.ReturnDate.Date);

            //CleanUp
            context.BookRentals.Remove(bookRental);
            context.Readers.Remove(reader);
            context.BookCopys.Remove(bookCopy);
            context.BookTitles.Remove(booktitle);
            context.SaveChanges();
        }
    }
}
