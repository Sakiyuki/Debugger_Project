namespace Debugger_Project.Migrations
{
    using Debugger_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Debugger_Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Debugger_Project.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            #region
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                roleManager.Create(new IdentityRole { Name = "Project Manager" });
            }

            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                roleManager.Create(new IdentityRole { Name = "Developer" });
            }

            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                roleManager.Create(new IdentityRole { Name = "Submitter" });
            }
            #endregion


            var userManager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(context));

            if (!context.Users.Any(u => u.Email == "cwellionaire@gmail.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "cwellionaire@gmail.com",
                    Email = "cwellionaire@gmail.com",
                    FirstName = "Bryant",
                    LastName = "Caldwell",
                    DisplayName = "C-Wellionaire"
                }, "Abc&123");
            }
            if (!context.Users.Any(u => u.Email == "JoeSchmo@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "JoeSchmo@Mailinator.com",
                    Email = "JoeSchmo@Mailinator.com",
                    FirstName = "Joe",
                    LastName = "Schmo",
                    DisplayName = "Twich"
                }, "Abc&123");
            }

            var userId = userManager.FindByEmail("cwellionaire@gmail.com").Id;
            userManager.AddToRole(userId, "Admin");

            userId = userManager.FindByEmail("JoeSchmo@Mailinator.com").Id;
            userManager.AddToRole(userId, "Project Manager");
        }
    }
}
