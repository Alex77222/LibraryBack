using Library.Data.Entities;
using Library.Models;

namespace Library.Services.Contracts;

public interface IBookService
{
    public Task<List<BookDto>> GetBooksAsync(
        string? searchString,
        int currentPage,
        int pageSize);

    public Task<BookDto> GetBookAsync(int id);

    public Task<BookDto> AddBookAsync(BookDto book);

    public Task<BookDto> UpdateBookAsync(UpdateBookModel model);
}