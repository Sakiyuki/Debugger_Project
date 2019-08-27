using Debugger_Project.Models;
using System;


namespace Debugger_Project.Helpers
{
    public static class Utilities
    {
        private static ApplicationDbContext db = new ApplicationDbContext();

        public static string MakeReadable(string property, string value)
        {
            switch (property)
            {
                case "TicketStatusId":
                    return db.TicketStatuses.Find(Convert.ToInt32(value)).Name;
                case "TicketPriorityId":
                    return db.TicketPriorities.Find(Convert.ToInt32(value)).Name;
                case "TicketTypeId":
                    return db.TicketTypes.Find(Convert.ToInt32(value)).Name;
                case "AssignedToUserId":
                case "OwnerUserId":
                    return db.Users.Find(value).FullName;
                default:
                    return value;

            }
        }
    }
}