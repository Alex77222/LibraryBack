using AutoMapper;
using Library.Data.Entities;
using Library.Models;

namespace Library.Mapper.Profiles;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
    }
}