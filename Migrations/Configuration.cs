namespace Debugger_Project.Migrations
{
    using Debugger_Project.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Configuration;

    internal sealed class Configuration : DbMigrationsConfiguration<Debugger_Project.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
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

            if (!context.Users.Any(u => u.Email == "TupacShakur@Mailinator.com"))
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


            //Introduction to the Demo Users....
            if (!context.Users.Any(u => u.Email == "DemoAdmin@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoAdmin@Mailinator.com",
                    Email = "DemoAdmin@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Admin",
                    DisplayName = "Demo Admin"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoProjectManager@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoProjectManager@Mailinator.com",
                    Email = "DemoProjectManager@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Project Manager",
                    DisplayName = "The PM"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoDeveloper@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoDeveloper@Mailinator.com",
                    Email = "DemoDeveloper@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Developer",
                    DisplayName = "The Dev"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }

            if (!context.Users.Any(u => u.Email == "DemoSubmitter@Mailinator.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    UserName = "DemoSubmitter@Mailinator.com",
                    Email = "DemoSubmitter@Mailinator.com",
                    FirstName = "Demo",
                    LastName = "Submitter",
                    DisplayName = "The Sub"
                }, WebConfigurationManager.AppSettings["DemoUserPassword"]);
            }


            var adminId = userManager.FindByEmail("cwellionaire@gmail.com").Id;
            userManager.AddToRole(adminId, "Admin");

            adminId = userManager.FindByEmail("DemoAdmin@Mailinator.com").Id;
            userManager.AddToRole(adminId, "Admin");

            var pmId = userManager.FindByEmail("JoeSchmo@Mailinator.com").Id;
            userManager.AddToRole(pmId, "Project Manager");

            pmId = userManager.FindByEmail("DemoProjectManager@Mailinator.com").Id;
            userManager.AddToRole(pmId, "Project Manager");

            var subId = userManager.FindByEmail("Submitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var devId = userManager.FindByEmail("Developer@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

            devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");
        }
    }
}
