using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalGroupProjectTeam8.Models
{
    public enum PayeeType { CreditCard, Utilities, Rent, Mortgage, Other }
    public class Payee
    {

        public String PayeeID { get; set; }
        [Required]
        public PayeeType Type { get; set; }
        [Required]
        public String Name { get; set; }
        [Required]
        public String Street { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        public String State { get; set; }
        [Required]
        public String Zip { get; set; }
        [Required]
        public String PhoneNumber { get; set; }

        /**
         * Navigational properties
         */
        
        // A payee can be tied to multiple transactions
        public virtual List<Transaction> Transactions { get; set; }
        // A payee can belong to many users
        public virtual List<AppUser> AppUsers { get; set; }


        // A payee can be tied to multiple users
        public virtual List<Payee> Payees { get; set; }

    }
}