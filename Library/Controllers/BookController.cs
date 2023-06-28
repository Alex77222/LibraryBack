using Library.Models;
using Library.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpPost]
    [Route("addBook")]
    public async Task<IActionResult> AddBookAsync(AddBookModel model)
    {
        var result = await _bookService.AddBookAsync(model);
        return Ok(result);
    }

    [HttpGet]
    [Route("getBooks")]
    public async Task<IActionResult> GetBooksAsync(string? searchString, int? currentPage = 1, int? pageSize = 10)
    {
        return Ok(await _bookService.GetBooksAsync(searchString,currentPage!.Value,pageSize!.Value));
    }

    [HttpGet]
    [Route("getBook")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        var result = await _bookService.GetBookAsync(id);
        return Ok(result);
    }

    [HttpPut]
    [Route("updateBook")]
    public async Task<IActionResult> UpdateBookAsync(UpdateBookModel model)
    {
        return Ok(await _bookService.UpdateBookAsync(model));
    }

    [HttpDelete]
    [Route("deleteBook")]
    public async Task<IActionResult> DeleteBookAsync(int id)
    {
        return Ok(await _bookService.DeleteBookAsync(id));
    }
}