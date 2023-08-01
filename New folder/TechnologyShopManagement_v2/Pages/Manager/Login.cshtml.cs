using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    public class LoginModel : PageModel
    {
        public const string islogin = "isLogin";
      

        private readonly Technology_ManagementContext dbContext;
        public LoginModel(Technology_ManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Account Account { get; set; }
        public async Task<IActionResult> OnPost()
        {

            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var acc = new Account();
            if (Regex.IsMatch(Account.Username, "^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$"))
            {
                acc = await dbContext.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(Account.Username) && a.Password.Equals(Account.Password) && a.Role!=3);
            }
            else
            {
                acc = await dbContext.Accounts.SingleOrDefaultAsync(a => a.Username.Equals(Account.Username) && a.Password==Account.Password && a.Role != 3);
             
            }

            if (acc == null)
            {
                ViewData["msg"] = "Email/ Password is wrong or not allow to access system";
                return Page();
            }

            //HttpContext.Session.SetString("CustSession", JsonSerializer.Serialize(acc));
            //HttpContext.Session.SetInt32("CustRoleId", (int)acc.Role);

            //Admin
            if (acc.Role ==2 )
            {
                HttpContext.Session.SetString(islogin, acc.Username);
                return RedirectToPage("/Manager/Admin/adminManager");
            }
            //Staff
            else if (acc.Role ==1)
            {
                HttpContext.Session.SetString(islogin, acc.Username);
                return RedirectToPage("/Manager/Employee/employeeManager");
            }
            //Customer
            else if (acc.Role == 0)
            {
                if (acc.Status==0 || acc.Role==3)
                {
                    ViewData["msg"] = "Your account is not allowed to access the system";
                    return Page();

                }




                HttpContext.Session.SetString(islogin, acc.Username);
                HttpContext.Session.SetString("accountId", acc.UserId.ToString());

                Console.WriteLine("success for " + acc.Username);
                return RedirectToPage("/HomePage/Home");
            }

            return RedirectToPage("/index");
        }

    }
}
