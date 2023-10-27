using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PersonalCollectionManagement.Data.Contexts;

namespace IdentityMS.Data
{
    public static class SeedData
    {
        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var services = scope.ServiceProvider;

            var context = services.GetService<ApplicationDbContext>();

            context.Database.Migrate();

            var roleContext = scope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            await AddRolesAsync(roleContext);

            return app;
        }

        private static async Task AddRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            if (await roleManager.FindByNameAsync("Admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            if (await roleManager.FindByNameAsync("User") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}
