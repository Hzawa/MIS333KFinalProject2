using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity.Validation;

namespace FinalGroupProjectTeam8.Controllers
{
    public class TransactionsController : Controller
    {

        private AppDbContext db = new AppDbContext();

        public ActionResult Success(String Message)
        {
            ViewBag.Message = Message;
            return View();
        }

        // GET: Disputes/Edit/5
        public ActionResult Resolve(string TransactionID)
        {
            if (TransactionID == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction Transaction = db.Transactions.Find(TransactionID);
            if (Transaction == null)
            {
                return HttpNotFound();
            }
            return View(Transaction);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Resolve(Transaction Transaction)
        {

            // Update the Transaction
            Transaction EditedTransaction = db.Transactions.Find(Transaction.TransactionID);
            EditedTransaction.TransactionStatus = Transaction.TransactionStatus;
            EditedTransaction.Comments = Transaction.Comments;

            if (EditedTransaction.TransactionStatus == Transaction.TransactionStatusEnum.Approved)
            {

                // Get the corresponding bank account
                BankAccount BankAccount = db.BankAccounts.Find(EditedTransaction.BankAccountID);

                // If transaction was successful, adjust account with the difference
                if (EditedTransaction.TransactionType == Transaction.TransactionTypeEnum.Deposit)
                {
                    BankAccount.Balance = BankAccount.Balance + EditedTransaction.Amount;
                }
                else if (EditedTransaction.TransactionType == Transaction.TransactionTypeEnum.Withdrawal || EditedTransaction.TransactionType == Transaction.TransactionTypeEnum.Payment)
                {
                    BankAccount.Balance = BankAccount.Balance - EditedTransaction.Amount;
                }
                else if (EditedTransaction.TransactionType == Transaction.TransactionTypeEnum.Transfer)
                {
                    BankAccount.Balance = BankAccount.Balance - EditedTransaction.Amount;

                    // Must also update the receiving bank account
                    Transfer Transfer = (Transfer)db.Transactions.Find(EditedTransaction.TransactionID);
                    BankAccount ReceivingBankAccount = db.BankAccounts.Find(Transfer.ReceivingBankAccountID);
                    ReceivingBankAccount.Balance = ReceivingBankAccount.Balance + EditedTransaction.Amount;
                    db.Entry(ReceivingBankAccount).State = EntityState.Modified;
                    db.SaveChanges();
                }

                // Update BankAccount
                db.Entry(BankAccount).State = EntityState.Modified;
                db.SaveChanges();

            }

            // Finally, update the dispute
            ModelState.Clear();
            if (TryValidateModel(EditedTransaction))
            {
                db.Entry(EditedTransaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ConfirmResolution");
            }
            return View(Transaction);
        }

        public ActionResult ConfirmResolution() {
            return View();
        }


        // GET: Transactions
        public ActionResult Index()
        {
            var transactions = db.Transactions.Include(t => t.BankAccount).Include(t => t.Disputes).Include(t => t.Payee);
            return View(transactions.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(string id)
        {

            // Get an instance of current transaction
            Transaction Transaction = db.Transactions.Find(id);

            //find most recent transactions
            var transactions = from t in db.Transactions
                               where t.TransactionType.Equals(Transaction.TransactionType)
                               orderby t.Date
                               select t;
            List<Transaction> AllTransactions = transactions.ToList();

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

        public ActionResult CreateTransaction()
        {
            // A view with 3 buttons, one to create withdrawal, deposit, and transfer
            return View();
        }

        //GET: Transactions/Deposit
        [HttpGet]
        public ActionResult CreateDeposit()
        {

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create a deposit
            return View();
        }
        //POST: Transactions/Deposit
        [HttpPost]
        public ActionResult CreateDeposit(Deposit deposit)
        {

            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) deposit.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                deposit.TransactionID = nextIdString;
            }

            // Do validation here
            if (deposit.Amount <= 0) {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (deposit.Amount > 5000)
            {
                deposit.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                deposit.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(deposit.BankAccountID);
                BankAccount.Balance = BankAccount.Balance + deposit.Amount;
                db.SaveChanges();
            }

            // Actually committing the deposit
            if (ModelState.IsValid)
            {
                db.Transactions.Add(deposit);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = deposit.BankAccountID });
            }

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create a deposit
            return RedirectToAction("Details", "BankAccount", new { BankAccountID = deposit.BankAccountID });
        }

        //GET: Transactions/Withdrawal
        [HttpGet]
        public ActionResult CreateWithdrawal()
        {
            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create withdrawal
            return View();
        }
        //POST: Transactions/Withdrawal
        [HttpPost]
        public ActionResult CreateWithdrawal(Withdrawal withdrawal)
        {
            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) withdrawal.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                withdrawal.TransactionID = nextIdString;
            }

            // Do validation here
            if (withdrawal.Amount <= 0)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (withdrawal.Amount > 1000)
            {
                withdrawal.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                withdrawal.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(withdrawal.BankAccountID);
                BankAccount.Balance = BankAccount.Balance - withdrawal.Amount;
                db.SaveChanges();
            }

            // Actually committing the withdrawal
            if (ModelState.IsValid)
            {
                db.Transactions.Add(withdrawal);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = withdrawal.BankAccountID });
            }

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create withdrawwal
            return RedirectToAction("Details", "BankAccount", new { BankAccountID = withdrawal.BankAccountID });
        }

