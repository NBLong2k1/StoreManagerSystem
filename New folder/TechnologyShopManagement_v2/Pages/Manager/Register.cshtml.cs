using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Text.RegularExpressions;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        private readonly Technology_ManagementContext dbContext;
        public RegisterModel(Technology_ManagementContext dbContext)
        {
            this.dbContext = dbContext;
        }

   
        public Account Account { get; set; }

        public async Task<IActionResult> OnPost(string rePass)
        {
            //foreach (var key in ModelState.Keys)
            //{
            //    if (ModelState[key].Errors.Any())
            //    {
            //        var errorMsg = ModelState[key].Errors.First().ErrorMessage;
            //        // handle error message for the key
            //    }
            //}
            // check modal valid
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //check phone fomat
            if (!Regex.IsMatch(Account.PhoneNumber, "^[\\+]?[(]?[0-9]{3}[)]?[-\\s\\.]?[0-9]{3}[-\\s\\.]?[0-9]{4,6}$"))
            {
                ViewData["msg"] = "Phone Number is not correct";
                return Page();
            }
            //check email fomat
            var acc = new Account();
            var username = new Account();
            if (Regex.IsMatch(Account.Email, "^([a-zA-Z0-9_\\-\\.]+)@([a-zA-Z0-9_\\-\\.]+)\\.([a-zA-Z]{2,5})$"))
            {
                try
                {

               
                username = await dbContext.Accounts.SingleOrDefaultAsync(a => a.Username.Equals(Account.Username));
                acc = await dbContext.Accounts.SingleOrDefaultAsync(a => a.Email.Equals(Account.Email));
                }
                catch (Exception ex)
                {
                    ViewData["msg"] = "Email have been used";
                    return Page();
                }
                
                if (acc != null)
                {

                    ViewData["msg"] = "Email have been used";
                    return Page();
                }
                if (username != null)
                {

                    ViewData["msg"] = "Username have been used";
                    return Page();
                }
            }
            else
            {
                ViewData["msg"] = "Email is not correct format";
                return Page();
            }

            if (!Account.Password.Equals(rePass))
            {
                ViewData["msg"] = "Password and Re-Enter Password not matches";
                return Page();
            }

            //HttpContext.Session.SetString("CustSession", JsonSerializer.Serialize(acc));
            //HttpContext.Session.SetInt32("CustRoleId", (int)acc.Role);



            Account.Role = 0;
            Account.Status = 1;
            await dbContext.Accounts.AddAsync(Account);
            await dbContext.SaveChangesAsync();

            return RedirectToPage("/Manager/Login");
        }

    }
}
