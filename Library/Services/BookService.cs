using AutoMapper;
using Library.Data;
using Library.Data.Entities;
using Library.Exceptions;
using Library.Models;
using Library.Services.Contracts;
using Microsoft.EntityFrameworkCore;

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

    public async Task<Page<BookDto>> GetBooksAsync(string? searchString, int currentPage, int pageSize)
    {
        var books = await SearchBookAsync(searchString);

        return new Page<BookDto>()
        {
            CurrentPage = currentPage,
            PageSize = pageSize,
            TotalItems = books.Count,
            Content = books.Skip((currentPage-1)*pageSize).Take(pageSize).ToList()
        };
    }

    public async Task<BookDto> GetBookAsync(int id)
    {
        var book = await _app.Book.FirstOrDefaultAsync(x => x.Id == id);
        if (book == null) throw new BookException();
        return _mapper.Map<BookDto>(book);
    }

    public async Task<BookDto> AddBookAsync(AddBookModel model)
    {
        if (_app.Book.Any(x => x.BookName == model.BookName))
        {
            throw new BookException("This book is already!");
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

    public async Task<BookDto> UpdateBookAsync(UpdateBookModel model)
    {
        var book = await _app.Book.FirstOrDefaultAsync(x => x.Id == model.Id);
        if (book == null) throw new BookException();
        if (!string.IsNullOrEmpty(model.Description))
        {
            book.Description = model.Description;
        }

        if (!string.IsNullOrEmpty(model.Author))
        {
            book.Author = model.Author;
        }

        if (!string.IsNullOrEmpty(model.Description))
        {
            book.BookName = model.NameBook;
        }

        _app.Book.Update(book);
        await _app.SaveChangesAsync();
        return _mapper.Map<BookDto>(book);
    }

    public async Task<string> DeleteBookAsync(int id)
    {
        var book = await _app.Book.FirstOrDefaultAsync(x=>x.Id==id);
        if (book == null) throw new BookException();
         _app.Book.Remove(book);
         await _app.SaveChangesAsync();

         return "Book remove";
    }

    private async Task<List<BookDto>> SearchBookAsync(string? searchString)
    {
        var books = new List<Book>();

        var search = !string.IsNullOrEmpty(searchString) ? searchString.Replace(" ", "") : string.Empty;

        var booksByName = await _app.Book.Where(x => x.BookName != null && x.BookName.Contains(search)).ToListAsync();
        books.AddRange(booksByName);

        var booksByAuthor = await _app.Book.Where(x => x.Author.Contains(search)).ToListAsync();
        books.AddRange(booksByAuthor.Except(booksByName));

        return _mapper.Map<List<BookDto>>(books);
    }
}