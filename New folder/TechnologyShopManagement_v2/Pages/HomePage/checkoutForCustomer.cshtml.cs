using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    [BindProperties]
    public class checkoutForCustomerModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";
        private readonly Technology_ManagementContext _dbContext;
        public checkoutForCustomerModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public Order order { get; set; }
        public List<Cart> cartList { get; set; }
        public async Task<IActionResult>  OnGet(decimal totalBill)
        {
            int code = 1;

          
            while(_dbContext.Orders.SingleOrDefault(s => s.Code == code) != null)
            {
                code++;
            }

            Console.WriteLine("code is " + code);
            string user = HttpContext.Session.GetString("isLogin");
            //if not login
            if (user != null)
            {
                Account acc = new Account();
                cartList = new List<Cart>();

                acc = await _dbContext.Accounts.FirstOrDefaultAsync(s => s.Username == user);
                cartList = (from cart in _dbContext.Carts
                            where cart.AccountId == acc.UserId
                            select cart).ToList();



                int id = acc.UserId;


                //Order infor
                order = new Order
                {
                    CustomerId = id,
                    CustomerName = acc.Name,
                    Staff = null,
                    Address = acc.Address,
                    PhoneNumber = acc.PhoneNumber,
                    Discount = 0,
                    DateOrder = DateTime.Now,
                    DateDelivery = null,
                    DateRecipt = null,
                    TotalPrice = totalBill,
                    Code=code,
                    Status=0
                };




                await _dbContext.AddAsync(order);
                if (_dbContext.SaveChanges() > 0)
                {


                    //errorrrrrrrrrrr
                    List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();

                    foreach (var i in cartList)
                    {
                        int proId = i.ProductId;
                        int proqQuantity = (int)i.Quantity;
                        listP.Add((proId, proqQuantity));
                    }

                    var orderid = await _dbContext.Orders.SingleOrDefaultAsync(s => s.Code == code);

                    int orID = orderid.Id;

                    foreach (var item in listP)
                    {
                        var proPrice = await _dbContext.Products.SingleOrDefaultAsync(s => s.Id == item.proId);

                        var orderDetail = new OrderDetail
                        {
                            OrderId = orID,
                            ProductId = item.proId,
                            Quantity = item.proQuantity,
                            Price = proPrice.Price,
                            TotalEachOrder = proPrice.Price * item.proQuantity

                        };

                        await _dbContext.OrderDetails.AddAsync(orderDetail);
                    }


                    if (await _dbContext.SaveChangesAsync() > 0)
                    {
                        Technology_ManagementContext myDb = new Technology_ManagementContext();
                        var orders = _dbContext.OrderDetails.Include(s => s.Product)
                .Where(s => s.OrderId == orID);
                        foreach (var q in orders)
                        {
                            var quantity = q.Quantity;
                            var product = _dbContext.Products.Find(q.ProductId);
                            var newProductStock = product.Stock - quantity;
                            product.Stock = newProductStock;
                            myDb.Update(product);
                            myDb.SaveChanges();

                        }

                        Cart cart;
                        foreach (var list in cartList)
                        {
                            cart = new Cart();
                            cart = list;
                            _dbContext.Carts.Remove(cart);
                        }

                        _dbContext.SaveChanges();

                        return Redirect("/HomePage/successCheckout");
                    }
                    else
                    {
                        messError = "Fail To Checkout, please return later";
                        HttpContext.Session.SetString(errorMessage, messError);
                        return Redirect("/HomePage/checkoutShoppingCart");
                    }


                }
            }
            return Redirect("/HomePage/successCheckout");

        }



    }
}