        //GET: Transactions/Transfer
        [HttpGet]
        public ActionResult CreateTransfer()
        {
            // We need a list of bank accounts to transfer to and from
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");
            ViewBag.ReceivingBankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create transfer
            return View();
        }
        //POST: Transactions/Transfer
        [HttpPost]
        public ActionResult CreateTransfer(Transfer transfer)
        {
            // Give this transaction the right primary key
            var idObject = db.Transactions.OrderByDescending(b => b.TransactionID).FirstOrDefault();
            if (idObject == null) transfer.TransactionID = "1000000000";
            else
            {
                int nextId = Convert.ToInt32(idObject.TransactionID) + 1;
                String nextIdString = nextId.ToString();
                transfer.TransactionID = nextIdString;
            }

            // Do validation here
            if (transfer.Amount <= 0)
            {
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Please deposit an amount greater than 0." });
            }

            // Any changes
            if (transfer.Amount > 1000)
            {
                transfer.TransactionStatus = Transaction.TransactionStatusEnum.Pending;
            }
            else
            {
                transfer.TransactionStatus = Transaction.TransactionStatusEnum.Approved;

                // Must update the balance
                BankAccount BankAccount = db.BankAccounts.Find(transfer.BankAccountID);
                BankAccount.Balance = BankAccount.Balance - transfer.Amount;
                db.SaveChanges();

                // Of the receiving account too...
                BankAccount ReceivingBankAccount = db.BankAccounts.Find(transfer.ReceivingBankAccountID);
                ReceivingBankAccount.Balance = ReceivingBankAccount.Balance + transfer.Amount;
                db.SaveChanges();
            }

            // Actually committing the withdrawal
            if (ModelState.IsValid)
            {
                db.Transactions.Add(transfer);
                db.SaveChanges();
                return RedirectToAction("Details", "BankAccount", new { BankAccountID = transfer.BankAccountID });
            } else
            {
                foreach (ModelState modelState in ViewData.ModelState.Values)
                {
                    foreach (ModelError error in modelState.Errors)
                    {
                        System.Diagnostics.Debug.WriteLine("Error: " + error.ErrorMessage);
                    }
                }
                return RedirectToAction("Error", "Home", new { ErrorMessage = "Something went wrong creating your transfer." });
            }

        }

        [HttpGet]
        public ActionResult CreatePayment()
        {

            // The actual view to create transfer
            return View();
        }
        //POST: Transactions/Deposit
        [HttpPost]
        public ActionResult CreatePayment(Payment payment, Payee payee)
        {

            // The actual view to create transfer
            return View();
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name");
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

            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
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
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
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
            ViewBag.BankAccountID = new SelectList(db.BankAccounts, "BankAccountID", "Name", transaction.BankAccountID);
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
