using Entities.Helpers;
using Entities.Models.Enums;
using Entities.Models.Identities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.DbContext
{
    public static class DbInitializer
    {
        public static async Task SeedRoleData(this IHost host)
        {
            var serviceProvider = host.Services.CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();

            var rolesEnumList = EnumExtension.GetEnumResults<ERole>();
            if (rolesEnumList.Any())
            {
                foreach (var item in rolesEnumList)
                {
                    var roleRecord = context.Roles.Where(x => x.Name.Equals(item.Name));
                    if (roleRecord.FirstOrDefault()?.Name == null)
                    {
                        Role role = new()
                        {
                            ConcurrencyStamp = Guid.NewGuid().ToString(),
                            Name = item.Name,
                        };
                        await roleManager.CreateAsync(role);
                    }
                }

            }
        }

        public static async Task SeedUserData(this IHost host)
        {
            var serviceProvider = host.Services.CreateScope().ServiceProvider;
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            await context.Database.EnsureCreatedAsync();

            var query = context.Set<User>().AsQueryable();
            var email = "admin@admin.com";

            var getUser = await query.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserName.Equals(email));

            if (getUser == null)
            {

                var user = new User
                {
                    Id = Guid.NewGuid(),
                    FirstName = "admin",
                    LastName = "admin",
                    Email = "admin@admin.com",
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    EmailConfirmed = true,
                    TwoFactorEnabled = false,
                    PhoneNumberConfirmed = false,
                    LockoutEnabled = false,
                    UserName = "admin@admin.com",
                    PhoneNumber = "07036000000",
                };

                var role = ERole.Admin.ToString();
                await userManager.CreateAsync(user, "Admin123@");
                if (!(await userManager.IsInRoleAsync(user, role)))
                {
                    var newUser = await query.IgnoreQueryFilters().FirstOrDefaultAsync(x => x.UserName.Equals(email));
                    await userManager.AddToRoleAsync(newUser, role);
                }
            }
        }

    }
}
