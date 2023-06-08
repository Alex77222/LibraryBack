using Library.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Library.Data.Initializers;

public static class RolesInitializer
{
    public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
    {
        if (await roleManager.FindByNameAsync(Role.Admin) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Admin));
        }

        if (await roleManager.FindByNameAsync(Role.Moderator) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Moderator));
        }

        if (await roleManager.FindByNameAsync(Role.Reader) == null)
        {
            await roleManager.CreateAsync(new IdentityRole(Role.Reader));
        }
    }
}