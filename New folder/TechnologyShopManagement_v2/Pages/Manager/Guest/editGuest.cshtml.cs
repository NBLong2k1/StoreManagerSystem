using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Guest
{
    [BindProperties]
    public class editGuestModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        static int guestId = 0;
        private readonly Technology_ManagementContext _dbContext;
        public editGuestModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Account accountU { get; set; }
        public Account account { get; set; }
        public void OnGet(int userId)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _dbContext.Accounts.FirstOrDefault(s => s.Username.Equals(username));

            account = null;



            account = _dbContext.Accounts.Find(userId);

            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }

        }


        public async Task<IActionResult> OnPost(string guestStatus)
        {
            int status = 0;

            try
            {
                if (guestStatus == null)
                {
                    status = 0;
                }
                else
                {
                    status = 1;
                }
                Console.WriteLine("status is " + status);
                account.Status = status;
                account.Role = 3;
                _dbContext.Accounts.Update(account);
                await _dbContext.SaveChangesAsync();
                messError = "";
                return Redirect("/Manager/Guest/listGuest");

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Update Fail";
            guestId = account.UserId;
            return Redirect("/Manager/Guest/editGuest");


        }
    }
}
