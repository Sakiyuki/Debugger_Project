using Debugger_Project.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace Debugger_Project.Helpers
{
    public class HistoryHelper : CommonHelper
    {
        public void RecordHistory(Ticket oldTicket, Ticket newTicket)
        {
            foreach (var propObj1 in newTicket.GetType().GetProperties())
            {
                //If the current property is not one of the properties I am interested in then moveon..
                var trackedProperties = WebConfigurationManager.AppSettings["TrackedHistoryProperties"].Split(',').ToList();
                if (!trackedProperties.Contains(propObj1.Name))
                    continue;

                //Else generate a TicketHistory record
                var oldTicketProp = oldTicket.GetType().GetProperty(propObj1.Name);
                var newTicketProp = newTicket.GetType().GetProperty(propObj1.Name);

                var oldPropValue = propObj1.GetValue(oldTicket, null).ToString();
                var newPropValue = propObj1.GetValue(newTicket, null).ToString();

                if (oldPropValue != newPropValue)
                {
                    var newHistory = new TicketHistory
                    {
                        PropertyName = propObj1.Name,
                        OldValue = Utilities.MakeReadable(propObj1.Name, oldPropValue.ToString()),
                        NewValue = Utilities.MakeReadable(propObj1.Name, newPropValue.ToString()),
                        UpDated = DateTime.Now,
                        TicketId = newTicket.Id,
                        UserId = HttpContext.Current.User.Identity.GetUserId()
                    };
                    db.TicketHistories.Add(newHistory);
                }
                db.SaveChanges();
            }
        }
        
    }
}