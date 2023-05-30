using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Configurations;

public static class CommentConfiguration
{
    public static void Create(ModelBuilder builder)
    {
        builder.Entity<Comments>()
            .HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId);
    }
}