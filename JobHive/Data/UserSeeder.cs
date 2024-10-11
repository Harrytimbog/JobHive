using JobHive.Constants;
using Microsoft.AspNetCore.Identity;

namespace JobHive.Data
{
    public class UserSeeder
    {
        public static async Task SeedUserAsync(IServiceProvider serviceProvider)
        {
            // Get a new instance of ApplicationDbContext for seeding purposes
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            if (await userManager.FindByEmailAsync("admin@jobhive.com") == null)
            {
                var user = new IdentityUser
                {
                    Email = "admin@jobhive.com",
                    EmailConfirmed = true,
                    UserName = "admin@jobhive"
                };

                await userManager.CreateAsync(user, "Admin@123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, Roles.Admin);
                }
            }
    }
}
