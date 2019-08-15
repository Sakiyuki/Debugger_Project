using Debugger_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public class NotificationHelper : CommonHelper
    {
        public static void CreateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            //There are 4 cases I can think of
            //1.  No Change...
            //2.  An assignment -This means that the old AssignedToUserId was null and the current one is not.
            //3.  An unassignment -This means that the old AssignedToUserId had a a value but the current one is null.
            //4.  A reassignment -The Ticket was assigned to someone and it is now assigned to someone else
            //Setting up a few boolean variables to determine which case I am in

            var noChange = (oldTicket.AssignedToUserId == newTicket.AssignedToUserId);
            var assignment = (string.IsNullOrEmpty(oldTicket.AssignedToUserId));
            var unassignment = (string.IsNullOrEmpty(newTicket.AssignedToUserId));

            if (noChange)
                return;

            if (assignment)
                GenerateAssignmentNotification(oldTicket, newTicket);

            else if (unassignment)
                GenerateUnAssignmentNotification(oldTicket, newTicket);

            else
            {
                GenerateAssignmentNotification(oldTicket, newTicket);
                GenerateUnAssignmentNotification(oldTicket, newTicket);
            }
        }

        private static void GenerateUnAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were unassigned from Ticket Id {newTicket.Id} on {DateTime.Now}",
                Read = false,
                RecipientId = oldTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"Please acknowledge that you have read this notification by marking it'Read'",
                TicketId = newTicket.Id

            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();
        }

        private static void GenerateAssignmentNotification(Ticket oldTicket, Ticket newTicket)
        {
            var notification = new TicketNotification
            {
                Created = DateTime.Now,
                Subject = $"You were assigned to Ticket Id {newTicket.Id} on {DateTime.Now}",
                Read = false,
                RecipientId = newTicket.AssignedToUserId,
                SenderId = HttpContext.Current.User.Identity.GetUserId(),
                NotificationBody = $"Please acknowledge that you have read this notification by checking 'Read'",
                TicketId = newTicket.Id
            };

            db.TicketNotifications.Add(notification);
            db.SaveChanges();

        }
    }
}