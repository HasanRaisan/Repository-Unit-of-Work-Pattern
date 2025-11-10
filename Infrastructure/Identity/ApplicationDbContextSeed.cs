using Infrastructure.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class ApplicationDbContextSeed
    {

        public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRoleEntity>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUserEntity>>();

            string[] RoleNames = { "Admin", "Teacher", "Student", "User" };

            foreach (var RoleName in RoleNames)
            {
                if (!await RoleManager.RoleExistsAsync(RoleName))
                {
                    await RoleManager.CreateAsync(new ApplicationRoleEntity { Name = RoleName, Description = $"{RoleName} role" });
                }
            }

            // Create default admin user
            string AdminEmail = "admin@app.com";
            var AdminUser = await UserManager.FindByEmailAsync(AdminEmail);

            if (AdminUser == null)
            {
                var user = new ApplicationUserEntity
                {
                    UserName = "admin",
                    Email = AdminEmail,
                    FullName = "System Administrator",
                    EmailConfirmed = true
                };

                var result = await UserManager.CreateAsync(user, "Admin@123");

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
