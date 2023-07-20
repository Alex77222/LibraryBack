namespace Library.Models;

public class AddBookModel
{
    public string BookName { get; set; } = string.Empty;

    public string Author { get; set; } = string.Empty;

    public string? Description { get; set; }

}