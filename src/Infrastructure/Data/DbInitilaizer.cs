using ApplicationCore.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task SeedAsync(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
            ))
            {
                context.Database.EnsureCreated();
                var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
                if (roleManager is not null)
                {
                    var roles = new string[] { "admin", "owner", "user" };
                    foreach (string role in roles)
                    {
                        var roleExist = await roleManager.RoleExistsAsync(role);
                        if (!roleExist)
                        {
                            await roleManager.CreateAsync(new ApplicationRole { Name = role });
                        }
                    }
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                if (userManager is not null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = "exampleUser",
                        Email = "example@example.com",
                    };
                    var user = await userManager.FindByEmailAsync(newUser.Email);
                    if (user is null)
                    {
                        await userManager.CreateAsync(newUser);
                        await userManager.AddToRoleAsync(newUser, "admin");
                    }
                }
            }
        }
    }
}
