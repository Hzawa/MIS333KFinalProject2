using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Account
    {
        
        public enum AccountTypeEnum { CheckingAccount, SavingsAccount, IRA, StockPortfolio }
        public AccountTypeEnum AccountType { get; set; }

        public String AccountID { get; set; }
        public String Name { get; set; }
        public Decimal Balance { get; set; }
        public Boolean Active { get; set; }

        /**
         * Navigational properties
         */
        
        // Each account must be owned by a single customer
        [Key]
        public String CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual AppUser User { get; set; }

        // Each account has multiple transactions
        public ICollection<Transaction> Transactions { get; set; }

    }
}