using Debugger_Project.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public class NavHelper : CommonHelper
    {
        //Help determine what links I can see in the side nav
        public bool UserCanAddTickets()
        {
            // I am going to switch off of the
            switch (this.CurrentRole)
            {
                case SystemRole.Submitter:
                    return true;
                case SystemRole.Admin: //These 3 lines of code are called a fall through case.
                case SystemRole.ProjectManager:
                case SystemRole.Developer:
                default:
                    return false;
            }
        }
    }
}