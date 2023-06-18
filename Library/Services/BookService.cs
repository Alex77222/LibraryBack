using AutoMapper;
using Library.Data;
using Library.Data.Entities;
using Library.Models;
using Library.Services.Contracts;

namespace Library.Services;

public class BookService : IBookService
{
    private readonly AppDbContext _app;
    private readonly IMapper _mapper;

    public BookService(AppDbContext app, IMapper mapper)
    {
        _app = app;
        _mapper = mapper;
    }

    public Task<List<BookDto>> GetBooksAsync(string? searchString, int currentPage, int pageSize)
    {
        throw new NotImplementedException();
    }

    public Task<BookDto> GetBookAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<BookDto> AddBookAsync(AddBookModel model)
    {
        if (!_app.Book.Any(x=>x.BookName == model.BookName))
        {
            throw new Exception();
        }

        var book = new Book()
        {
            BookName = model.BookName,
            Author = model.Author,
            Description = model.Description
        };

        await _app.Book.AddAsync(book);
        await _app.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);

    }

    public Task<BookDto> UpdateBookAsync(UpdateBookModel model)
    {
        throw new NotImplementedException();
    }
}