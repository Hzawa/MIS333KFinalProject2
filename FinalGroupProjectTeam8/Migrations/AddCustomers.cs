using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using FinalGroupProjectTeam8.Models;

namespace FinalGroupProjectTeam8.Migrations
{
    public class AddCustomers
    {
        public static void SeedCustomers(AppDbContext context)
        {
            if (!context.Users.Any(u => u.Email == "cbaker@freezing.co.uk"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "cbaker@freezing.co.uk",
                    FName = "Christopher",
                    LName = "Baker",
                    MiddleInitial = "L",
                    Street = "1245 Lake Austin Blvd.",
                    City = "Austin",
                    State = "TX",
                    Zip = "78733",
                    PhoneNumber = "5125571146",
                    Birthday = DateTime.Parse("2/7/91"),
                    UserName = "cbaker@freezing.co.uk",
                    UserType = UserTypeEnum.Customer,
                    
                };
                manager.Create(user, "gazing");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("cbaker@freezing.co.uk");
                manager.AddToRole(userToAdd.Id, "Customer");
                
            }
            
            if (!context.Users.Any(u => u.Email == "mb@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "mb@aool.com",
                    FName = "Michelle",
                    LName = "Banks",
                    MiddleInitial = "nan",
                    Street = "1300 Tall Pine Lane",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78261",
                    PhoneNumber = "2102678873",
                    Birthday = DateTime.Parse("6/23/90"),
                    UserName = "mb@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "banquet");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("mb@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "fd@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "fd@aool.com",
                    FName = "Franco",
                    LName = "Broccolo",
                    MiddleInitial = "V",
                    Street = "62 Browning Rd",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77019",
                    PhoneNumber = "8175659699",
                    Birthday = DateTime.Parse("5/6/86"),
                    UserName = "fd@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "666666");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("fd@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "wendy@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "wendy@ggmail.com",
                    FName = "Wendy",
                    LName = "Chang",
                    MiddleInitial = "L",
                    Street = "202 Bellmont Hall",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78713",
                    PhoneNumber = "5125943222",
                    Birthday = DateTime.Parse("12/21/64"),
                    UserName = "wendy@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "clover");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("wendy@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "limchou@yaho.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "limchou@yaho.com",
                    FName = "Lim",
                    LName = "Chou",
                    MiddleInitial = "nan",
                    Street = "1600 Teresa Lane",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78266",
                    PhoneNumber = "2107724599",
                    Birthday = DateTime.Parse("6/14/50"),
                    UserName = "limchou@yaho.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "austin");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("limchou@yaho.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "Dixon@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "Dixon@aool.com",
                    FName = "Shan",
                    LName = "Dixon",
                    MiddleInitial = "D",
                    Street = "234 Holston Circle",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75208",
                    PhoneNumber = "2142643255",
                    Birthday = DateTime.Parse("5/9/30"),
                    UserName = "Dixon@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "mailbox");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("Dixon@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "louann@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "louann@ggmail.com",
                    FName = "Lou Ann",
                    LName = "Feeley",
                    MiddleInitial = "K",
                    Street = "600 S 8th Street W",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77010",
                    PhoneNumber = "8172556749",
                    Birthday = DateTime.Parse("2/24/30"),
                    UserName = "louann@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "aggies");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("louann@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            
            if (!context.Users.Any(u => u.Email == "tfreeley@minntonka.ci.state.mn.us"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "tfreeley@minntonka.ci.state.mn.us",
                    FName = "Tesa",
                    LName = "Freeley",
                    MiddleInitial = "P",
                    Street = "4448 Fairview Ave.",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77009",
                    PhoneNumber = "8173255687",
                    Birthday = DateTime.Parse("9/1/35"),
                    UserName = "tfreeley@minntonka.ci.state.mn.us",
                    UserType = UserTypeEnum.Customer,
                    
                };
                manager.Create(user, "raiders");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("tfreeley@minntonka.ci.state.mn.us");
                manager.AddToRole(userToAdd.Id, "Customer");
                

            }
            
            if (!context.Users.Any(u => u.Email == "mgar@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "mgar@aool.com",
                    FName = "Margaret",
                    LName = "Garcia",
                    MiddleInitial = "L",
                    Street = "594 Longview",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77003",
                    PhoneNumber = "8176593544",
                    Birthday = DateTime.Parse("7/3/90"),
                    UserName = "mgar@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "mustangs");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("mgar@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "chaley@thug.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "chaley@thug.com",
                    FName = "Charles",
                    LName = "Haley",
                    MiddleInitial = "E",
                    Street = "One Cowboy Pkwy",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75261",
                    PhoneNumber = "2148475583",
                    Birthday = DateTime.Parse("9/17/85"),
                    UserName = "chaley@thug.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "region");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("chaley@thug.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "jeff@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "jeff@ggmail.com",
                    FName = "Jeffrey",
                    LName = "Hampton",
                    MiddleInitial = "T",
                    Street = "337 38th St.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5126978613",
                    Birthday = DateTime.Parse("1/23/95"),
                    UserName = "jeff@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "hungry");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("jeff@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "wjhearniii@umch.edu"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "wjhearniii@umch.edu",
                    FName = "John",
                    LName = "Hearn",
                    MiddleInitial = "B",
                    Street = "4225 North First",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75237",
                    PhoneNumber = "2148965621",
                    Birthday = DateTime.Parse("1/8/94"),
                    UserName = "wjhearniii@umch.edu",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "logicon");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("wjhearniii@umch.edu");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "hicks43@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "hicks43@ggmail.com",
                    FName = "Anthony",
                    LName = "Hicks",
                    MiddleInitial = "J",
                    Street = "32 NE Garden Ln., Ste 910",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78239",
                    PhoneNumber = "2105788965",
                    Birthday = DateTime.Parse("10/6/90"),
                    UserName = "hicks43@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "doofus");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("hicks43@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "bradsingram@mall.utexas.edu"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "bradsingram@mall.utexas.edu",
                    FName = "Brad",
                    LName = "Ingram",
                    MiddleInitial = "S",
                    Street = "6548 La Posada Ct.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78736",
                    PhoneNumber = "5124678821",
                    Birthday = DateTime.Parse("4/12/84"),
                    UserName = "bradsingram@mall.utexas.edu",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "mother");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("bradsingram@mall.utexas.edu");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "mother.Ingram@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "mother.Ingram@aool.com",
                    FName = "Todd",
                    LName = "Jacobs",
                    MiddleInitial = "L",
                    Street = "4564 Elm St.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78731",
                    PhoneNumber = "5124653365",
                    Birthday = DateTime.Parse("4/4/83"),
                    UserName = "mother.Ingram@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "whimsical");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("mother.Ingram@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "victoria@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "victoria@aool.com",
                    FName = "Victoria",
                    LName = "Lawrence",
                    MiddleInitial = "M",
                    Street = "6639 Butterfly Ln.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78761",
                    PhoneNumber = "5129457399",
                    Birthday = DateTime.Parse("2/3/61"),
                    UserName = "victoria@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "nothing");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("victoria@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "lineback@flush.net"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "lineback@flush.net",
                    FName = "Erik",
                    LName = "Lineback",
                    MiddleInitial = "W",
                    Street = "1300 Netherland St",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78293",
                    PhoneNumber = "2102449976",
                    Birthday = DateTime.Parse("9/3/46"),
                    UserName = "lineback@flush.net",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "GoodFellow");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("lineback@flush.net");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "elowe@netscrape.net"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "elowe@netscrape.net",
                    FName = "Ernest",
                    LName = "Lowe",
                    MiddleInitial = "S",
                    Street = "3201 Pine Drive",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78279",
                    PhoneNumber = "2105344627",
                    Birthday = DateTime.Parse("2/7/92"),
                    UserName = "elowe@netscrape.net",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "impede");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("elowe@netscrape.net");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "luce_chuck@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "luce_chuck@ggmail.com",
                    FName = "Chuck",
                    LName = "Luce",
                    MiddleInitial = "B",
                    Street = "2345 Rolling Clouds",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78268",
                    PhoneNumber = "2106983548",
                    Birthday = DateTime.Parse("10/25/42"),
                    UserName = "luce_chuck@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "LuceyDucey");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("luce_chuck@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "mackcloud@pimpdaddy.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "mackcloud@pimpdaddy.com",
                    FName = "Jennifer",
                    LName = "MacLeod",
                    MiddleInitial = "D",
                    Street = "2504 Far West Blvd.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78731",
                    PhoneNumber = "5124748138",
                    Birthday = DateTime.Parse("8/6/65"),
                    UserName = "mackcloud@pimpdaddy.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "cloudyday");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("mackcloud@pimpdaddy.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "liz@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "liz@ggmail.com",
                    FName = "Elizabeth",
                    LName = "Markham",
                    MiddleInitial = "P",
                    Street = "7861 Chevy Chase",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78732",
                    PhoneNumber = "5124579845",
                    Birthday = DateTime.Parse("4/13/59"),
                    UserName = "liz@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "emarkbark");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("liz@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "mclarence@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "mclarence@aool.com",
                    FName = "Clarence",
                    LName = "Martin",
                    MiddleInitial = "A",
                    Street = "87 Alcedo St.",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77045",
                    PhoneNumber = "8174955201",
                    Birthday = DateTime.Parse("1/6/90"),
                    UserName = "mclarence@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "smartinmartin");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("mclarence@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "smartinmartin.Martin@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "smartinmartin.Martin@aool.com",
                    FName = "Gregory",
                    LName = "Martinez",
                    MiddleInitial = "R",
                    Street = "8295 Sunset Blvd.",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77030",
                    PhoneNumber = "8178746718",
                    Birthday = DateTime.Parse("10/9/87"),
                    UserName = "smartinmartin.Martin@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "looter");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("smartinmartin.Martin@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "cmiller@mapster.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "cmiller@mapster.com",
                    FName = "Charles",
                    LName = "Miller",
                    MiddleInitial = "R",
                    Street = "8962 Main St.",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77031",
                    PhoneNumber = "8177458615",
                    Birthday = DateTime.Parse("7/21/84"),
                    UserName = "cmiller@mapster.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "chucky33");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("cmiller@mapster.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "nelson.Kelly@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "nelson.Kelly@aool.com",
                    FName = "Kelly",
                    LName = "Nelson",
                    MiddleInitial = "T",
                    Street = "2601 Red River",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78703",
                    PhoneNumber = "5122926966",
                    Birthday = DateTime.Parse("7/4/56"),
                    UserName = "nelson.Kelly@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "orange");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("nelson.Kelly@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "jojoe@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "jojoe@ggmail.com",
                    FName = "Joe",
                    LName = "Nguyen",
                    MiddleInitial = "C",
                    Street = "1249 4th SW St.",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75238",
                    PhoneNumber = "2143125897",
                    Birthday = DateTime.Parse("1/29/63"),
                    UserName = "jojoe@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "victorious");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("jojoe@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "orielly@foxnets.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "orielly@foxnets.com",
                    FName = "Bill",
                    LName = "O'Reilly",
                    MiddleInitial = "T",
                    Street = "8800 Gringo Drive",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78260",
                    PhoneNumber = "2103450925",
                    Birthday = DateTime.Parse("1/7/83"),
                    UserName = "orielly@foxnets.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "billyboy");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("orielly@foxnets.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "or@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "or@aool.com",
                    FName = "Anka",
                    LName = "Radkovich",
                    MiddleInitial = "L",
                    Street = "1300 Elliott Pl",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75260",
                    PhoneNumber = "2142345566",
                    Birthday = DateTime.Parse("3/31/80"),
                    UserName = "or@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "radicalone");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("or@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "megrhodes@freezing.co.uk"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "megrhodes@freezing.co.uk",
                    FName = "Megan",
                    LName = "Rhodes",
                    MiddleInitial = "C",
                    Street = "4587 Enfield Rd.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78707",
                    PhoneNumber = "5123744746",
                    Birthday = DateTime.Parse("8/12/44"),
                    UserName = "megrhodes@freezing.co.uk",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "gohorns");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("megrhodes@freezing.co.uk");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "erynrice@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "erynrice@aool.com",
                    FName = "Eryn",
                    LName = "Rice",
                    MiddleInitial = "M",
                    Street = "3405 Rio Grande",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5123876657",
                    Birthday = DateTime.Parse("8/2/34"),
                    UserName = "erynrice@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "iloveme");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("erynrice@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "jorge@hootmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "jorge@hootmail.com",
                    FName = "Jorge",
                    LName = "Rodriguez",
                    MiddleInitial = "nan",
                    Street = "6788 Cotter Street",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77057",
                    PhoneNumber = "8178904374",
                    Birthday = DateTime.Parse("8/11/89"),
                    UserName = "jorge@hootmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "greedy");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("jorge@hootmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "ra@aoo.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "ra@aoo.com",
                    FName = "Allen",
                    LName = "Rogers",
                    MiddleInitial = "B",
                    Street = "4965 Oak Hill",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78732",
                    PhoneNumber = "5128752943",
                    Birthday = DateTime.Parse("8/27/67"),
                    UserName = "ra@aoo.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "familiar");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("ra@aoo.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            
            
