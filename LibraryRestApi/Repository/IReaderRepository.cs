using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRestApi.Models;

namespace LibraryRestApi.Repository
{
    public interface IReaderRepository
    {
        Task<ICollection<Reader>> GetAllReaders();
        Task<Reader> GetReader(long id);
        Task<Reader> AddReader(Reader reader);
        void DeleteReader(long id);
    }
}