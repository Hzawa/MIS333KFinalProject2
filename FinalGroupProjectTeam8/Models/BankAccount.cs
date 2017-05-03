using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class BankAccount
    {
        
        public enum BankAccountTypeEnum { CheckingAccount, SavingsAccount, IRA, StockPortfolio }
        public BankAccountTypeEnum AccountType { get; set; }

        public Int32 BankAccountID { get; set; }
        public String Name { get; set; }
        public Decimal Balance { get; set; }
        public Boolean Active { get; set; }

        /**
         * Navigational properties
         */
        
        // Each account must be owned by a single customer
        [Key]
        public String UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual AppUser User { get; set; }

        // Each account has multiple transactions
        public virtual List<Transaction> Transactions { get; set; }

    }
}