            if (!context.Users.Any(u => u.Email == "ss34@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "ss34@ggmail.com",
                    FName = "Sarah",
                    LName = "Saunders",
                    MiddleInitial = "J",
                    Street = "332 Avenue C",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5123497810",
                    Birthday = DateTime.Parse("10/29/77"),
                    UserName = "ss34@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "guiltless");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("ss34@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "willsheff@email.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "willsheff@email.com",
                    FName = "William",
                    LName = "Sewell",
                    MiddleInitial = "T",
                    Street = "2365 51st St.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78709",
                    PhoneNumber = "5124510084",
                    Birthday = DateTime.Parse("4/21/41"),
                    UserName = "willsheff@email.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "frequent");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("willsheff@email.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "sheff44@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "sheff44@ggmail.com",
                    FName = "Martin",
                    LName = "Sheffield",
                    MiddleInitial = "J",
                    Street = "3886 Avenue A",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5125479167",
                    Birthday = DateTime.Parse("11/10/37"),
                    UserName = "sheff44@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "history");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("sheff44@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "johnsmith187@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "johnsmith187@aool.com",
                    FName = "John",
                    LName = "Smith",
                    MiddleInitial = "A",
                    Street = "23 Hidden Forge Dr.",
                    City = "San Antonio",
                    State =  "TX",
                    Zip = "78280",
                    PhoneNumber = "2108321888",
                    Birthday = DateTime.Parse("10/26/54"),
                    UserName = "johnsmith187@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "squirrel");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("johnsmith187@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "dustroud@mail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "dustroud@mail.com",
                    FName = "Dustin",
                    LName = "Stroud",
                    MiddleInitial = "P",
                    Street = "1212 Rita Rd",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75221",
                    PhoneNumber = "2142346667",
                    Birthday = DateTime.Parse("9/1/32"),
                    UserName = "dustroud@mail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "snakes");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("dustroud@mail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "ericstuart@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "ericstuart@aool.com",
                    FName = "Eric",
                    LName = "Stuart",
                    MiddleInitial = "D",
                    Street = "5576 Toro Ring",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78746",
                    PhoneNumber = "5128178335",
                    Birthday = DateTime.Parse("12/28/30"),
                    UserName = "ericstuart@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "landus");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("ericstuart@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "peterstump@hootmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "peterstump@hootmail.com",
                    FName = "Peter",
                    LName = "Stump",
                    MiddleInitial = "L",
                    Street = "1300 Kellen Circle",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77018",
                    PhoneNumber = "8174560903",
                    Birthday = DateTime.Parse("8/13/89"),
                    UserName = "peterstump@hootmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "rhythm");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("peterstump@hootmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "tanner@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "tanner@ggmail.com",
                    FName = "Jeremy",
                    LName = "Tanner",
                    MiddleInitial = "S",
                    Street = "4347 Almstead",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77044",
                    PhoneNumber = "8174590929",
                    Birthday = DateTime.Parse("5/21/82"),
                    UserName = "tanner@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "kindly");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("tanner@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "taylordjay@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "taylordjay@aool.com",
                    FName = "Allison",
                    LName = "Taylor",
                    MiddleInitial = "R",
                    Street = "467 Nueces St.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5124748452",
                    Birthday = DateTime.Parse("1/8/60"),
                    UserName = "taylordjay@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "instrument");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("taylordjay@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "TayTaylor@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "TayTaylor@aool.com",
                    FName = "Rachel",
                    LName = "Taylor",
                    MiddleInitial = "K",
                    Street = "345 Longview Dr.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78705",
                    PhoneNumber = "5124512631",
                    Birthday = DateTime.Parse("7/27/75"),
                    UserName = "TayTaylor@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "arched");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("TayTaylor@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "teefrank@hootmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "teefrank@hootmail.com",
                    FName = "Frank",
                    LName = "Tee",
                    MiddleInitial = "J",
                    Street = "5590 Lavell Dr",
                    City = "Houston",
                    State =  "TX",
                    Zip = "77004",
                    PhoneNumber = "8178765543",
                    Birthday = DateTime.Parse("4/6/68"),
                    UserName = "teefrank@hootmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "median");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("teefrank@hootmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "tuck33@ggmail.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "tuck33@ggmail.com",
                    FName = "Clent",
                    LName = "Tucker",
                    MiddleInitial = "J",
                    Street = "312 Main St.",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75315",
                    PhoneNumber = "2148471154",
                    Birthday = DateTime.Parse("5/19/78"),
                    UserName = "tuck33@ggmail.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "approval");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("tuck33@ggmail.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "avelasco@yaho.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "avelasco@yaho.com",
                    FName = "Allen",
                    LName = "Velasco",
                    MiddleInitial = "G",
                    Street = "679 W. 4th",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75207",
                    PhoneNumber = "2143985638",
                    Birthday = DateTime.Parse("10/6/63"),
                    UserName = "avelasco@yaho.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "decorate");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("avelasco@yaho.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "westj@pioneer.net"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "westj@pioneer.net",
                    FName = "Jake",
                    LName = "West",
                    MiddleInitial = "T",
                    Street = "RR 3287",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75323",
                    PhoneNumber = "2148475244",
                    Birthday = DateTime.Parse("10/14/93"),
                    UserName = "westj@pioneer.net",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "grover");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("westj@pioneer.net");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "louielouie@aool.com"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "louielouie@aool.com",
                    FName = "Louis",
                    LName = "Winthorpe",
                    MiddleInitial = "L",
                    Street = "2500 Padre Blvd",
                    City = "Dallas",
                    State =  "TX",
                    Zip = "75220",
                    PhoneNumber = "2145650098",
                    Birthday = DateTime.Parse("5/31/52"),
                    UserName = "louielouie@aool.com",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "sturdy");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("louielouie@aool.com");
                manager.AddToRole(userToAdd.Id, "Customer");
            }
            if (!context.Users.Any(u => u.Email == "rwood@voyager.net"))
            {
                var store = new UserStore<AppUser>(context);
                var manager = new UserManager<AppUser>(store);
                AppUser user = new AppUser()
                {
                    Email = "rwood@voyager.net",
                    FName = "Reagan",
                    LName = "Wood",
                    MiddleInitial = "B",
                    Street = "447 Westlake Dr.",
                    City = "Austin",
                    State =  "TX",
                    Zip = "78746",
                    PhoneNumber = "5124545242",
                    Birthday = DateTime.Parse("4/24/92"),
                    UserName = "rwood@voyager.net",
                    UserType = UserTypeEnum.Customer,

                };
                manager.Create(user, "decorous");
                context.SaveChanges();
                AppUser userToAdd = manager.FindByEmail("rwood@voyager.net");
                manager.AddToRole(userToAdd.Id, "Customer");
                


            }
            
            context.SaveChanges();

        }
    }
}