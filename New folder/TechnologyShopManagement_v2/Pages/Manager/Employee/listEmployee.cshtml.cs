using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLabs.Model;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Employee
{
    public class listEmployeeModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        private readonly IConfiguration Configuration;
        public listEmployeeModel(Technology_ManagementContext db, IConfiguration configuration)
        {
            Configuration = configuration;
            _db = db;
        }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Account> accounts { get; set; }
        public Account account { get; set; }
        public async Task OnGetAsync(string sortOrder,
                 string currentFilter, string searchString, int? pageIndex)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
            CurrentSort = sortOrder;
            if (searchString != null)
            {
                pageIndex = 1;

            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Account> accountIQ = _db.Accounts.Where(s => s.Role == 1);


            if (!String.IsNullOrEmpty(searchString))
            {
                int number = 0;
                bool isNumber = int.TryParse(searchString, out number);
                accountIQ = accountIQ.Where(s => ((s.Name.Contains(searchString) || s.UserId == number || s.Username.Contains(searchString) || s.Email.Contains(searchString) || s.PhoneNumber.Contains(searchString)) && s.Role == 1));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            accounts = await PaginatedList<Account>.CreateAsync(accountIQ.AsNoTracking(), pageIndex ?? 1, pageSize);



        }
    }
}
