using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using RazorPagesLabs.Model;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text.Json;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Earning
{
    public class listEarningModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        private readonly IConfiguration Configuration;


      

        public listEarningModel(Technology_ManagementContext db, IConfiguration configuration)
        {
         

            Configuration = configuration;
            _db = db;
        }
        public Account account { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Order> orders { get; set; }
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





            IQueryable<Order> orderIQ = _db.Orders.Where(s => s.Status == 2);






            if (!String.IsNullOrEmpty(searchString))
            {
                var date = DateTime.Parse(searchString);

                int number = 0;
                bool isNumber = int.TryParse(searchString, out number);
                orderIQ = orderIQ.Where(s => ((s.DateOrder.Equals(date) || s.DateDelivery.Equals(date) || (s.DateRecipt.Equals(date)))  && s.Status == 2));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            orders = await PaginatedList<Order>.CreateAsync(orderIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        }
    }
}
