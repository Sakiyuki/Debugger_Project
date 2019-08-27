namespace Debugger_Project.Migrations
{
    using Debugger_Project.Helpers;
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

            var subId = userManager.FindByEmail("DemoSubmitter@Mailinator.com").Id;
            userManager.AddToRole(subId, "Submitter");

            var devId = userManager.FindByEmail("DemoDeveloper@Mailinator.com").Id;
            userManager.AddToRole(devId, "Developer");

           
            context.Projects.AddOrUpdate(
               p => p.Name,
                   new Project { Name = "Spock IT Blog", Description = "This is the Spock Blog project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "Spock Portfolio", Description = "This is the Portfolio project that is now out in the wild.", Created = DateTime.Now },
                   new Project { Name = "Spock BugTracker", Description = "This is the Spock BugTracker project that is now out in the wild.", Created = DateTime.Now }
               );

            context.SaveChanges();


            #region Project Assignment
            var blogProjectId = context.Projects.FirstOrDefault(p => p.Name == "Spock IT Blog").Id;
            var bugTrackerProjectId = context.Projects.FirstOrDefault(p => p.Name == "Spock BugTracker").Id;

            var projectHelper = new ProjectsHelper();

            //Assign all three users to the Blog project
            projectHelper.AddUserToProject(pmId, blogProjectId);
            projectHelper.AddUserToProject(devId, blogProjectId);
            projectHelper.AddUserToProject(subId, blogProjectId);

            //Assign all three users to the Blog project
            projectHelper.AddUserToProject(pmId, bugTrackerProjectId);
            projectHelper.AddUserToProject(devId, bugTrackerProjectId);
            projectHelper.AddUserToProject(subId, bugTrackerProjectId);
            #endregion

            #region Priority, Status & Type creation (required FK's for a Ticket)
            context.TicketPriorities.AddOrUpdate(
                t => t.Name,
                    new TicketPriority { Name = "Immediate", Description = "Highest priority level requiring immediate action" },
                    new TicketPriority { Name = "High", Description = "A high priority level requiring quick action" },
                    new TicketPriority { Name = "Medium", Description = "" },
                    new TicketPriority { Name = "Low", Description = "" },
                    new TicketPriority { Name = "None", Description = "" }
                );

            context.TicketStatuses.AddOrUpdate(
                t => t.Name,
                    new TicketStatus { Name = "New / UnAssigned", Description = "" },
                    new TicketStatus { Name = "UnAssigned", Description = "" },
                    new TicketStatus { Name = "New / Assigned", Description = "" },
                    new TicketStatus { Name = "Assigned", Description = "" },
                    new TicketStatus { Name = "In Progress", Description = "" },
                    new TicketStatus { Name = "Completed", Description = "" },
                    new TicketStatus { Name = "Archived", Description = "" }
                );

            context.TicketTypes.AddOrUpdate(
                t => t.Name,
                    new TicketType { Name = "Bug", Description = "An error has occurred that resulted in either the application crashing or the user seeing error information" },
                    new TicketType { Name = "Defect", Description = "An error has occurred that resulted in either a miscalculation or an in correct workflow" },
                    new TicketType { Name = "Feature Request", Description = "A client has called in asking for new functionality in an existing application" },
                    new TicketType { Name = "Documentation Request", Description = "A client has called in asking for new documentation to be created for the existing application" },
                    new TicketType { Name = "Training Request", Description = "A client has called in asking to schedule a training session" },
                    new TicketType { Name = "Complaint", Description = "A client has called in to make a general complaint about our application" },
                    new TicketType { Name = "Other", Description = "A call has been received that requires follow up but is outside the usual parameters for a request" }
                );

            context.SaveChanges();
            #endregion

            #region Ticket creation          
            context.Tickets.AddOrUpdate(
               p => p.Title,
                //1 unassigned Bug on the Blog project
                //1 assigned Defect on the Blog project
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #1",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = blogProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #2",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                },

                //1 unassigned Bug on the BugTracker
                //1 assigned Defect on the BugTracker
                //1 unassigned Bug on the Blog project
                //1 assigned Defect on the Blog project
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    Title = "Seeded Ticket #3",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / UnAssigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Bug").Id,
                },
                new Ticket
                {
                    ProjectId = bugTrackerProjectId,
                    OwnerUserId = subId,
                    AssignedToUserId = devId,
                    Title = "Seeded Ticket #4",
                    Description = "Testing a seeded Ticket",
                    Created = DateTime.Now,
                    TicketPriorityId = context.TicketPriorities.FirstOrDefault(t => t.Name == "Medium").Id,
                    TicketStatusId = context.TicketStatuses.FirstOrDefault(t => t.Name == "New / Assigned").Id,
                    TicketTypeId = context.TicketTypes.FirstOrDefault(t => t.Name == "Defect").Id,
                });

        }
    }
}
#endregion