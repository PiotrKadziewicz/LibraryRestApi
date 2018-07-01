using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Models
{
    public class BookRental
    {
        public BookRental()
        {
        }

        public BookRental(DateTime rentDate, BookCopy bookCopy, Reader reader)
        {
            RentDate = rentDate;
            BookCopy = bookCopy;
            Reader = reader;
        }

        [Key]
        public long Id { get; set; }
        [Required]
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }

        [Required]
        public long? BookCopyId { get; set; }
        [ForeignKey("BookCopyId")]
        public virtual BookCopy BookCopy { get; set; }

        [Required]
        public long ReaderId { get; set; }
        [ForeignKey("ReaderId")]
        public Reader Reader { get; set; }
    }
}
