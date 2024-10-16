using Microsoft.AspNetCore.Identity;
using OnlineLearningPlatform.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLearningPlatform.DAL.Data
{
    public static class SeedData
    {
        public static async Task Initialize(AppDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Tracks.Count() < 4)
            {
                var tracksToAdd = new List<Track>
            {
                new Track { Name = "Web Development", Description = "Learn the fundamentals of web development, including HTML, CSS, and JavaScript." },
                new Track { Name = "Data Science", Description = "Explore the world of data science, including Python, machine learning, and data analysis." },
                new Track { Name = "Mobile Development", Description = "Create mobile apps with Android and iOS platforms using modern development tools." },
                new Track { Name = "Cybersecurity", Description = "Understand the basics of network security, cryptography, and risk management." }
            };
                context.Tracks.AddRange(tracksToAdd);
                await context.SaveChangesAsync();
            }

            if (context.Courses.Count() < 3)
            {
                var coursesToAdd = new List<Course>
            {
                new Course { Title = "HTML & CSS Basics", Description = "Start with the basics of HTML and CSS to build your first website.", TrackId = context.Tracks.First(t => t.Name == "Web Development").Id },
                new Course { Title = "JavaScript for Beginners", Description = "Learn the basics of JavaScript to make your websites interactive.", TrackId = context.Tracks.First(t => t.Name == "Web Development").Id },
                new Course { Title = "Introduction to Data Science", Description = "Understand the fundamental concepts of data science and machine learning.", TrackId = context.Tracks.First(t => t.Name == "Data Science").Id },
                new Course { Title = "Python for Data Analysis", Description = "Learn to use Python for data analysis, manipulation, and visualization.", TrackId = context.Tracks.First(t => t.Name == "Data Science").Id }
            };
                context.Courses.AddRange(coursesToAdd);
                await context.SaveChangesAsync();
            }

            await SeedRoles(userManager, roleManager);

            await SeedAdminUser(userManager, roleManager);
        }

        private static async Task SeedRoles(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var adminRole = new IdentityRole<int> { Name = "Admin" };
                await roleManager.CreateAsync(adminRole);
            }

            if (!await roleManager.RoleExistsAsync("Student"))
            {
                var studentRole = new IdentityRole<int> { Name = "Student" };
                await roleManager.CreateAsync(studentRole);
            }
        }

        private static async Task SeedAdminUser(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
        {
            var adminUserName = "admin";
            var adminUser = await userManager.FindByNameAsync(adminUserName);
            if (adminUser == null)
            {
                adminUser = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = "admin@learning.com",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(adminUser, "111z");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }

                else
                {
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine(error.Description);
                    }
                }
            }
        }
    }


}
