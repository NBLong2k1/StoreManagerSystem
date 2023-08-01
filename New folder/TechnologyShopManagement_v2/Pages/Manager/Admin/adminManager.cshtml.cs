using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Admin
{
    public class adminManagerModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public adminManagerModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public IEnumerable<Order> orders { get; set; }
        public Account account { get; set; }
        //public Decimal totalYear { get; set; }
        //public Decimal totalMonthly { get; set; }
        //public Decimal totalDaily { get; set; }
        //public List<Decimal> totalEachMonth { get; set; }
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
            //totalDaily = getTotalDaily();
            //totalYear = getTotalYear();
            //totalMonthly = getTotalMonthly();
            //totalEachMonth = getListTotalEachMonth(); 
        }


        //public List<Decimal> getListTotalEachMonth()
        //{
        //    List<Decimal> totalEachMonth = new List<Decimal>();
        //    for (int i = 1; i <= 12; i++)
        //    {
        //        var total = from o in _db.Orders
        //                    where (o.DateRecipt ?? DateTime.MinValue).Month == i && o.Status == 2
        //                    select o;
        //        Decimal sum = 0;
        //        foreach (var o in total)
        //        {

        //            sum += (decimal)o.TotalPrice;


        //        }
        //        totalEachMonth.Add(sum);
        //    }
        //    return totalEachMonth;
        //}
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
        //public decimal getTotalMonthly()
        //{
        //    Decimal sum = 0;
        //    var thisMonth = DateTime.Now.Month;

        //    var total = from o in _db.Orders
        //                where (o.DateRecipt ?? DateTime.MinValue).Month == thisMonth && o.Status == 2
        //                select o;

        //    foreach (var o in total)
        //    {

        //        sum += (decimal)o.TotalPrice;


        //    }
        //    return sum;
        //}
        //public decimal getTotalYear()
        //{
        //    Decimal sum = 0;
        //    int getYear = DateTime.Now.Year;

        //    var total = from o in _db.Orders
        //                where (o.DateRecipt ?? DateTime.MinValue).Year == getYear && o.Status == 2
        //                select o;

        //    foreach (var o in total)
        //    {

        //        sum += (decimal)o.TotalPrice;


        //    }
        //    return sum;
        //}
    }
}
