using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Payee
    {

        String PayeeID { get; set; }
        String Name { get; set; }
        String Street { get; set; }
        String City { get; set; }
        String State { get; set; }
        String Zip { get; set; }
        String PhoneNumber { get; set; }

        /**
         * Navigational properties
         */
        
        // A payee can be tied to multiple transactions
        public ICollection<Transaction> Transactions { get; set; }

    }
}