using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using LibraryRestApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace LibraryRestApi.Service
{
    public class ReaderDbService : IReaderRepository
    {
        private readonly AppDbContext _context;

        public ReaderDbService(AppDbContext context) => _context = context;

        public async Task<ICollection<Reader>> GetAllReaders()
        {
            return await _context.Readers.ToListAsync();
        }

        public async Task<Reader> GetReader(long id)
        {
            return await _context.Readers.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reader> AddReader(Reader reader)
        {
            await _context.Readers.AddAsync(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        public void DeleteReader(long id)
        {
            var reader = _context.Readers.FirstOrDefault(r => r.Id == id);
                _context.Readers.Remove(reader);
                _context.SaveChanges();
        }
    }
}
