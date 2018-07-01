using LibraryRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Dtos
{
    public class BookRentalDto
    {

        public long Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public BookCopyDto BookCopyDto { get; set; }
        public ReaderDto ReaderDto{ get; set; }
    }
}
