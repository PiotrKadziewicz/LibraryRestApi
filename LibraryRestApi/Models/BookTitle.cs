using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Models
{
    public class BookTitle
    {

        [Key]
        public long Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int PublicationYear { get; set; }
        public virtual ICollection<BookCopy> BookCopies { get; set; }
    }
}
