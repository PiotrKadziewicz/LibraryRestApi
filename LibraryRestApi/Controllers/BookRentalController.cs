using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryRestApi.Dtos;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryRestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/BookRental")]
    public class BookRentalController : Controller
    {
        private readonly IBookRentalRepository _repo;
        private readonly IMapper _mapper;

        public BookRentalController(IBookRentalRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<BookRentalDto>> GetAll() => _mapper.Map<ICollection<BookRental>, ICollection<BookRentalDto>>(await _repo.GetAll());

        [HttpPost]
        public async Task<BookRentalDto> BorrowBook([FromQuery]string author, [FromQuery] string title, [FromQuery] long userId) => _mapper.Map<BookRental, BookRentalDto>(await _repo.AddRental(new AddBookRentalDto(author, title, userId)));

        [HttpPost("json")]
        public async Task<BookRentalDto> BorrowBookJson([FromBody]AddBookRentalDto addBookRentalDto) => _mapper.Map<BookRental, BookRentalDto>(await _repo.AddRental(addBookRentalDto));
    }
}
