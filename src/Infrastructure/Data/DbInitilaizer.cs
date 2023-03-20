using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Infrastructure;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void Seed(IServiceProvider serviceProvider)
        {
            var service = serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>();
            using (var context = new ApplicationDbContext(service))
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
