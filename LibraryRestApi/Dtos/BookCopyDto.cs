using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using LibraryRestApi.Models;

namespace LibraryRestApi.Dtos
{
    public class BookCopyDto
    {
        public long Id { get; set; }
        public string Status { get; set; }
        public BookTitleDto BookTitleDto { get; set; }
    }
}
