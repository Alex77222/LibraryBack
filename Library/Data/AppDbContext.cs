using Library.Data.Configurations;
using Library.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Library.Data;

public class AppDbContext:IdentityDbContext<User>
{
    public DbSet<Book> Book { get; set; }
    public DbSet<Comments> Comments { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        CommentConfiguration.Create(builder);
    }
}