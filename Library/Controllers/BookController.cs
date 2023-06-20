﻿using Library.Models;
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
    [Route("getBook")]
    public async Task<IActionResult> GetBookAsync(int id)
    {
        var result = await _bookService.GetBookAsync(id);
        return Ok(result);
    }
}