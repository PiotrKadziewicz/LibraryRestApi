using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryRestApi.Dtos;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using LibraryRestApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryRestApi.Controllers
{
    [Route("api/[controller]")]    [Produces("application/json")]

    public class BookTitleController : Controller
    {
        private readonly IBooktTitleRepository _repo;
        private readonly IBookCopyRepository _copyRepo;
        private readonly IMapper _mapper;

        public BookTitleController(IBooktTitleRepository repo, IBookCopyRepository copyrepo, IMapper mapper)
        {
            _repo = repo;
            _copyRepo = copyrepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<BookTitleDto>> GetAllTitles()
        {
            var bookTitles = _mapper.Map<ICollection<BookTitle>, ICollection<BookTitleDto>>(await _repo.GetAll());
            bookTitles.ToList().ForEach(b => b.Copies = _copyRepo.CountByStatusAndTitle("Free", b.Id).Result);
            return bookTitles;
        }

        [HttpGet("{id}")]
        public async Task<BookTitleDto> GetTitleById(long id)
        {
            var bookTitleDto = _mapper.Map<BookTitle, BookTitleDto>(await _repo.GetById(id));
            bookTitleDto.Copies = _copyRepo.CountByStatusAndTitle("Free", bookTitleDto.Id).Result;
            return _mapper.Map<BookTitle, BookTitleDto>(await _repo.GetById(id));
        }

        [HttpPost]
        public async Task<BookTitleDto> AddBookTitle([FromBody] BookTitleDto bookTitleDto)
        {
            var title = _repo.GetBookTitleByAuthorAndTitle(bookTitleDto.Author, bookTitleDto.Title).Result;
            if (title != null)
            {
                await _copyRepo.AddBook(new BookCopy("Free", title));
                return _mapper.Map<BookTitle, BookTitleDto>(title);
            }
            else
            {
                var bookTitle = _mapper.Map<BookTitleDto, BookTitle>(bookTitleDto);
                return _mapper.Map<BookTitle, BookTitleDto>(await _repo.AddBookTitle(bookTitle));
            }
        }

        [HttpDelete("{id}")]
        public void DeleteTitle(long id)
        {
            _repo.DeleteReader(id);
        }
    }
}