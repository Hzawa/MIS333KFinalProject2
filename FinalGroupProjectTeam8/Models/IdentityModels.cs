using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace FinalGroupProjectTeam8.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class AppUser : IdentityUser
    {

        public enum UserTypeEnum { Customer, Employee, Manager }
        public UserTypeEnum UserType { get; set; }

        [Display(Name = "Member ID")]
        public String BankUserID { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First name is required.")]
        public String FirstName { get; set; }

        [Display(Name = "Middle Initial")]
        [Required(ErrorMessage = "Middle initial is required.")]
        public String MiddleInitial { get; set; }

        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Email address is required.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public String EmailAddress { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Password is required.")]
        public String Password { get; set; }

        /**
         * Identity has build in phone number - ignore this for now
         */
        /* 
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public String PhoneNumber { get; set; }
        */

        [Display(Name = "Birthday")]
        [Required(ErrorMessage = "Birthday is required.")]
        public DateTime Birthday { get; set; }

        [Display(Name = "Street")]
        [Required(ErrorMessage = "Street is required.")]
        public String Street { get; set; }

        [Display(Name = "City")]
        [Required(ErrorMessage = "City is required.")]
        public String City { get; set; }

        [Display(Name = "State")]
        [Required(ErrorMessage = "State is required.")]
        public String State { get; set; }

        [Display(Name = "Zipcode")]
        [Required(ErrorMessage = "Zipcode is required.")]
        public String Zip { get; set; }

        //This method allows you to create a new user
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    //TODO: Here's your db context for the project.  All of your db sets should go in here
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        //TODO:  Add dbsets here, for instance there's one for books
        //Remember, Identity adds a db set for users, so you shouldn't add that one - you will get an error

        // AppUser automatically supported by Identity: public DbSet<AppUser> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Dispute> Disputes { get; set; }
        public DbSet<Payee> Payees { get; set; }

        //TODO: Make sure that your connection string name is correct here.
        public AppDbContext()
            : base("MyDBConnection", throwIfV1Schema: false)
        {
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<AppRole> AppRoles { get; set; }
    }
}