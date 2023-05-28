using Library.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Initializers;

public static class RolesInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(Roles.Admin) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        }

        if (await roleManager.FindByNameAsync(Roles.Moderator) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Moderator));
        }

        if (await roleManager.FindByNameAsync(Roles.Reader) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Reader));
        }
    }
}