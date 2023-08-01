using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using TechnologyShopManagement_v2.Models;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    public class HomeModel : PageModel
    {
        public const string SessionNumber = "numberCart";

        private readonly Technology_ManagementContext _dbContext;
        public IEnumerable<Product> productsList { get; set; }
        public IEnumerable<Product> iphoneList { get; set; }
        public IEnumerable<Product> laptopList { get; set; }
        public IEnumerable<Product> pcList { get; set; }
        public IEnumerable<Cart> cartList { get; set; }

        public HomeModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async void OnGet()
        {
            productsList = new List<Product>();
            productsList = _dbContext.Products
                         .Where(s => s.Status > 0 && s.Stock > 0)
                        .Take(8);
               
            iphoneList = from iphone in _dbContext.Products
                         where iphone.CategoryCode == "phone" && iphone.Status>0 && iphone.Stock>0
                         select iphone;

            laptopList = from laptop in _dbContext.Products
                         where laptop.CategoryCode == "laptop" && laptop.Status > 0 &&laptop.Stock > 0
                         select laptop;
            pcList = from pc in _dbContext.Products
                     where pc.CategoryCode == "pc" && pc.Status > 0 && pc.Stock > 0 
                     select pc;

            
            //Customer have account
            //Customer have account
            //if (user != null)
            //{
            //    var account = await _dbContext.Accounts.SingleOrDefaultAsync(s => s.Username == user);
            //    HttpContext.Session.SetInt32(SessionNumber, 55);

            //}
            //else {
            string user = HttpContext.Session.GetString("isLogin");
            var listP = new List<(int proId, int proQuantity)>();
            
            if (user != null)
            {
                Account acc = new Account();
                acc = _dbContext.Accounts.FirstOrDefault(s => s.Username == user);
                cartList = from s in _dbContext.Carts
                           where s.AccountId == acc.UserId
                           select s;
                HttpContext.Session.SetInt32(SessionNumber, cartList.Count());
            }
            else
            {
                listP = getList();
                HttpContext.Session.SetInt32(SessionNumber, listP.Count());
            }
              
          //  }
                
            
                 

         
        }




        public async Task<IActionResult> OnPost(int proId, int proQuantity)
        {
            string user = HttpContext.Session.GetString("isLogin");
            Console.WriteLine("username is " + user);
                  //Customer have account
            if (user !=null)
            {
                var account = await _dbContext.Accounts.SingleOrDefaultAsync(s=>s.Username==user);

               
                var cart = await _dbContext.Carts.SingleOrDefaultAsync(s => s.AccountId == account.UserId && s.ProductId==proId);

                if (cart != null)
                {
                    int currentQuantity = 0;
                    currentQuantity = (int)cart.Quantity;
                    currentQuantity += proQuantity;
                    cart.Quantity = currentQuantity;
                    _dbContext.Carts.Update(cart);
                  await  _dbContext.SaveChangesAsync();

                }
                else
                {
                    addCart(proId,proQuantity,account.UserId);
                }
               

            }

            //Guest
            else
            {

                //if product exist
                if (proId > 0 && proQuantity > 0)
                {
                    //save to cookie
                    appenCookie(proId, proQuantity);

                }
            }


            //back to home page
            return RedirectToPage("Home");

        }

        public void updateCart(int accountID, int productId, int quantity)
        {

            Cart cart = new Cart
            {
                AccountId = accountID,
                ProductId = productId,
                Quantity = quantity,
                DateAdd = DateTime.Now
            };

            Console.WriteLine("proId quantity is " + cart.ProductId);
            Console.WriteLine("acId quantity is " + cart.AccountId);
            Console.WriteLine("quantity quantity is " + cart.Quantity);
            _dbContext.Carts.Update(cart);
            _dbContext.SaveChanges();

        }
        public void addCart(int productId, int quantity,int accountID)
        {
            string accID =HttpContext.Session.GetString("accountId").ToString();

            accountID = Int32.Parse(accID);


            Cart cart = new Cart
            {
                AccountId = accountID,
                ProductId = productId,
                Quantity = quantity,

                DateAdd = DateTime.Now

            };

            _dbContext.Add(cart);
            _dbContext.SaveChanges();

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

    }
}
