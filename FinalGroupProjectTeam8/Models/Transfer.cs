using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Transfer : Transaction
    {

        // Transfers are received by accounts
        public String ReceivingBankAccountID { get; set; }
        [ForeignKey("ReceivingBankAccountID")]
        public virtual BankAccount ReceivingBankAccount { get; set; }

        public Transfer()
        {

            // Setting the type on instantiation so we can be sure type is always properly set
            this.TransactionType = TransactionTypeEnum.Transfer;
        }
    }
}