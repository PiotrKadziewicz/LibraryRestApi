using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LibraryRestApi.Models
{
    public class BookCopy
    {
        public BookCopy()
        {
        }

        public BookCopy(string status, BookTitle bookTitle)
        {
            Status = status;
            BookTitle = bookTitle;
        }

        //[Key]
        public long Id { get; set; }

        //[Required]
        public string Status { get; set; }

        //public long BookTitleId { get; set; }
        //[ForeignKey("BookTitleId")]
        public BookTitle BookTitle { get; set; }
        public ICollection<BookRental> BookRentals { get; set; }
    }
}
