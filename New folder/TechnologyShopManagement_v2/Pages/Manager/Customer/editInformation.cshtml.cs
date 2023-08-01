using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Customer
{
    [BindProperties]
    public class editInformationModel : PageModel
    {
        public static string currentEmail { get; set; }
        public const string errorMessage = "errorMessage";
        public static string messError = "";
        private readonly Technology_ManagementContext _db;
        public editInformationModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public Account account { get; set; }

     
        public void OnGet()
        {
           
            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }


           
                string username = HttpContext.Session.GetString("isLogin").ToString();
                account = new Account();
                account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
                currentEmail = account.Email;
            
           
            
             
        }

        public async Task <IActionResult> OnPost()
        {
            bool checkDup = false;
            Technology_ManagementContext myDb= new Technology_ManagementContext();
            List<Account> accList = _db.Accounts.ToList();

            foreach(var i in accList)
            {
                if (i.Email.Equals(account.Email))
                {
                    checkDup = true;
                    break;
                }
            }
           
            if (checkDup==true) {
                if (account.Email.Equals(currentEmail))
                {
                    account.Email = account.Email;
                    account.Status = 1;
                    account.Role = 0;
                    myDb.Accounts.Update(account);
                    await myDb.SaveChangesAsync();
                    messError = "";
                    return Redirect("/Manager/Customer/customerManager");
                }
                messError = "Email is already exist !!!";
                return Redirect("/Manager/Customer/editInformation");
            }
            else
            {
                Console.WriteLine("is false");

                account.Email = account.Email;
                account.Status = 1;
                account.Role = 0;
                myDb.Accounts.Update(account);
               await myDb.SaveChangesAsync();
                messError = "";
                return Redirect("/Manager/Customer/customerManager");
            }

          

        }

      
    }
}
