using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Employee
{
    public class employeeManagerModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public employeeManagerModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        //public Decimal totalDaily { get; set; }
        //public Decimal totalDailyGuest { get; set; }
        //public Decimal totalDailyCustomer { get; set; }
       // public IEnumerable<Order> orders { get; set; }
        public Account account { get; set; }
        public void OnGet()
        {
            //totalDaily = getTotalDaily();
            //totalDailyGuest = getTotalDailyGuest();
            //totalDailyCustomer = getTotalDailyCustomer();
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
            //orders = _db.Orders
            //        .OrderByDescending(o => o.Status)
            //        .Include(s => s.Customer)
            //        .Where(s => s.Status < 1)
            //        .Take(5);
        }


        //public decimal getTotalDaily()
        //{
        //    Decimal sumToday = 0;
        //    var today = _db.Orders
        //                   .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2);


        //    foreach (var order in today)
        //    {
        //        sumToday += (Decimal)order.TotalPrice;

        //    }

        //    return sumToday;
        //}
        //public decimal getTotalDailyGuest()
        //{
        //    Decimal sumToday = 0;
        //    var today = _db.Orders
        //                   .Include(s => s.Customer)
        //                   .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2 && s.Customer.Role == 3);

            
        //    foreach (var order in today)
        //    {
        //        sumToday += (Decimal)order.TotalPrice;

        //    }

        //    return sumToday;
        //}
        //public decimal getTotalDailyCustomer()
        //{
        //    Decimal sumToday = 0;
        //    var today = _db.Orders
        //                   .Include(s => s.Customer)
        //                   .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2 && s.Customer.Role == 0);

           
        //    foreach (var order in today)
        //    {
        //        sumToday += (Decimal)order.TotalPrice;

        //    }

        //    return sumToday;
        //}
    }
}
