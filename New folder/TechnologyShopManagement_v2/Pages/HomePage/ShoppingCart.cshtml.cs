using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    public class ShoppingCartModel : PageModel
    {
        public const string SessionNumber = "numberCart";
        private readonly Technology_ManagementContext _dbContext;
        public List<(Product, int proQuantity)>? ProductsListCart { get; set; }



        public List<Cart> cartList { get; set; }



        public ShoppingCartModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;

        }
        public void OnGet()
        {
            List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();

            string user = HttpContext.Session.GetString("isLogin");
            if (user != null)
            {  //create new list 
                ProductsListCart = new List<(Product, int proQuantity)>();




                Account acc = new Account();
                acc = _dbContext.Accounts.FirstOrDefault(s => s.Username == user);






                cartList = (from cart in _dbContext.Carts
                           where cart.AccountId == acc.UserId
                           select cart).ToList();






                //cartList = from s in _dbContext.Carts
                //           where s.AccountId == acc.UserId
                //           select s;
                //HttpContext.Session.SetInt32(SessionNumber, cartList.Count());
                Product product = new Product();
                int quantity = 2;
                foreach(var i in cartList)
                {
                    quantity = (int)i.Quantity;
                    ProductsListCart.Add((_dbContext.Products.Find(i.ProductId), quantity));
                }
                HttpContext.Session.SetInt32(SessionNumber, cartList.Count());

            }
            else
            { //get cookie
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


                listP = getList();
                HttpContext.Session.SetInt32(SessionNumber, listP.Count());

            }

            

         
        }
        public async Task<IActionResult> OnPost(int proid, int proQuantity)
        {

            string user = HttpContext.Session.GetString("isLogin");
          //if not login
            if (user != null)
            {
                Cart cart = new Cart();
                Account acc = new Account();
                acc = _dbContext.Accounts.FirstOrDefault(s => s.Username == user);


                cart = _dbContext.Carts.SingleOrDefault(s=>s.ProductId==proid && s.AccountId==acc.UserId);
                cart.Quantity = proQuantity;
                _dbContext.Update(cart);
                _dbContext.SaveChanges();
                //Console.WriteLine("Update successssssssssssssssssssss");

            }
            else
            {
                //Update product in Shopping Cart Cookie
                UpdateCookie(proid, proQuantity);
            }
               

            //return shopping cart
            return RedirectToPage("ShoppingCart");

        }

        public void UpdateCookie(int proid, int proQuantity)
        {
            CookieOptions options = new CookieOptions();

            //get cookie
            var cookie = Request.Cookies["myCard"];
            if (cookie != null)
            {
                //create new list 
                var listProductCart = new List<(int proId, int proQuantity)>();
                string newCookie = "";

                //create list split by ' AND '
                string[] items = splitString(cookie, "AND");
                foreach (string item in items)
                {
                    //create list split by ' - '
                    string[] pair = splitString(item, "-");
                    try
                    {
                        //add to list with Key and Value
                        listProductCart.Add((int.Parse(pair[0]), int.Parse(pair[1])));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("error: " + ex);
                    }
                }

                //check Id exist in list
                bool checkDuplicate = false;
                foreach (var item in listProductCart)
                {
                    //if exist return true
                    if (item.proId == proid)
                    {
                        checkDuplicate = true;
                        break;
                    }
                }

                //if is true
                if (checkDuplicate == true)
                {
                    for (int i = 0; i < listProductCart.Count; i++)
                    {
                        //find Id in list
                        if (listProductCart[i].Item1 == proid)
                        {
                            //update product in list
                            listProductCart[i] = (listProductCart[i].Item1, proQuantity);
                            break;
                        }

                    }

                    foreach (var item in listProductCart)
                    {
                        //append new string value
                        newCookie += "AND" + item.proId + "-" + item.proQuantity;
                    }

                    //if string begin with ' AND '
                    if (newCookie.Substring(0, 3).Equals("AND"))
                    {
                        //remove AND from begin string value
                        newCookie = newCookie.Substring(3, newCookie.Length - 3);

                    }

                    //Delete cookie
                    options.Expires = DateTime.Now.AddSeconds(0);

                    //Add new cookie
                    options.Expires = DateTime.Now.AddSeconds(1000);
                    Response.Cookies.Append("myCard", newCookie, options);

                    //Console.WriteLine("new cookie after update: " + newCookie);

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
                        //Console.WriteLine("error: " + ex);
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
        public List<(int proId, int proQuantity)> getList()
        {
            List<(int proId, int proQuantity)> list = new List<(int proId, int proQuantity)>();
            var cookie = Request.Cookies["myCard"];
            if (cookie != null)
            {


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
