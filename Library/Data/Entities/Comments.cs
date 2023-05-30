namespace Library.Data.Entities;

public class Comments
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;

    public DateTime Date { get; set; }

    public int BookId { get; set; }

    public Book? Book { get; set; }
}