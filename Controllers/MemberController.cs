using Debugger_Project.Helpers;
using Debugger_Project.Models;
using Debugger_Project.ViewModels;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Debugger_Project.Controllers
{
    public class MemberController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Member
        [HttpGet]
        [Authorize]
        public ActionResult EditProfile()
        {
            var userId = User.Identity.GetUserId();

            //I want to go out to the database and get a portion of the user record for this person
            // in order to create an instance of MemberViewModel
            var member = db.Users.Select(user => new UserProfileViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                DisplayName = user.DisplayName,
                Email = user.Email,
                //AvatarUrl = user.AvatarUrl
            }).FirstOrDefault(user => user.Id == userId);

            //I want to push an instance of MemberViewModel into the view...
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(UserProfileViewModel member)
        {
            //Step 1: Use the incoming member Id to get the actual Application User record from the DB
            var user = db.Users.Find(member.Id);
            user.FirstName = member.FirstName;
            user.LastName = member.LastName;
            user.DisplayName = member.DisplayName;
            user.Email = member.Email;
            user.UserName = member.Email;

            //Step 2: Determine if the user has changed their Avatar
            if (ImageHelper.IsWebFriendlyImage(member.Avatar))
            {
                var fileName = Path.GetFileName(member.Avatar.FileName);
                member.Avatar.SaveAs(Path.Combine(Server.MapPath("~/Uploads/"), fileName));
                user.AvatarUrl = "/Uploads/" + fileName;
            }

            //Step 3: Persist the changes to SQL
            db.SaveChanges();

            //Step 4: Redirect the user to an appropriate area of the application
            return RedirectToAction("Index", "Home");
          
        }

    }
}