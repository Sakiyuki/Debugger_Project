using Debugger_Project.Enumerations;
using Debugger_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public abstract class CommonHelper
    {
        protected static ApplicationDbContext db = new ApplicationDbContext();
        protected static UserRolesHelper RoleHelper = new UserRolesHelper();
        protected static ProjectsHelper ProjectHelper = new ProjectsHelper();
        protected static ApplicationUser CurrentUser = null;
        protected SystemRole CurrentRole = SystemRole.None;

        protected CommonHelper()
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            if (userId != null)
                CurrentUser = db.Users.Find(userId);

            var stringRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();
            if (!string.IsNullOrEmpty(stringRole))
                CurrentRole = (SystemRole)Enum.Parse(typeof(SystemRole), stringRole);
            
        }
    }
}