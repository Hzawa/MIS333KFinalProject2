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
    //enum properties
    public enum TransactionType { All, Deposit, Withdrawal, Transfer, Bill, Fee }

    //public enum CustomAmounts { FirstHundred, SecondHundred, ThirdHundred, Over300 }

    public enum SortOrder { Ascending, Descending }

    public class TransactionsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        //method to get all transactions
        public SelectList GetAllTransactions()
        {
            var query = from c in db.Transactions
                        orderby c.TransactionType
                        select c.TransactionType;

            List<Transaction.TransactionTypeEnum> allTransactions = query.Distinct().ToList();

            Transaction.TransactionTypeEnum NoChoice = new Transaction.TransactionTypeEnum() { };
            allTransactions.Add(NoChoice);

            SelectList allTransactionsList = new SelectList(allTransactions);

            return allTransactionsList;
        }

        //advanced search method
        public ActionResult SearchResults(String SearchString, String Description, Transaction.TransactionTypeEnum SelectedTransaction, Decimal Amount, String TransactionID, DateTime Date, SortOrder SelectedSortOrder)
        {
            List<Transaction> SelectedTransactions = new List<Transaction>();
            List<Transaction> AllTransactions = db.Transactions.ToList();

            //start with db set with wanted data
            var query = from t in db.Transactions
                        select t;
            
            //code for textbox searching
            if (SearchString == null || SearchString == "") //they didn't select anything
            {
                //query = query
            }
            else //they picked something
            {
                query = query.Where( t => t.Description.Contains(SearchString) || t.TransactionID.Contains(SearchString) );
            }

            //Transaction drop down list
            if (SelectedTransaction == 0)
            {
                //query = query
            }
            else
            {
                query = query.Where(t => t.TransactionType == SelectedTransaction);
            }

            //Code for radio buttons
            //switch (SelectedGender)
            //{
            //    case Gender.All:
            //        //query = query 
            //        break;
            //    case Gender.Male:
            //        query = query.Where(c => c.Gender == "Male");
            //        break;
            //    case Gender.Female:
            //        query = query.Where(c => c.Gender == "Female");
            //        break;
            //}

            //Code for desired sales textbox
            //check to see if string is valid
            if ( Convert.ToString(Amount) == null || Convert.ToString(Amount) == "")
            {
                //query = query 
            }
            else
            {
                try
                {
                    Amount = Convert.ToDecimal(Amount);
                }

                //code to display when something is wrong
                catch
                {
                    //add message for viewbag
                    ViewBag.Message = Amount + " is not a valid number. Please try again.";

                    //re-populate dropdown
                    ViewBag.TransactionType = GetAllTransactions();

                    //send user back to advanced search pagee
                    return View("Transactions/Details");
                }

                //radio buttons for specific parameters 
                //switch (Convert.ToString(Amount))
                //    {
                //        case :
                //        {
                //            query = query.Where(c => c.AverageSale >= decSalesAmount);
                //            break;
                //        }
                //        case CustomAmounts.FirstHundred:
                //        {
                //            query = query.Where(c => c.AverageSale <= decSalesAmount);
                //            break;
                //        }
                //        case CustomAmounts.FirstHundred:
                //        {
                //            query = query.Where(c => c.AverageSale <= decSalesAmount);
                //            break;
                //        }
                //        case CustomAmounts.FirstHundred:
                //        {
                //            query = query.Where(c => c.AverageSale <= decSalesAmount);
                //            break;
                //        }
                //   }
            }

            // Transaction Date
            //if (daysBack == 0)
            //{
            //    // Display / add to view bag
            //    ViewBag.NumberSelectedTransactions = db.Transactions.ToList().Count;

            //    // Display list
            //    return View(db.Transactions.ToList());
            //}
            //else
            //{
            //    var oldestDate = (DateTime.Today).AddDays(Convert.ToDouble(daysBack) * -1);

            //    query = from t in db.Transactions
            //            where t.TransactionDate >= oldestDate
            //            select t;

            //}

            SelectedTransactions = query.ToList();

            //count # of records
            ViewBag.CustomerCount = SelectedTransactions.Count();
            ViewBag.TotalCustomerCount = AllTransactions.Count();

            //return limited queries
            return View("Transactions/Details", SelectedTransactions.OrderBy(t => t.TransactionID).ThenBy(t =>t.TransactionType).ThenBy(t => t.Description).ThenBy(t => t.Amount));
        }




public ActionResult Search() {
            // Kevin work here

            return View();
        }

        public ActionResult SearchResults() {
            // Kevin work here

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

        public ActionResult CreateDeposit() {

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
            if (ModelState.IsValid)
            {
                
                try
                {
                    // Your code...
                    // Could also be before try if you know the exception occurs in SaveChanges
                    db.Transactions.Add(deposit);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                } catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
                
                return RedirectToAction("Index");
            }

            // We need a list of bank accounts to deposit to
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            var accounts = from a in db.BankAccounts
                           where a.UserID.Equals(userId)
                           select a;
            ViewBag.BankAccountID = new SelectList(accounts, "BankAccountID", "Name");

            // The actual view to create a deposit
            return View();
        }

        //GET: Transactions/Withdrawal
        [HttpGet]
        public ActionResult CreateWithdrawal()
        {

            // The actual view to create withdrawal
            return View();
        }
        //POST: Transactions/Withdrawal
        [HttpPost]
        public ActionResult CreateWithrawal(Withdrawal withdrawal, BankAccount.BankAccountTypeEnum from)
        {

            // The actual view to create withdrawal
            return View();
        }

        //GET: Transactions/Transfer
        [HttpGet]
        public ActionResult CreateTransfer()
        {

            // The actual view to create transfer
            return View();
        }
        //POST: Transactions/Transfer
        [HttpPost]
        public ActionResult CreateTransfer(Transfer transfer, BankAccount.BankAccountTypeEnum from, BankAccount.BankAccountTypeEnum to)
        {

            // The actual view to create transfer
            return View();
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
