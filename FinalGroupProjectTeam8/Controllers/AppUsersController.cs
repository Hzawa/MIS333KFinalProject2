using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using System.Data.Entity.Validation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace FinalGroupProjectTeam8.Controllers
{
    public class AppUsersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: AppUsers
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: AppUsers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // GET: AppUsers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AppUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FName,MiddleInitial,LName,Birthday,Street,City,State,Zip,UserType,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] AppUser appUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(appUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(appUser);
        }

        // GET: AppUsers/Edit/5
        public ActionResult EditPassword(string id)
        {

            // Employees only screen, customers have a separate screen for this
            if (!User.IsInRole("Employee") && !User.IsInRole("Manager"))
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "You don't have permission to be here." });
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassword(AppUser appUser)
        {

            // Hash the password
            String hashedNewPassword = HttpContext.GetOwinContext().GetUserManager<AppUserManager>().PasswordHasher.HashPassword(appUser.PasswordHash);

            // We only want to edit certain fields
            AppUser EditingAppUser = db.Users.Find(appUser.Id);
            EditingAppUser.PasswordHash = hashedNewPassword;

            // Update DB
            db.Entry(EditingAppUser).State = EntityState.Modified;
            db.SaveChanges();

            // Different redirect for employees / managers
            if (User.IsInRole("Employee") || User.IsInRole("Manager"))
            {
                return View(appUser);
            }

            // Customers go back to their manage page
            return RedirectToAction("Index", "Manage");
        }

        // GET: AppUsers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FName,MiddleInitial,LName,Birthday,Street,City,State,Zip,PhoneNumber,UserName")] AppUser appUser)
        {

            // We only want to edit certain fields
            AppUser EditingAppUser = db.Users.Find(appUser.Id);
            EditingAppUser.FName = appUser.FName;
            EditingAppUser.LName = appUser.LName;
            EditingAppUser.MiddleInitial = appUser.MiddleInitial;
            EditingAppUser.Birthday = appUser.Birthday;
            EditingAppUser.Street = appUser.Street;
            EditingAppUser.City = appUser.City;
            EditingAppUser.State = appUser.State;
            EditingAppUser.Zip = appUser.Zip;
            EditingAppUser.PhoneNumber = appUser.PhoneNumber;
            EditingAppUser.UserName = appUser.UserName;

            if (TryValidateModel(EditingAppUser))
            {
                db.Entry(EditingAppUser).State = EntityState.Modified;
                db.SaveChanges();

                // Different redirect for employees / managers
                if (User.IsInRole("Employee") || User.IsInRole("Manager")) {
                    return View(appUser);
                }

                // Customers go back to their manage page
                return RedirectToAction("Index", "Manage");
            }

            return View(appUser);
        }

        // GET: AppUsers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AppUser appUser = db.Users.Find(id);
            if (appUser == null)
            {
                return HttpNotFound();
            }
            return View(appUser);
        }

        // POST: AppUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AppUser appUser = db.Users.Find(id);
            db.Users.Remove(appUser);
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
