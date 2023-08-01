using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Employee
{
    [BindProperties]
    public class editEmployeeModel : PageModel
    {
        static string currentEmail = "";
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        private readonly Technology_ManagementContext _db;
        public editEmployeeModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        static int usID = 0;
        public Account accountss { get; set; }
        public Account account { get; set; }
        public void OnGet(string userId)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));

            if (userId!=null)
            {
                usID = Int32.Parse(userId);
            }
            

            accountss = new Account();
            accountss =  _db.Accounts.Find(usID);
            currentEmail = accountss.Email;
            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }
             
        }

        public async Task<IActionResult> OnPost(string accountStatus)
        {
            int status = 0;
            if (accountStatus == null)
            {
                status = 0;
            }
            else
            {
                status = 1;
            }


            bool checkDup = false;
            Technology_ManagementContext myDb = new Technology_ManagementContext();
            List<Account> accList = _db.Accounts.ToList();

            foreach (var i in accList)
            {
                if (i.Email.Equals(accountss.Email))
                {
                    checkDup = true;
                    break;
                }
            }

            if (checkDup == true)
            {
                if (accountss.Email.Equals(currentEmail))
                {
                    accountss.Email = accountss.Email;
                    accountss.Status = status;
                    accountss.Role = 1;
                    myDb.Accounts.Update(accountss);
                    await myDb.SaveChangesAsync();
                    messError = "";
                    return Redirect("/Manager/Employee/listEmployee");
                }
                messError = "Email is already exist !!!";
                usID = accountss.UserId;
                return Redirect("/Manager/Employee/editEmployee");
            }
            else
            {
                Console.WriteLine("is false");

                accountss.Email = accountss.Email;
                accountss.Status = status;
                accountss.Role = 1;
                myDb.Accounts.Update(accountss);
                await myDb.SaveChangesAsync();
                messError = "";
                return Redirect("/Manager/Employee/listEmployee");
            }





        }

        
    }
}
