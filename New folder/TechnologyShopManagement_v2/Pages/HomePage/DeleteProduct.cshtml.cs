using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    public class DeleteProductModel : PageModel
    {
        public const string SessionNumber = "numberCart";
        private readonly Technology_ManagementContext _dbContext;
        public DeleteProductModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Cart> cartList { get; set; } 
        public Cart cart { get; set; }
        public async Task<IActionResult> OnGet(int proId)
        {
            string user = HttpContext.Session.GetString("isLogin");
            var listP = new List<(int proId, int proQuantity)>();

            if (user != null)
            {
                Console.WriteLine("HEHEHEHEHEHEHEHEHEHEEeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee");
                Account acc = new Account();
                acc = _dbContext.Accounts.FirstOrDefault(s => s.Username == user);
                cartList = from s in _dbContext.Carts
                           where s.AccountId == acc.UserId
                           select s;
                cart = new Cart();
                cart = (from s in _dbContext.Carts
                        where s.ProductId == proId && s.AccountId == acc.UserId
                        select s).Single();

               _dbContext.Carts.Remove(cart);
               await  _dbContext.SaveChangesAsync();
               
                Console.WriteLine("Successsssssssssssssssssssssssssssssssssss");

            }
            else
            {
                var cookie = Request.Cookies["myCard"];
                if (cookie != null)
                {
                    Console.WriteLine("This is delete onGet proId : " + proId);

                    //delete product with id
                    DeleteProductCookie(proId);

                    //return shopping cart
                   listP = new List<(int proId, int proQuantity)>();
                    listP = getList();

                    
                }
                HttpContext.Session.SetInt32(SessionNumber, listP.Count());
            }
           



            return RedirectToPage("ShoppingCart");
        }
        public void DeleteProductCookie(int proid)
        {
            CookieOptions options = new CookieOptions();

            //get cookie 
            var cookie = Request.Cookies["myCard"];
            if (cookie != null)
            {
                //create new list
                var listProductCart = new List<(int proId, int proQuantity)>();

                //create empty string
                string newCookie = "";

                //create list split cookie value by ' AND '
                string[] items = splitString(cookie, "AND");

                foreach (string item in items)
                {
                    //create list split by ' - '
                    string[] pair = splitString(item, "-");
                    try
                    {
                        //add product id and quantity to list
                        listProductCart.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error: " + ex);
                    }
                }


                //check if ID exist in list
                bool checkDuplicate = false;
                foreach (var item in listProductCart)
                {
                    if (item.proId == proid)
                    {
                        checkDuplicate = true;
                        break;
                    }
                }


                if (checkDuplicate == true)
                {
                    for (int i = 0; i < listProductCart.Count; i++)
                    {
                        //if Id exist in List
                        if (listProductCart[i].Item1 == proid)
                        {
                            //remove from list
                            listProductCart.Remove(listProductCart[i]);

                            break;
                        }

                    }

                    //if list not empty
                    if (listProductCart.Count > 0)
                    {
                        foreach (var item in listProductCart)
                        {
                            //append new string cookie value
                            newCookie += "AND" + item.proId + "-" + item.proQuantity;
                        }


                        //if string begin with ' AND '
                        if (newCookie.Substring(0, 3).Equals("AND"))
                        {
                            //remove ' AND ' from begining string
                            newCookie = newCookie.Substring(3, newCookie.Length - 3);

                        }
                    }

                    //if list is empty
                    else
                    {
                        newCookie = null;
                    }


                    //options.Expires = DateTime.Now.AddSeconds(0);

                    //if new string value is null
                    if (newCookie == null)
                    {
                        // Expire cookie
                        options.Expires = DateTime.Now.AddSeconds(0);
                        Response.Cookies.Append("myCard", "", options);
                    }

                    //if new string not null
                    else
                    {
                        //append new string to cookie
                        options.Expires = DateTime.Now.AddSeconds(1000);
                        Response.Cookies.Append("myCard", newCookie, options);
                    }
                    Console.WriteLine("new cookie after update: " + newCookie);

                }

            }


        }
        public string[] splitString(string value, string charac)
        {
            string[] result = value.Split(charac);

            return result;
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
    }
}
