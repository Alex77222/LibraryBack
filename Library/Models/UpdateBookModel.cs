namespace Library.Models;

public class UpdateBookModel
{
    public int Id { get; set; }

    public string? NameBook { get; set; } = string.Empty;

    public string? Description { get; set; }

    public string? Author { get; set; }
}