using JobHive.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using JobHive.Data;

namespace JobHive.Data
{
    public class RolesSeeder
    {
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            // Get a new instance of ApplicationDbContext for seeding purposes
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(Roles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(Roles.JobSeeker))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.JobSeeker));
            }

            if (!await roleManager.RoleExistsAsync(Roles.Employer))
            {
                await roleManager.CreateAsync(new IdentityRole(Roles.Employer));
            }
        }
    }
}
