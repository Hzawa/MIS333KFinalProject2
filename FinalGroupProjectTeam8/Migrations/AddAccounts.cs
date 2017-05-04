using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FinalGroupProjectTeam8.Models;

namespace FinalGroupProjectTeam8.Migrations
{
    public class AddAccounts
    {
        public static void SeedAccounts(AppDbContext context)
        {
            context.BankAccounts.AddOrUpdate(
                a => a.BankAccountID,
                new StockPortfolio()
                {
                    BankAccountID = 1000000000.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "Dixon@aool.com").ToList()[0],
                    Name = "Shan's Stock",
                    Balance = 0.0m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000001.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "willsheff@email.com").ToList()[0],
                    Name = "William's Savings",
                    Balance = 40035.5m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000002.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "smartinmartin.Martin@aool.com").ToList()[0],
                    Name = "Gregory's Checking",
                    Balance = 39779.49m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000003.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "avelasco@yaho.com").ToList()[0],
                    Name = "Allen's Checking",
                    Balance = 47277.33m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000004.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "rwood@voyager.net").ToList()[0],
                    Name = "Reagan's Checking",
                    Balance = 70812.15m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000005.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "nelson.Kelly@aool.com").ToList()[0],
                    Name = "Kelly's Savings",
                    Balance = 21901.97m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000006.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "erynrice@aool.com").ToList()[0],
                    Name = "Eryn's Checking",
                    Balance = 70480.99m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000007.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "westj@pioneer.net").ToList()[0],
                    Name = "Jake's Savings",
                    Balance = 7916.4m,
                    Active = true
                },
                new StockPortfolio()
                {
                    BankAccountID = 1000000008.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "mb@aool.com").ToList()[0],
                    Name = "Michelle's Stock",
                    Balance = 0.0m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000009.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "jeff@ggmail.com").ToList()[0],
                    Name = "Jeffrey's Savings",
                    Balance = 69576.83m,
                    Active = true
                },
                new StockPortfolio()
                {
                    BankAccountID = 1000000010.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "nelson.Kelly@aool.com").ToList()[0],
                    Name = "Kelly's Stock",
                    Balance = 0.0m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000011.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "erynrice@aool.com").ToList()[0],
                    Name = "Eryn's Checking 2",
                    Balance = 30279.33m,
                    Active = true
                },
                new IRA()
                {
                    BankAccountID = 1000000012.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "mackcloud@pimpdaddy.com").ToList()[0],
                    Name = "Jennifer's IRA",
                    Balance = 53177.21m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000013.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "ss34@ggmail.com").ToList()[0],
                    Name = "Sarah's Savings",
                    Balance = 11958.08m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000014.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "tanner@ggmail.com").ToList()[0],
                    Name = "Jeremy's Savings",
                    Balance = 72990.47m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000015.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "liz@ggmail.com").ToList()[0],
                    Name = "Elizabeth's Savings",
                    Balance = 7417.2m,
                    Active = true
                },
                new IRA()
                {
                    BankAccountID = 1000000016.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "ra@aoo.com").ToList()[0],
                    Name = "Allen's IRA",
                    Balance = 75866.69m,
                    Active = true
                },
                new StockPortfolio()
                {
                    BankAccountID = 1000000017.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "johnsmith187@aool.com").ToList()[0],
                    Name = "John's Stock",
                    Balance = 0.0m,
                    Active = true
                },
                new SavingsAccount()
                {
                    BankAccountID = 1000000018.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "mclarence@aool.com").ToList()[0],
                    Name = "Clarence's Savings",
                    Balance = 1642.82m,
                    Active = true
                },
                new CheckingAccount()
                {
                    BankAccountID = 1000000019.ToString(),
                    User = context.Users.Where((AppUser user) => user.Email == "ss34@ggmail.com").ToList()[0],
                    Name = "Sarah's Checking",
                    Balance = 84421.45m,
                    Active = true
                }
            );
        }
    }
}