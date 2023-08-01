using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    public class DetailsProcessOrderModel : PageModel
    {
        static int ordID = 0;
        private readonly Technology_ManagementContext _db;
        public DetailsProcessOrderModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public IEnumerable<OrderDetail> orders { get; set; }
        public Account accountU { get; set; }
        public Order ord { get; set; }
        public async void OnGet(int orderId, int valueStatus)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));


            Console.WriteLine("id issssssssssssssssssss " + orderId);
                orders = _db.OrderDetails.Include(s => s.Product)
                    .Where(s => s.OrderId == orderId);
            ord = _db.Orders.FirstOrDefault(s => s.Id == orderId);
            if (valueStatus!=0)
            {
                var Order = new Order();
                Order=_db.Orders.FirstOrDefault(s => s.Id==orderId);

               
                    Order.Status = valueStatus;
                
                 
                if (valueStatus==1)
                {
                    Order.DateDelivery = DateTime.Now;
                    



                }
                if (valueStatus == 2)
                {
                    if (Order.DateDelivery==null)
                    {
                        Order.DateDelivery = DateTime.Now;
                    }
                    Order.DateRecipt = DateTime.Now;
                }
                if (valueStatus == 3)
                {
                    if (Order.DateDelivery == null)
                    {
                        Order.DateDelivery = DateTime.Now;
                    }
                    
                    Order.DateRecipt = DateTime.Now;

                    foreach (var q in orders)
                    {
                        Technology_ManagementContext myDb = new Technology_ManagementContext();
                        var quantity = q.Quantity;
                        var product = _db.Products.Find(q.ProductId);
                        var newProductStock = product.Stock + quantity;
                        product.Stock = newProductStock;
                        myDb.Update(product);
                        myDb.SaveChanges();

                    }

                }




                _db.Orders.Update(Order);

                _db.SaveChanges();
                Response.Redirect("/Manager/listProcessOrder");
                Console.WriteLine("order issssssssssssssssssss " + orderId);
                Console.WriteLine("value issssssssssssssssssss " + valueStatus);

            }




        }
    }
}
