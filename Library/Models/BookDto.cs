using System.ComponentModel;

namespace Library.Models;

public class BookDto
{
    public int  Id { get; set; }

    public string Author { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Likes { get; set; }
}