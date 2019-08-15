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

        public static bool TicketDetailsViewableByUser(int ticketId)
        {
            //How do I use User when I am outside of the Controller??
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var roleName = roleHelper.ListUserRoles(userId).FirstOrDefault();
            var systemRole = (SystemRole)Enum.Parse(typeof(SystemRole), roleName);

            switch (systemRole)
            {
                case SystemRole.Admin:
                    break;
                case SystemRole.ProjectManager:
                    break;
                case SystemRole.Developer:
                    break;
                case SystemRole.Subscriber:
                    break;

            }
            return false;
        }

        public static bool TicketIsEditableByUser(Ticket ticket)
        {
            var userId = HttpContext.Current.User.Identity.GetUserId();
            var MyRole = roleHelper.ListUserRoles(userId).FirstOrDefault();

            switch (MyRole)
            {
                case "Developer":
                    return ticket.AssignedToUserId == userId;
                case "Submitter":
                    return ticket.OwnerUserId == userId;
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