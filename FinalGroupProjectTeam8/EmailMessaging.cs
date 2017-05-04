using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Net;

namespace FinalGroupProjectTeam8.Messaging
{
    public class EmailMessaging
    {
        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {

            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("mydummyemail@gmail.com", "Password123"),
                EnableSsl = true
            };

            //Add anything that you need to the body of the message
            // /n is a new line – this will add some white space after the main body of the message
            String finalMessage = emailBody + "\n\n This is a disclaimer that will be on all    messages. ";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress("mydummyemail@gmail.com", "Team 8");


            MailMessage mm = new MailMessage();
            mm.Subject = "Team 8 - " + emailSubject;
            mm.Sender = senderEmail;
            mm.From = senderEmail;
            mm.To.Add(new MailAddress(toEmailAddress));
            mm.Body = finalMessage;
            client.Send(mm);
        }

    }
}
