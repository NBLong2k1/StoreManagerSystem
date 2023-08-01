using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLabs.Model;
using System.Security.Principal;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Customer
{
    public class customerHistoryListModel : PageModel
    {
        //public string CurrentFilter { get; set; }
        //public string CurrentSort { get; set; }
        //public PaginatedList<Order> orders { get; set; }

        private readonly IConfiguration Configuration;
        private readonly Technology_ManagementContext _db;
        public customerHistoryListModel(Technology_ManagementContext db, IConfiguration configuration)
        {
            _db = db;
            Configuration = configuration;
        }
        // public IEnumerable<Order> orders { get; set; }
        public Account account { get; set; }
        public async Task OnGetAsync(string sortOrder,
                 string currentFilter, string searchString, int? pageIndex)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));



            //CurrentSort = sortOrder;
            //if (searchString != null)
            //{
            //    pageIndex = 1;

            //}
            //else
            //{
            //    searchString = currentFilter;
            //}
            //CurrentFilter = searchString;

            //IQueryable<Order> orderIQ = _db.Orders
            //     .OrderByDescending(s => s.Status)
            //     .Include(s => s.Customer)
            //     .Where(s => s.Status > 1 && s.CustomerId==account.UserId);


            //if (!String.IsNullOrEmpty(searchString))
            //{
            //    var date = DateTime.Parse(searchString);
            //    int number = 0;
            //    bool isNumber = int.TryParse(searchString, out number);
            //    orderIQ = orderIQ
            //        .OrderByDescending(s => s.Status)
            //        .Where(s => 
            //        (s.DateOrder.Equals(date) || s.DateDelivery.Equals(date) || s.DateRecipt.Equals(date) ) 
            //        && s.Status > 1 && s.CustomerId == account.UserId);
            //}

            //var pageSize = Configuration.GetValue("PageSize", 4);
            //orders = await PaginatedList<Order>.CreateAsync(orderIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        }
    }
}
