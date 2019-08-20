using Debugger_Project.Enumerations;
using Debugger_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public class TicketDecisionHelper : CommonHelper
    {
        private static ApplicationDbContext db = new ApplicationDbContext();
        private static UserRolesHelper roleHelper = new UserRolesHelper();
        private static ProjectsHelper projectHelper = new ProjectsHelper();

        public static bool TicketDetailsViewableByUser(int ticketId)
        {
            //How do I use User when I am outside of the Controller??
            var userId = HttpContext.Current.User.Identity.GetUserId();// Here var userId is being set to the current userId by using 
            //Current.User.Identity.GetUserId(). Since we are not in a class that is 
            //a sub-class of controller we have to go all the way out to HttpContext this gets the UserId, but the user has to be logged in.
            var roleName = roleHelper.ListUserRoles(userId).FirstOrDefault();//This line will list the user role based on Id and return the first or default role.
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName); //This line will turn systemRole into it's enum equivalent.

            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Submitter:
                    break;

            }
            return false;
        }

        public static bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var myRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (myRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId; //If I am a developer and I have been assigned to this ticket, I can edit it.
                case "Submitter":
                    return ticket.OwnerUserId == userId;// If I am a submitter, and I am assigned to this ticket, I can edit it.
                case "ProjectManager":

                    var myProjects = projectHelper.ListUserProjects(userId);
                    foreach (var project in myProjects)
                    {
                        foreach (var projticket in project.Tickets)
                        {
                            if (projticket.Id == ticket.Id)
                                return true;
                        }
                    }
                    return false;

                case "Admin":
                    return true;
                default:
                    return false;
            }
        }                                        
    }

}