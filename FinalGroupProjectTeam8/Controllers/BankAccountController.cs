using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FinalGroupProjectTeam8.Models;

namespace FinalGroupProjectTeam8.Controllers
{
    public class BankAccountController : Controller
    {

        private AppDbContext db = new AppDbContext();

        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //public SelectList GetAllBankAccounts(AppUser @appuser)  //COMMITTEE ALREADY CHOSEN
        //{
        //    //populate list of committees
        //    var query = from c in db.BankAccounts
        //                orderby c.Name
        //                select c;
        //    //create list and execute query
        //    List<BankAccount> allBankAccounts = query.ToList();

        //    //convert to select list
        //    SelectList list = new SelectList(allBankAccounts, "BankAccountID", "AccountType", @appuser.BankAccounts.);
        //    return list;
        //}

        //public SelectList GetAllBankAccounts()  //NO COMMITTEE CHOOSEN
        //{
        //    //create query to find all committees
        //    var query = from c in db.BankAccounts
        //                orderby c.Name
        //                select c;
        //    //execute query and store in list
        //    List<BankAccount> allBankAccounts = query.ToList();

        //    //convert list to select list format needed for HTML
        //    SelectList allBankAccountsList = new SelectList(allBankAccounts, "BankAccountID", "Name");

        //    return allBankAccountsList;
        //}


        //
        // POST: /Account/Register
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Register(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //Add fields to user here so they will be saved to do the database
        //        var user = new AppUser { UserName = model.Email, Email = model.Email, FName = model.FName, MiddleInitial = model.MiddleInitial, LName = model.LName, Street = model.Street, City = model.City, State = model.State, Zip = model.Zip, Birthday = model.Birthday, PhoneNumber = model.PhoneNumber };

        //        //add user to database
        //        var result = await UserManager.CreateAsync(user, model.Password);

        //        //TODO:  Once you get roles working, you may want to add users to roles upon creation
        //        await UserManager.AddToRoleAsync(user.Id, "BankUser");
        //        // --OR--
        //        // await UserManager.AddToRoleAsync(user.Id, "Employee");


        //        if (result.Succeeded)
        //        {
        //            await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

        //            // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
        //            // Send an email with this link
        //            // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
        //            // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
        //            // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        //            return RedirectToAction("Index", "Home");
        //        }
        //        AddErrors(result);
        //    }

        //    // If we got this far, something failed, redisplay form
        //    return View(model);
        //}
    }
}