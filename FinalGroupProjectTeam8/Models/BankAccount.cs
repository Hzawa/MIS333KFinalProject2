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

        [Display(Name = "Bank Account Number")]
        public String BankAccountID { get; set; }
        public String Name { get; set; }
        public Decimal Balance { get; set; }
        public Boolean Active { get; set; }

        /**
         * Navigational properties
         */
        
        // Each account must be owned by a single customer
        public String UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual AppUser User { get; set; }

        // Each account has multiple transactions
        public virtual List<Transaction> Transactions { get; set; }

        // ShortID for displaying ID with last 4 characters
        public String ShortBankAccountID {
            get { return "XXXXXX" + BankAccountID.Substring(Math.Max(0, BankAccountID.Length - 4)); }
        }
        public String NameWithBalance
        {
            get { return "(" + Balance.ToString("C") + ")" + " " + Name;  }
        }

    }
}