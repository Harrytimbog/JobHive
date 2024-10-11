using JobHive.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

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


            await CreateUserWithRole(userManager, "admin@jobhive.com", "Admin123!", Roles.Admin);
            await CreateUserWithRole(userManager, "jobseeker@jobhive.com", "JobSeeker123!", Roles.JobSeeker);
            await CreateUserWithRole(userManager, "employer@jobhive.com", "Employer123!", Roles.Employer);

        }


        private static async Task CreateUserWithRole(UserManager<IdentityUser> userManager, string email, string password, string role)
        {
            if (await userManager.FindByEmailAsync(email) == null)
            {
                var user = new IdentityUser
                {
                    Email = email,
                    EmailConfirmed = true,
                    UserName = email
                };

                var result = await userManager.CreateAsync(user, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
                else
                {
                    throw new Exception($"Failed creating user with email {user.Email}. Errors: {string.Join(",", result.Errors)}");
                }
            }
        }
    }
}
