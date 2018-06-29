using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Models
{
    public class BookCopy
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime SignUpDate { get; set; }

        public ISet<BookRental> bookRentals { get; set; }
    }
}
