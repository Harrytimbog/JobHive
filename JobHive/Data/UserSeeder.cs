using Microsoft.AspNetCore.Identity;

namespace JobHive.Data
{
    public class UserSeeder
    {
        public static async Task SeeedUserAsyn(IServiceProvider serviceProvider)
        {
            // Get a new instance of ApplicationDbContext for seeding purposes
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

            userManager.

        }
    }
}
