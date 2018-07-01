using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Models
{
    public class Reader
    {
        public Reader() { }

        //[Key]
        public long Id { get; set; }
        //[Required]
        public string Name { get; set; }
        //[Required]
        public string LastName { get; set; }
        //[Required]
        public decimal Account { get; set; }
        public ICollection<BookRental> BookRentals { get; set; }
    }
}
