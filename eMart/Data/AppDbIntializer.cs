using eMart.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using eMart.Data.Static;

namespace eMart.Data
{
    public class AppDbIntializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //owners
                //owners
                if (!context.Owners.Any())
                {
                    context.Owners.AddRange(new List<ProductOwner>()
                    {
                        new ProductOwner()
                        {
                            Name = "Apple",
                            Logo = "Assets/Apple.png"
                        },
                        new ProductOwner()
                        {
                            Name = "Samsung",
                            Logo = "Assets/Samsung.png"
                        },
                        new ProductOwner()
                        {
                            Name = "Nike",
                            Logo = "Assets/Nike.png"
                        },
                        new ProductOwner()
                        {
                            Name = "Adidas",
                            Logo = "Assets/Adidas.png"
                        },
                        new ProductOwner()
                        {
                            Name = "Tommy Hilfiger",
                            Logo = "Assets/Tommy.png"
                        },
                        new ProductOwner()
                        {
                            Name = "Aeropostale",
                            Logo = "Assets/Aeropostale.png"
                        }
                    });
                    context.SaveChanges();
                }
                //products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Name = "iPhone 13",
                            Description = "Apple's iPhone 13",
                            Price = 49999,
                            ImageURL = "Assets/iphone_13.jpeg",
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(30),
                            ProductOwnerId = 1,
                            ProductCategory = ProductCategory.MobilePhone
                        },
                        new Product()
                        {
                            Name = "Galaxy S22",
                            Description = "Samsung's Galaxy S22",
                            Price = 44999,
                            ImageURL = "Assets/s22.jpeg",
                            StartDate = DateTime.Now.AddDays(1),
                            EndDate = DateTime.Now.AddDays(30),
                            ProductOwnerId = 2,
                            ProductCategory = ProductCategory.MobilePhone
                        },
                        new Product()
                        {
                            Name = "MacBook Air",
                            Description = "Apple's MacBook Air with all new M1 chip",
                            Price = 89999,
                            ImageURL = "Assets/macbook.jpeg",
                            StartDate = DateTime.Now.AddDays(10),
                            EndDate = DateTime.Now.AddDays(60),
                            ProductOwnerId = 1,
                            ProductCategory = ProductCategory.Computer
                        }
                    });
                    context.SaveChanges();

                }
            }
        }

        public static async Task SeesUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@emart.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@emart.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
