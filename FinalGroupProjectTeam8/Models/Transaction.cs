﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Transaction
    {
        
        public enum TransactionTypeEnum { All, Withdrawal, Deposit, Transfer, Payment, Fee }
        public TransactionTypeEnum TransactionType { get; set; }

        public enum TransactionStatusEnum { Pending, Approved, Rejected }
        public TransactionStatusEnum TransactionStatus { get; set; }

        [Display(Name = "Transaction Number")]
        public String TransactionID { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required.")]
        public String Description { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount is required.")]
        public Decimal Amount { get; set; }

        public String Comments { get; set; }
        public DateTime Date { get; set; }

        /**
         * Navigational properties
         */

        // Transactions are created by accounts
        public String BankAccountID { get; set; }
        [ForeignKey("BankAccountID")]
        public virtual BankAccount BankAccount { get; set; }

        // A transaction may have a payee
        public String PayeeID { get; set; }
        [ForeignKey("PayeeID")]
        public virtual Payee Payee { get; set; }

        // A transaction may have multiple disputes
        public virtual List<Dispute> Disputes { get; set; }

    }
}