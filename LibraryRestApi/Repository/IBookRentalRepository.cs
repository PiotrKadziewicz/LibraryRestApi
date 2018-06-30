using System.Collections.Generic;
using System.Threading.Tasks;
using LibraryRestApi.Models;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using Microsoft.IdentityModel.Logging;

namespace LibraryRestApi.Repository
{
    public interface IBookRentalRepository
    {
        Task<ICollection<BookRental>> GetAll();
        Task<BookRental> AddRental(BookRental bookRental);
        Task<BookRental> GetById(long id);
        Task<BookRental> GetByBookAndUser(long bookId, long readerId);
    }
}