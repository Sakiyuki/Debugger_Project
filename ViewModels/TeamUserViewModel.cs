using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Debugger_Project.ViewModels
{
    public class TeamUserViewModel
    {
        public string Id { get; set; }
        public string AvatarUrl { get; set; }
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}