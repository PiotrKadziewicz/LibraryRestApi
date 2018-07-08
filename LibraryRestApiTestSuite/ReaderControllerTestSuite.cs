using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibraryRestApi;
using LibraryRestApi.Controllers;
using LibraryRestApi.Dtos;
using LibraryRestApi.Helpers;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using LibraryRestApi.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json;
using Xunit;

namespace LibraryRestApiTestSuite
{
    public class ReaderControllerTestSuite
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly TestServer server;
        private readonly HttpClient client;


        public ReaderControllerTestSuite()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Library;Trusted_Connection=True;MultipleActiveResultSets=true").Options;
            this.context = new AppDbContext(options);
            var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            mapper = new Mapper(config);
            server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            client = server.CreateClient();
        }

        [Fact]
        public async Task GetAllReaders()
        {
            //Arrange
            ICollection<Reader> readers = new List<Reader>(){new Reader()
            {
                Account = 10, Id = 1L, Name = "Test name", LastName = "Test lastname"
            }};

            var serviceMock = new Mock<IReaderRepository>();

            serviceMock.Setup(m => m.GetAllReaders()).Returns(Task.FromResult(readers));
            var controller = new ReaderController(serviceMock.Object, mapper);

            //Act
            var result = await controller.GetAllReaders();

            //Assert
            Assert.Equal(1, result.Count);
            Assert.Equal("Test name", result.First().Name);
        }

        [Fact]
        public async Task GetReadersTest()
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
            var response = await client.GetAsync("/api/Reader");
            var responsJson = await response.Content.ReadAsStringAsync();

            var readers = JsonConvert.DeserializeObject<ICollection<ReaderDto>>(responsJson);

            Assert.Equal("Test1",readers.First().Name);

            //CleanUp
            context.Readers.RemoveRange(reader);
            context.SaveChanges();
        }

    }
}
