using CoursesSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoursesSystem.Web.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRolesMigrations(this IApplicationBuilder app)
        {

            using (var servicescope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = servicescope.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var userManager = servicescope.ServiceProvider.GetService<UserManager<ApplicationUser>>();


                Task.Run(async () =>
                {
                    string[] roleNames = { RolesConstants.Administratior, RolesConstants.Trainer };

                    foreach (var roleName in roleNames)
                    {
                        var result = await roleManager.RoleExistsAsync(roleName);
                        if (!result)
                        {
                            await roleManager.CreateAsync(new IdentityRole
                            {
                                Name = roleName
                            });
                        }

                    }



                    var admin = await userManager.FindByNameAsync(RolesConstants.Administratior);

                    if (admin == null)
                    {
                        admin = new ApplicationUser
                        {
                            UserName = "admin",
                            Email = "admin@admin"
                        };

                        await userManager.CreateAsync(admin, "admin");
                        await userManager.AddToRoleAsync(admin, RolesConstants.Administratior);
                    }


                }
                    ).Wait();


            }

            return app;
        }
    }
}

