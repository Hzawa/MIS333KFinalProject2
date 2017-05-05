using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FinalGroupProjectTeam8.Models.Transaction;

namespace FinalGroupProjectTeam8.Models
{
    public class BankAccountDetailsViewModel
    {
        public BankAccount BankAccount;
        public List<Transaction> Transactions;

        public String TransactionID { get; set; }
        public String DescriptionFilter { get; set; }
        public TransactionTypeEnum TransactionType { get; set; }
        public Decimal AmountLowerBound { get; set; }
        public Decimal AmountUpperBound { get; set; }
        public DateTime DateLowerBound { get; set; }
        public DateTime DateUpperBound { get; set; }

        public String BankAccountID { get; set; }
    }
}