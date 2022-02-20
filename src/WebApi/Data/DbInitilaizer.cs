using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Models;

namespace WebApi.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()
            ))
            {
                context.Database.EnsureCreated();
                if (context.Roles is not null)
                {
                    if (!context.Roles.Any())
                    {
                        context.Roles.AddRange(
                            new ApplicationRole { Name = "admin", NormalizedName = "ADMIN" },
                            new ApplicationRole { Name = "owner", NormalizedName = "OWNER" },
                            new ApplicationRole { Name = "user", NormalizedName = "USER" }
                        );
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
