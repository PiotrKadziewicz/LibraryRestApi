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
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryRestApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ReaderController : Controller
    {
        private readonly IReaderRepository _repo;
        private readonly IMapper _mapper;

        public ReaderController(IReaderRepository readerRepository, IMapper mapper)
        {
            _repo = readerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ICollection<ReaderDto>> GetAllReaders()
        {
            var readers = await _repo.GetAllReaders();
            return _mapper.Map<ICollection<Reader>, ICollection<ReaderDto>>(readers);
        }

        [HttpGet("{id}")]
        public async Task<ReaderDto> GetUser(int id)
        {
            var reader = await _repo.GetReader(id);
            return _mapper.Map<Reader, ReaderDto>(reader);
        }

        [HttpPost]
        public async Task<ReaderDto> CreateReader([FromBody] ReaderDto readerDto)
        {
            var reader = await _repo.AddReader(_mapper.Map<ReaderDto, Reader>(readerDto));
            return _mapper.Map<Reader, ReaderDto>(reader);
        }

        [HttpDelete("{id}")]
        public void  DeleteUser(long id)
        {
            _repo.DeleteReader(id);
        }
    }
}