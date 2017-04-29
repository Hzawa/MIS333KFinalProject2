using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalGroupProjectTeam8.Models
{
    public class CheckingAccount : CashAccount
    {
        public CheckingAccount()
        {

            // Setting the type on instantiation so we can be sure type is always properly set
            this.AccountType = AccountTypeEnum.CheckingAccount;
        }

    }
}