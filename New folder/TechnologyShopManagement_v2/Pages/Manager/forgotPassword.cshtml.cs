using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class forgotPasswordModel : PageModel
    {
        private readonly Technology_ManagementContext dbContext;
        public forgotPasswordModel(Technology_ManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public Account Account { get; set; }
        public async Task<IActionResult> OnPost()
        {
            var acc = new Account();
            if (Regex.IsMatch(Account.Email, "^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$"))
            {
             acc = await dbContext.Accounts.SingleOrDefaultAsync(a => a.Username.Equals(Account.Username) && a.Email.Equals(Account.Email));
            }

            if (acc == null)
            {
                ViewData["msg"] = "Email/ User is wrong";
                return Page();
            }
            else
            {
                string newPass = randomPass();
                acc.Password= newPass;
                 dbContext.Accounts.Update(acc);
                if (dbContext.SaveChanges()>0)
                {
                    ViewData["msg"] = "Validate Success, Please check password on your email !";
                    SendEmail(acc.Email,newPass);
                    return Page();
                }
                else
                {
                    ViewData["msg"] = "Validate Fail, Please Comback Later !";
                    return Page();
                }  
                 
                 
            }
        }

        public string randomPass()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

            // Initialize a random number generator
            Random rnd = new Random();

            // Generate a random sequence of 8 characters
            string result = new string(Enumerable.Range(0, 8)
                .Select(x => chars[rnd.Next(chars.Length)])
                .ToArray());

            return result;
        }
        public void SendEmail(string email,string password)
        {
            // email recieve
            string toEmail = email;

            // email from
            string fromEmail = "longnbha150108@fpt.edu.vn";

            //  MailMessage object
            MailMessage message = new MailMessage(fromEmail, toEmail);
            message.Subject = "FORGOT PASSWORD !!!";
            message.Body = "Hi there, this is your new password: "+ password + ", Please don't share it with anyone else !!! Best Regard !";

            // create SmtpClient object
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("longnbha150108@fpt.edu.vn", "Louis1112");
            smtpClient.EnableSsl = true;
          ;
            // send email
            smtpClient.Send(message);
        }


    }
}
