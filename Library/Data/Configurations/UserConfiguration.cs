using Library.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Configurations;

public static class UserConfiguration
{
    public static void Create(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.Books);
    }
}