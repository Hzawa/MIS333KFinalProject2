using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using static FinalGroupProjectTeam8.Models.Transaction;

namespace FinalGroupProjectTeam8.Models
{
    public class BankAccountDetailsViewModel
    {
        public BankAccount BankAccount;
        public List<Transaction> Transactions;

        [Display(Name = "Transaction Number")]
        public String TransactionID { get; set; }

        [Display(Name = "Description")]
        public String DescriptionFilter { get; set; }

        [Display(Name = "Transaction Type")]
        public TransactionTypeEnum TransactionType { get; set; }

        [Display(Name = "Amount Lower Bound")]
        public Decimal AmountLowerBound { get; set; }

        [Display(Name = "Amount Upper Bound")]
        public Decimal AmountUpperBound { get; set; }

        [Display(Name = "Date Lower Bound")]
        public DateTime DateLowerBound { get; set; }

        [Display(Name = "Date Upper Bound")]
        public DateTime DateUpperBound { get; set; }

        public String BankAccountID { get; set; }

        public Boolean AllowDisputeCreation { get; set; }
    }
}