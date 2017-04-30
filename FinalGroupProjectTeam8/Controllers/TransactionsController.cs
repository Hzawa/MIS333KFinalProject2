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
    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.Disputes).Include(t => t.Payee);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "CustomerID", "BankAccountID");
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID");
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,TransactionType,Description,Amount,Comments,Date,BankAccountID,PayeeID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "CustomerID", "BankAccountID", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "CustomerID", "BankAccountID", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,TransactionType,Description,Amount,Comments,Date,BankAccountID,PayeeID")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "CustomerID", "BankAccountID", transaction.BankAccountID);
            ViewBag.TransactionID = new SelectList(db.Disputes, "TransactionID", "DisputeID", transaction.TransactionID);
            ViewBag.PayeeID = new SelectList(db.Payees, "PayeeID", "Name", transaction.PayeeID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
