using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager.Customer
{
    [BindProperties]
    public class customerManagerModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public customerManagerModel(Technology_ManagementContext db)
        {
            _db = db;
        }

        public Account account { get; set; }
        public List<(Product, int proQuantity)>? ProductsListCart { get; set; }
        public List<Cart> cartList { get; set; }
        public IEnumerable<Order> orders { get; set; }
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            account = new Account();
            account = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));
            orders = _db.Orders
                      .OrderByDescending(o => o.DateRecipt)
                      .Where(s => s.CustomerId == account.UserId && s.Status >= 2)
                      .Take(5);


            ProductsListCart = new List<(Product, int proQuantity)>();


            cartList = (from cart in _db.Carts
                        where cart.AccountId == account.UserId
                        select cart).ToList();



            Product product = new Product();
            int quantity = 2;
            foreach (var i in cartList)
            {
                quantity = (int)i.Quantity;
                ProductsListCart.Add((_db.Products.Find(i.ProductId), quantity));
            }

        }
    }
}
