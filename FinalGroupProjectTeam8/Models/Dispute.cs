using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{

    public enum DisputeTypeEnum { Submitted, Accepted, Rejected, Adjusted }

    public class Dispute
    {
        [Display(Name = "Dispute Number")]
        public String DisputeID { get; set; }

        [Required(ErrorMessage = "Comments are required.")]
        public String Comments { get; set; }

        public DisputeTypeEnum DisputeType { get; set; }

        [Display(Name = "Correct Amount")]
        [Required(ErrorMessage = "Correct amount is required.")]
        public int CorrectAmount { get; set; }

        [Display(Name = "Request Deletion?")]
        [Required(ErrorMessage = "Request Deletion is required.")]
        public Boolean RequestDeletion { get; set; }

        /**
         * Navigational Properties
         */
        
        // Each dispute belongs to one transaction
        
        public String TransactionID { get; set; }
        [ForeignKey("TransactionID")]
        public virtual Transaction Transaction { get; set; }

    }
}