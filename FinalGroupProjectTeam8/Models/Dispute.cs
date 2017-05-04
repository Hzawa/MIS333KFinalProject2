using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FinalGroupProjectTeam8.Models
{
    public class Dispute
    {
        public String DisputeID { get; set; }
        public String Comments { get; set; }

        /**
         * Navigational Properties
         */
        
        // Each dispute belongs to one transaction
        
        public String TransactionID { get; set; }
        [ForeignKey("TransactionID")]
        public virtual Transaction Transaction { get; set; }

    }
}