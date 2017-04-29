using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Transaction
    {
        
        public enum TransactionTypeEnum { Withdrawal, Deposit, Transfer, Payment }
        public TransactionTypeEnum TransactionType { get; set; }

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
        [Key]
        public String AccountID { get; set; }
        [ForeignKey("AccountID")]
        public virtual Account Account { get; set; }

        // A transaction may have a payee
        [Key]
        public String PayeeID { get; set; }
        [ForeignKey("PayeeID")]
        public virtual Payee Payee { get; set; }

        // A transaction may have multiple disputes
        public ICollection<Dispute> Disputes { get; set; }

    }
}