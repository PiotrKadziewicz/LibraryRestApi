using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace LibraryRestApi.Repository
{
    public interface IBookCopyRepository
    {
        Task<ICollection<BookCopy>> GetAllBooks();
        Task<BookCopy> GetBookCopy(long id);
        Task<BookCopy> AddBook(BookCopy bookCopy);
        Task<int> CountByStatusAndTitle(string status, long id);
        Task<ICollection<BookCopy>> GetAllByStatusAndTitle(string status, long id);
        void DeleteBookCopy(long id);
    }
}