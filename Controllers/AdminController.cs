using Debugger_Project.Helpers;
using Debugger_Project.Models;
using Debugger_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugger_Project.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private UserRolesHelper roleHelper = new UserRolesHelper();
        private ProjectsHelper projectHelper = new ProjectsHelper();

        // GET: Admin
        public ActionResult UserIndex()
        {
            var roles = db.Roles.ToList();
            var projects = db.Projects;
            var users = db.Users.Select(u => new MemberViewModel //This line goes into the db, search for the Users table,
            //Selects the properties in the database listed below from the UserProfile View Model and converts it to a list.
            {
                Id = u.Id,
                DisplayName = u.LastName + "," + u.FirstName,
                //LastName = u.LastName,
                //DisplayName = u.DisplayName,
                AvatarUrl = u.AvatarUrl,
                Email = u.Email
            }).ToList();

            foreach(var user in users)
            {
                user.CurrentRole = new SelectList(roles, "Name", "Name", roleHelper.ListUserRoles(user.Id).FirstOrDefault());
                user.CurrentProjects = new MultiSelectList(projects, "Id", "Name", projectHelper.ListUserProjects(user.Id).Select(p => p.Id));
            }

            return View(users);
        }
        public ActionResult ManageUserRoles(string userId)
        {
            //How do I load up a DropDownList with Role information??
            //new SelectList("The list of data pushed int the control",
            //"The column that will be used to communicate my selectio(s) to the post", 
            //"The column thtat we show the user inside the control", 
            //"If they already occupy a role...show this instead of nothing)
            var currentRole = roleHelper.ListUserRoles(userId).FirstOrDefault();
            ViewBag.UserId = userId;
            ViewBag.Roles = new SelectList(db.Roles.ToList(), "Name", "Name", currentRole);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserRoles(string userId, string roleName)
        {
            //This is where I will be using the UserRolesHelper class
            //The first thing I want to do is make sure I remove user from all roles they may occupy.
            foreach(var role in roleHelper.ListUserRoles(userId))
            {
                roleHelper.RemoveUserFromRole(userId, role);
            }

            //If the incoming role selection Is not null i want to assign the user to selected role
            if(! string.IsNullOrEmpty(roleName))
            {
                roleHelper.AddUserToRole(userId, roleName);
            }
            return RedirectToAction("UserIndex");
       }

        public ActionResult ManageProjects()
        {
            return View();
        }

        public ActionResult ManageRoles()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ManageRoles(List<string>users, string roleName)
        {
            if(users != null)
            {
                foreach (var userId in users)
                {
                    //Get a list of roles for this user
                    foreach (var role in roleHelper.ListUserRoles(userId))
                    {
                        roleHelper.RemoveUserFromRole(userId, role);
                    }

                    //Only to add user back to role
                    if (roleName != null)
                    {
                        roleHelper.AddUserToRole(userId, roleName);
                    }


                }
                

            }
            return RedirectToAction("Index", "Home");
            
           

        }

        public ActionResult ManageUserProjects(string userId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ManageUserProjects(List<int>projects, string userId)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult ManageProjectUsers(int projectId, List<string> ProjectManagers, List<string> Developers, List<string> Submitters)
        {
            //Step 1.  Remove all users from the incoming project
            foreach(var user in projectHelper.UsersOnProject(projectId).ToList())
            {
                projectHelper.RemoveUserFromProject(user.Id, projectId);
            }
           
            //Step 2:  Adds Back all the selected PM's
            if(ProjectManagers != null)
            {
                foreach(var projectManagerId in ProjectManagers)
                {
                    projectHelper.AddUserToProject(projectManagerId, projectId);
                }
            }

            //Step 3:  Adds back all the selected Developers
            if (Developers != null)
            {
                foreach (var developerId in Developers)
                {
                    projectHelper.AddUserToProject(developerId, projectId);
                }
            }

            //Step4:  Adds back all the selected Submitters
            if (Submitters != null)
            {
                foreach (var submitterId in Submitters)
                {
                    projectHelper.AddUserToProject(submitterId, projectId);
                }
            }

            //Step 5:  Redirect the user somewhere
            return RedirectToAction("Details", "Projects", new { id =projectId});
        }


        public ActionResult ManageUsers()
        {
            return View();
        }

    }
}   