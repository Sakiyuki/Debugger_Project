using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.Helpers
{
    public class ImageHelpers
    {
        public static bool IsWebFriendlyImage(HttpPostedFileBase file)
        {
            return false;
        }

        public static bool IsValidAttachment(HttpPostedFileBase file)
        {
            return false;
        }
    }
}