using Debugger_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public class NavigationHelper : CommonHelper
    {
        public static List<Project>ListUserProjects(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return new List<Project>();

            //User Projects are whichever projects the Use is assigned to... This is not base on Role
            return db.Users.Find(userId).Projects.ToList();
        }
        
        //public static List<Ticket>ListUserTickets(string userId)
        //{
        //    if (string.IsNullOrEmpty(userId))
        //        return new List<Ticket>();

        //    //Ticket Lists are based on Role...
        //    var myRole = RoleHelper.ListUserRoles(userId).FirstOrDefault();
        //    switch (myRole)
        //    {

        //    }
        //}


        //Does the logged in User have access to a Create Ticket Link??
        //public static bool UserCanCreateTicket()
        //{
        //    var userId = HttpContext.Current.User.Identity.GetUserId();
        //}
    }
}