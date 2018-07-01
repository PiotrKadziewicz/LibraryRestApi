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
    
    public class BookCopyController : Controller
    {
        private readonly IBookCopyRepository _repo;
        private readonly IBooktTitleRepository _titleRepo;
        private readonly IMapper _mapper;

        public BookCopyController(IBookCopyRepository repo, IBooktTitleRepository titleRepo, IMapper mapper)
        {
            _repo = repo;
            _titleRepo = titleRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<ICollection<BookCopyDto>> GetAllBookCopies() => _mapper.Map<ICollection<BookCopy>, ICollection<BookCopyDto>>(await _repo.GetAllBooks());


        [HttpGet]
        [Route("api/[controller]/Get")]
        public async Task<long> GetByStatusAndAuthorAndTitle([FromQuery] string status, [FromQuery] string author, [FromQuery] string title)
        {
            var bookTitle = await _titleRepo.GetBookTitleByAuthorAndTitle(author, title);
            if (bookTitle != null)
            {
                return await _repo.CountByStatusAndTitle("Free", bookTitle.Id);
            }
            return 0;
        }

        [HttpGet]
        [Route("api/[controller]/GetByStatus")]
        public async Task<ICollection<BookCopy>> GetFreeCopies([FromQuery] string status, [FromQuery] long id) => await _repo.GetAllByStatusAndTitle(status, id);

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public void Delete(long id) => _repo.DeleteBookCopy(id);
    }
}
