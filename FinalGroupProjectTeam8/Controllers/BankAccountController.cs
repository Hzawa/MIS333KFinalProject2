using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace FinalGroupProjectTeam8.Controllers
{
    public class BankAccountController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: /Account/Register
        [Authorize(Roles = "Customer, Manager, Employee")]
        public ActionResult Register()
        {
            return View();
        }

        // POST: BankAccounts/Register
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "AccountType, Name, Balance")] BankAccount BankAccount)
        {
            if (ModelState.IsValid)
            {
                // Setting the UserID
                string UserId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                BankAccount.UserID = UserId;

                // Different behavior and checks depending on account type
                BankAccount.BankAccountTypeEnum BankAccountType = BankAccount.AccountType;
                if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA)
                {

                    // Check if this user already has an IRA account
                    var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
                    var accounts = from a in db.BankAccounts
                                   where a.UserID.Equals(userId)
                                   where a.AccountType.Equals(BankAccountType)
                                   select a;

                    // If we have any results, that means this user already has an IRA account
                    if (accounts.Count() > 0)
                    {
                        // Redirect

                    }

                    else if (BankAccountType == BankAccount.BankAccountTypeEnum.IRA || BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                    {
                        // Check to see if initial deposit greater than $5000
                        Decimal initialDeposit = BankAccount.Balance;
                        if (initialDeposit > 5000)
                        {
                            // Manager must approve if deposit > $5000    
                        }
                    }
                    //If account is checkings or savings provide default name
                    if (BankAccountType == BankAccount.BankAccountTypeEnum.CheckingAccount)
                    {
                        BankAccount.Name = "Longhorn Checking";
                    }
                    else if (BankAccountType == BankAccount.BankAccountTypeEnum.SavingsAccount)
                    {
                       BankAccount.Name = "Longhorn Savings";
                    }

                }

                // Adding the object to the DB
                db.BankAccounts.Add(BankAccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(BankAccount);
        }      
    }
}