using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryRestApi.Dtos
{
    public class AddBookRentalDto
    {
        public AddBookRentalDto(string author, string title, long userId)
        {
            Author = author;
            Title = title;
            UserId = userId;
        }

        public string Author { get; set; }
        public string Title { get; set; }
        public long UserId { get; set; }

    }
}
