using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RazorPagesLabs.Model;
using System.Configuration;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    public class listProcessOrderModel : PageModel
    {
        public Account accountU { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Order> orders { get; set; }

        private readonly IConfiguration Configuration;
        private readonly Technology_ManagementContext _db;
        public listProcessOrderModel(Technology_ManagementContext db,IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }
       // public IEnumerable<Order> orders { get; set; }
        public async Task OnGetAsync(string sortOrder,
                 string currentFilter, string searchString, int? pageIndex)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));

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

            IQueryable<Order> orderIQ = _db.Orders
                .OrderBy(s => s.Status)
                .Include(s => s.Customer)
                .Where(s => s.Status <= 1 );


            if (!String.IsNullOrEmpty(searchString))
            {
                int number = 0;
                bool isNumber = int.TryParse(searchString, out number);
                orderIQ = orderIQ.OrderBy(s => s.Status).Where(s => (s.CustomerName.Contains(searchString) || s.Id == number || s.PhoneNumber.Contains(searchString)) && s.Status <= 1);
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            orders = await PaginatedList<Order>.CreateAsync(orderIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        }


    }
}
