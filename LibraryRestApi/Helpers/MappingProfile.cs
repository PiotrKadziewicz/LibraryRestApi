using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LibraryRestApi.Dtos;
using LibraryRestApi.Models;

namespace LibraryRestApi.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reader, ReaderDto>();
            CreateMap<ReaderDto, Reader>();
            CreateMap<BookTitle, BookTitleDto>();
            CreateMap<BookTitleDto, BookTitle>();
            CreateMap<BookCopy, BookCopyDto>()
                .ForMember(dest => dest.BookTitleDto, opts => opts.MapFrom(src => src.BookTitle));
            CreateMap<BookRental, BookRentalDto>()
                .ForMember(dest => dest.ReaderDto, opts =>opts.MapFrom((src=>src.Reader)))
                .ForMember(dest=>dest.BookCopyDto, opts=>opts.MapFrom((src=>src.BookCopy)));
        }
    }
}
