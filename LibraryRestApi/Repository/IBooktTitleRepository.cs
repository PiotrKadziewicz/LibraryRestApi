using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRestApi.Models;

namespace LibraryRestApi.Repository
{
    public interface IBooktTitleRepository
    {
        Task<ICollection<BookTitle>> GetAll();
        Task<BookTitle> GetById(long id);
        Task<BookTitle> AddBookTitle(BookTitle bookTitle);
        void DeleteReader(long id);
        Task<BookTitle> GetBookTitleByAuthorAndTitle(string title, string author);
    }
}