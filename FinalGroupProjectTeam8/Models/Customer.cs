using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Customer : AppUser
    {
        public Customer()
        {

            // Setting the type on instantiation so we can be sure type is always properly set
            this.UserType = UserTypeEnum.Customer;
        }

        public String DefaultAccountID { get; set; }

        /**
         * Navigational properties
         */

        // Each customer can have many accounts
        public ICollection<Account> Accounts { get; set; }
    }
}