﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Debugger_Project.Models;

namespace Debugger_Project.Controllers
{
    public class ProjectUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProjectUsers
        public ActionResult Index()
        {
            return View(db.ProjectUsers.ToList());
        }

        // GET: ProjectUsers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsers projectUsers = db.ProjectUsers.Find(id);
            if (projectUsers == null)
            {
                return HttpNotFound();
            }
            return View(projectUsers);
        }

        // GET: ProjectUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ProjectId,UserId")] ProjectUsers projectUsers)
        {
            if (ModelState.IsValid)
            {
                db.ProjectUsers.Add(projectUsers);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projectUsers);
        }

        // GET: ProjectUsers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsers projectUsers = db.ProjectUsers.Find(id);
            if (projectUsers == null)
            {
                return HttpNotFound();
            }
            return View(projectUsers);
        }

        // POST: ProjectUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ProjectId,UserId")] ProjectUsers projectUsers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectUsers).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(projectUsers);
        }

        // GET: ProjectUsers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectUsers projectUsers = db.ProjectUsers.Find(id);
            if (projectUsers == null)
            {
                return HttpNotFound();
            }
            return View(projectUsers);
        }

        // POST: ProjectUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectUsers projectUsers = db.ProjectUsers.Find(id);
            db.ProjectUsers.Remove(projectUsers);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
