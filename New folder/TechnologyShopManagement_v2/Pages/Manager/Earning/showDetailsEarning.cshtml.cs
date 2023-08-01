using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Earning
{
    public class showDetailsEarningModel : PageModel
    {
        static int ordID = 0;
        private readonly Technology_ManagementContext _db;
        public showDetailsEarningModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public Account account { get; set; }
        public IEnumerable<OrderDetail> orders { get; set; }

        public Order ord { get; set; }
        public void OnGet(int orderId)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));


            orders = _db.OrderDetails.Include(s => s.Product)
                    .Where(s => s.OrderId == orderId);
            ord = _db.Orders.FirstOrDefault(s => s.Id == orderId);
        }
    }
}
