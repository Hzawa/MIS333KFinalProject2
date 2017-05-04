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
        public PayeeType Type { get; set; }
        public String Name { get; set; }
        public String Street { get; set; }
        public String City { get; set; }
        public String State { get; set; }
        public String Zip { get; set; }
        public String PhoneNumber { get; set; }

        /**
         * Navigational properties
         */
        
        // A payee can be tied to multiple transactions
        public virtual List<Transaction> Transactions { get; set; }

    }
}