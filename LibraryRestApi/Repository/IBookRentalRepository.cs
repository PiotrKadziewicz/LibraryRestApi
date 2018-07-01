using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRestApi.Dtos;
using LibraryRestApi.Models;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.IdentityModel.Logging;

namespace LibraryRestApi.Repository
{
    public interface IBookRentalRepository
    {
        Task<ICollection<BookRental>> GetAll();
        Task<BookRental> AddRental(AddBookRentalDto addBookRentalDto);
        Task<BookRental> ReturnByBookAndUser(long bookId, long readerId);
    }
}