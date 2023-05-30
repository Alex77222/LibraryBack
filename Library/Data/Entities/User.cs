using Microsoft.AspNetCore.Identity;

namespace Library.Data.Entities;

public class User : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public bool IsActive { get; set; }

    public ICollection<Book>? Books { get; set; }

    public Book? Book { get; set; }
}