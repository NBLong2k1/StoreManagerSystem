using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;
using static NuGet.Packaging.PackagingConstants;

namespace TechnologyShopManagement_v2.Pages.Shared
{
    public class testTotalEachMonthModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public testTotalEachMonthModel(Technology_ManagementContext db)
        {
            _db = db;
        }

        public List<Order> lstOrder { get; set; }


        public void OnGet()
        {

            ///////// Each Month
            //List<Decimal> totalEachMonth = new List<Decimal>();
            //for (int i = 1; i <= 12; i++)
            //{
            //    var total = from o in _db.Orders
            //                where (o.DateRecipt ?? DateTime.MinValue).Month == i && o.Status == 2
            //                select o;
            //    Decimal sum = 0;
            //    foreach (var o in total)
            //    {

            //        sum += (decimal)o.TotalPrice;


            //    }
            //    totalEachMonth.Add(sum);
            //}

            //int c = 1;
            //foreach (var sum in totalEachMonth)
            //{
            //    Console.WriteLine("Month " + c + " is: " + sum);
            //    c++;
            //}

            /////// End Each Month//////////////


            /////// Each day

            //var today = _db.Orders
            //               .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2);

            //Decimal sumToday = 0;
            //foreach (var order in today)
            //{
            //    sumToday += (Decimal)order.TotalPrice;

            //}
            ///////// End Each day//////////////







            ///////// Each year


            //int getYear=DateTime.Now.Year;

            //    var total = from o in _db.Orders
            //                where (o.DateRecipt ?? DateTime.MinValue).Year == getYear && o.Status == 2
            //                select o;
            //    Decimal sum = 0;
            //    foreach (var o in total)
            //    {

            //        sum += (decimal)o.TotalPrice;


            //    }

            ///////// End Each year//////////////





            /////total daily guest


            var today = _db.Orders
                           .Include(s=>s.Customer)
                           .Where(s => s.DateRecipt.Equals(DateTime.Today) && s.Status == 2 && s.Customer.Role==3);

            Decimal sumToday = 0;
            foreach (var order in today)
            {
                sumToday += (Decimal)order.TotalPrice;

            }

            Console.WriteLine("today guest is: "+sumToday);



            ///// end   total daily guest
        }
    }
}
