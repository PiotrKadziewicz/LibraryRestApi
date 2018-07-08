using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using LibraryRestApi.Service;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace LibraryRestApiTestSuite
{
    public class ReaderServiceTest
    {
        private readonly AppDbContext context;
        private readonly ReaderDbService service;
        public ReaderServiceTest()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            this.context = new AppDbContext(options);
            this.service = new ReaderDbService(context);
        }

        [Fact]
        public async Task GetAllReadersTest()
        {
            //Arrange
            var reader = new Reader
            {
                Name = "Test1",
                LastName = "Test1",
                Account = 10
            };

            context.Readers.Add(reader);
            context.SaveChanges();

            //Act
            var readers = await service.GetAllReaders();

            //Assert
            Assert.Equal(1, readers.Count);

            //CleanUp
            context.Readers.RemoveRange(readers);
            context.SaveChanges();
        }

        [Fact]
        public async Task GetReaderByIdTest()
        {
            //Arrange
            var reader = new Reader
            {
                Name = "Test1",
                LastName = "Test1",
                Account = 10
            };

            //Act
            context.Readers.Add(reader);
            context.SaveChanges();
            var getReader = await service.GetReader(reader.Id);

            //Assert
            Assert.Equal(reader, getReader);

            //CleanUp
            context.Readers.Remove(getReader);
            context.SaveChanges();
        }

        [Fact]

        public async Task AddReaderTest()
        {
            //Arrange
            var reader = new Reader
            {
                Name = "Test1",
                LastName = "Test1",
                Account = 10
            };

            //Act
            await service.AddReader(reader);

            //Assert
            Assert.Equal(1,context.Readers.Count());

            //CleanUp
            context.Readers.Remove(reader);
            context.SaveChanges();
        }

        [Fact]
        public void DeleteReaderTest()
        {
            //Arrange
            var reader = new Reader
            {
                Name = "Test1",
                LastName = "Test1",
                Account = 10
            };

            context.Readers.Add(reader);
            context.SaveChanges();

            //Act
            service.DeleteReader(reader.Id);

            //Assert
            Assert.Equal(0,context.Readers.Count());
        }
    }
}
