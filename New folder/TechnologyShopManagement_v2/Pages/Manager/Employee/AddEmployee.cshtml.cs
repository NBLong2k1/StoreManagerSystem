using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Employee
{
    [BindProperties]
    public class AddEmployeeModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        private readonly Technology_ManagementContext _db;
        public AddEmployeeModel(Technology_ManagementContext db)
        {
            _db = db;
        }

        public Account accountss { get; set; }
        public Account account { get; set; }
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }
        }
        public async Task<IActionResult> OnPost()
        {
            try
            {
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
                    if (i.Username.Equals(accountss.Username))
                    {
                        checkDup = true;
                        break;
                    }
                }

                if (checkDup == true)
                {
                    messError = "Email or username is already exist !!!";
                    return Redirect("/Manager/Employee/AddEmployee");

                }
                else
                {
                    accountss.Status = 1;
                    accountss.Role = 1;
                    await _db.AddAsync(accountss);
                    await _db.SaveChangesAsync();
                    messError = "";
                    return Redirect("/Manager/Employee/listEmployee");
                }


                     

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Add Fail";
            return Redirect("/Manager/Employee/AddEmployee");


        }
    }
}
