using Microsoft.AspNetCore.Identity;
using EjadaTraineesManagementSystem.Models;
using EjadaTraineesManagementSystem.Data;

namespace EjadaTraineesManagementSystem.Services
{
    public class SeedService
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<SeedService>>();

            try
            {
                //Ensure the databasee is ready
                logger.LogInformation("Ensuring the database is created.");
                await context.Database.EnsureCreatedAsync();

                //add roles
                logger.LogInformation("Seeding Roles .");
                await AddRoleAsync(roleManager, "Admin");
                await AddRoleAsync(roleManager, "Supervisor");

                //add Admin user
                logger.LogInformation("Seeding Admin user .");
                var adminEmail = "Admin1@ejada.com";
                if (await userManager.FindByEmailAsync(adminEmail) == null)
                {
                    var adminUser = new Users
                    {
                        fullName = "System Admin",
                        UserName = adminEmail,
                        NormalizedUserName = adminEmail.ToUpper(),
                        Email = adminEmail,
                        NormalizedEmail = adminEmail.ToUpper(),
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var result = await userManager.CreateAsync(adminUser, "Admin@123");
                    if (result.Succeeded) {
                        logger.LogInformation("Assigning Admin role to Admin user.");
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {
                        logger.LogError("Failed to create Admin user : {Errors}", string.Join(", ", result.Errors.Select(e => e.Description)));

                    }
                }
            }
            catch (Exception ex) {
                logger.LogError(ex, "Error occurred while seeding the database");
            }
        }

        private static async Task AddRoleAsync(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                var result = await roleManager.CreateAsync(new IdentityRole(roleName));
                if (!result.Succeeded) {
                    throw new Exception($"Failed to create role '{roleName}' : {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }
            }
        }
    }
}
