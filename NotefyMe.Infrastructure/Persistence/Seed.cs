using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NotefyMe.Domain.Entities;
using NotefyMe.Domain.Enums;
using NotefyMe.Domain.Users;
using NotefyMe.Infrastructure.Data;

namespace NotefyMe.Infrastructure.Persistence;

public class Seed
{
    public static void SeedData(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider
            .GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {

            if (!context.Notes.Any())
            {

                context.Notes.AddRange(new List<Note>()
                {

                    new Note()
                    {
                        Title = "Note 1",
                        Description = "This is the description of the first note",
                        DateCreated = DateTime.Parse("12/25/2023 06:25:30"),
                        DateUpdated = null,
                        NoteCategory = NoteCategory.Ordinary,
                        NoteColor = NoteColor.Green
                    },

                    new Note()
                    {
                        Title = "Note 2",
                        Description = "This is the description of the second note",
                        DateCreated = DateTime.Parse("01/15/2023 11:00:25"),
                        DateUpdated = null,
                        NoteCategory = NoteCategory.Urgent,
                        NoteColor = NoteColor.Purple
                    },

                    new Note()
                    {
                        Title = "Note 3",
                        Description = "This is the description of the third note",
                        DateCreated = DateTime.Parse("05/04/2023 15:40:22"),
                        DateUpdated = null,
                        NoteCategory = NoteCategory.Important,
                        NoteColor = NoteColor.Blue
                    },

                    new Note()
                    {
                        Title = "Note 4",
                        Description = "This is the description of the fourth note",
                        DateCreated = DateTime.Now,
                        DateUpdated = null,
                        NoteCategory = NoteCategory.Temporary,
                        NoteColor = NoteColor.Gray
                    }

                });

                context.SaveChanges();
            }
        }
    }

    public static async Task SeedUsersAndRolesAsync(IServiceProvider serviceProvider)
    {
        using (var context = new ApplicationDbContext(serviceProvider
            .GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
        {
            //Roles
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            if (!await roleManager.RoleExistsAsync(UserRoles.User))
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

            //Users
            var userManager = serviceProvider.GetRequiredService<UserManager<WebUser>>();

            var adminUserEmail = "admin@admin.com";
            var adminUser = await userManager.FindByEmailAsync(adminUserEmail);

            if (adminUser == null)
            {
                var newAdminUser = new WebUser()
                {
                    Name = "Application Admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    BirthDate = DateTime.Parse("02/12/2003"),
                    Email = adminUserEmail,
                    EmailConfirmed = true,
                };

                await userManager.CreateAsync(newAdminUser, "Admin@123");
                await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
            }

            var appUserEmail = "user@mail.com";
            var appUser = await userManager.FindByEmailAsync(appUserEmail);
            
            if (appUser == null)
            {
                var newAppUser = new WebUser()
                {
                    Name = "Application User",
                    UserName = "user",
                    NormalizedUserName = "USER",
                    BirthDate = DateTime.Parse("02/12/2003"),
                    Email = appUserEmail,
                    EmailConfirmed = true
                };
            
                await userManager.CreateAsync(newAppUser, "Password1!");
                await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
            }
        }
    }
}