using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Principal;
using TechnologyShopManagement_v2.Models.Entities;
using static NuGet.Packaging.PackagingConstants;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    [BindProperties]
    public class checkoutShoppingCartModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        public const string SessionNumber = "numberCart";
        private readonly Technology_ManagementContext _dbContext;
        public List<(Product, int proQuantity)>? ProductsListCart { get; set; }
        public checkoutShoppingCartModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
           
        }
        public Account account { get; set; }

        public Order order { get; set; }
        public OrderDetail orderDetail { get; set; }
        public IEnumerable<Account> accountList { get; set; }

        public void OnGet()

        {
            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }

            //get cookie
            var cookie = Request.Cookies["myCard"];

            //create new list 
            ProductsListCart = new List<(Product, int proQuantity)>();


            //if cookie not null
            if (cookie != null)
            {
                //clear all list cart
                ProductsListCart.Clear();


                //read cookie
                foreach (var item in ReadCookie())
                {
                    //Add product and quantity to list
                    ProductsListCart.Add((_dbContext.Products.Find(item.proId), item.proQuantity));
                }



            }


            List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();
            listP = getList();

            HttpContext.Session.SetInt32(SessionNumber, listP.Count());
        }

        public async Task<IActionResult> OnPost()
        {
             
            Console.WriteLine("hellooooooooooooooooooooooooooo");
            accountList = null;
            string username = "";
            bool check = false;
            try
            {
                accountList = _dbContext.Accounts;

                username = getUsernameForGuest(account.Name);
             
                int generateID = 0;
                string name = "";
            

                generateID = accountList.Count() + 1;
                username += generateID;



                //auto convert username password role for guest
                account.Username = username;
                account.Password = "123";
                account.Role = 3;


                await _dbContext.AddAsync(account);
                if (await _dbContext.SaveChangesAsync()>0) {
                    var userID = await _dbContext.Accounts.SingleOrDefaultAsync(s => s.Username.Equals(username));
                    int id = userID.UserId;
                    int code = 1;


                    while (_dbContext.Orders.SingleOrDefault(s => s.Code == code) != null)
                    {
                        code++;
                    }

                    //Order infor
                    order.CustomerId = id;
                    order.CustomerName = account.Name;
                    order.Staff = null;
                    order.Address = account.Address;
                    order.PhoneNumber = account.PhoneNumber;
                    order.Discount = 0;
                    order.DateOrder = DateTime.Now;
                    order.DateDelivery = null;
                    order.DateRecipt = null;
                    order.Status = 0;
                    order.Code = code;
                    await _dbContext.AddAsync(order);
                    if (await _dbContext.SaveChangesAsync() > 0)
                    {


                        //errorrrrrrrrrrr
                        List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();
                        listP = getList();
                        var orderid = await _dbContext.Orders.SingleOrDefaultAsync(s => s.CustomerId == id);

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


                        if (await _dbContext.SaveChangesAsync()>0)
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




                            //Expire cookie
                            CookieOptions options = new CookieOptions();
                            options.Expires = DateTime.Now.AddSeconds(0);
                            Response.Cookies.Append("myCard", "", options);

                            return Redirect("/HomePage/successCheckout");
                        }
                        else
                        {
                            messError = "Fail To Checkout, please return later";
                            return Redirect("/HomePage/checkoutShoppingCart");
                        }






                    }

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }

         return Redirect("/HomePage/successCheckout");
        }


        public string getUsernameForGuest(string fullName)
        {
           
            string username = "";
            string[] splitName = fullName.Split(' ');

            username = splitName[splitName.Length - 1];
            return username;

        }

        public List<(int proId, int proQuantity)> getList()
        {
            List<(int proId, int proQuantity)> list = new List<(int proId, int proQuantity)>();
            var cookie = Request.Cookies["myCard"];
            //empty string to append new cookie
            string newCookie = "";


            if (cookie != null)
            {

                //list to split by ' AND '
                string[] items = cookie.Split("AND");


                foreach (string item in items)
                {
                    //list to split by ' - '
                    string[] pair = item.Split("-");
                    try
                    {
                        //add to list with Key and Value
                        list.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error " + ex);
                    }
                }
            }

            return list;

        }
        public void appenCookie(int id, int quantity)
        {

            CookieOptions options = new CookieOptions();

            //get cookie 
            var cookie = Request.Cookies["myCard"];


            //if cookie is null
            if (cookie == null)

            {   //set Key
                string key = "myCard";

                //set value
                string value = id.ToString() + "-" + quantity.ToString();

                //set time expire
                options.Expires = DateTime.Now.AddSeconds(1000);

                //apend cookie
                Response.Cookies.Append(key, value, options);
            }

            //if not null
            else
            {
                //create list to store id and quantity
                var mylist = new List<(int proId, int proQuantity)>();

                //empty string to append new cookie
                string newCookie = "";

                //list to split by ' AND '
                string[] items = cookie.Split("AND");


                foreach (string item in items)
                {
                    //list to split by ' - '
                    string[] pair = item.Split("-");
                    try
                    {
                        //add to list with Key and Value
                        mylist.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error " + ex);
                    }
                }


                //check duplicate id
                bool checkDuplicate = false;

                foreach (var item in mylist)
                {
                    //if id already exist in list
                    if (item.proId == id)
                    {
                        //return true
                        checkDuplicate = true;
                        break;
                    }
                }

                //if product already exist in cookie
                if (checkDuplicate)
                {
                    for (int i = 0; i < mylist.Count; i++)
                    {
                        //if id exist in list
                        if (mylist[i].proId == id)
                        {
                            //update product in list
                            mylist[i] = (mylist[i].proId, mylist[i].proQuantity + quantity);
                            break;
                        }

                    }

                    foreach (var item in mylist)
                    {
                        //append new cookie
                        newCookie += "AND" + item.proId + "-" + item.proQuantity;
                    }

                    if (newCookie.Substring(0, 3).Equals("AND"))
                    {
                        //substring if string begin with 'AND'
                        newCookie = newCookie.Substring(3, newCookie.Length - 3);

                    }

                    //delete current cookie
                    // Response.Cookies.Delete(cookie);

                    //add new cookie with new string
                    options.Expires = DateTime.Now.AddSeconds(1000);
                    Response.Cookies.Append("myCard", newCookie, options);


                }

                //if product not exist in cookie
                else
                {

                    //append next to string
                    cookie += "AND" + id.ToString() + "-" + quantity.ToString();
                    options.Expires = DateTime.Now.AddSeconds(1000);
                    Response.Cookies.Append("myCard", cookie, options);
                }



            }


        }

        public List<(int proId, int proQuantity)> ReadCookie()
        {
            //create list
            var myProcductCart = new List<(int proId, int proQuantity)>();

            //get cookie
            var cookie = Request.Cookies["myCard"];
            if (cookie != null)
            {
                //create list split by ' AND '
                string[] items = splitString(cookie, "AND");

                foreach (string item in items)
                {
                    //create list split by ' - ' 
                    string[] pair = splitString(item, "-");

                    try
                    {
                        //add to list with Key and Value
                        myProcductCart.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error: " + ex);
                    }


                }

            }
            return myProcductCart;
        }

        public string[] splitString(string value, string charac)
        {
            string[] result = value.Split(charac);

            return result;
        }
    }
}
