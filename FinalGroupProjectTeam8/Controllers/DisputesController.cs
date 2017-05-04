using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;

namespace FinalGroupProjectTeam8.Controllers
{
    public class DisputesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Disputes
        public ActionResult Index()
        {
            var disputes = db.Disputes.Include(d => d.Transaction);
            return View(disputes.ToList());
        }

        // GET: Disputes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        public ActionResult CreateDispute(int TransactionID) {
            return View();
        }

        // GET: Disputes/Create
        public ActionResult Create()
        {
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "Description");
            return View();
        }

        // POST: Disputes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DisputeID,Comments,DisputeType,CorrentAmount,TransactionID")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                db.Disputes.Add(dispute);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "Description", dispute.TransactionID);
            return View(dispute);
        }

        // GET: Disputes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "Description", dispute.TransactionID);
            return View(dispute);
        }

        // POST: Disputes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DisputeID,Comments,DisputeType,CorrentAmount,TransactionID")] Dispute dispute)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dispute).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TransactionID = new SelectList(db.Transactions, "TransactionID", "Description", dispute.TransactionID);
            return View(dispute);
        }

        // GET: Disputes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dispute dispute = db.Disputes.Find(id);
            if (dispute == null)
            {
                return HttpNotFound();
            }
            return View(dispute);
        }

        // POST: Disputes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Dispute dispute = db.Disputes.Find(id);
            db.Disputes.Remove(dispute);
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
