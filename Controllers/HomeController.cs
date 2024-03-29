﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Debugger_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [Authorize]
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult DemoUser()
        {
            return View();
        }

        [Authorize]
        public ActionResult AdminDashBoard()
        {
            return View();
        }

        public ActionResult DemoAdmin()
        {
            return View();
        }

        [Authorize]
        public ActionResult ProjectManagerDashBoard()
        {
            return View();
        }

    }
}