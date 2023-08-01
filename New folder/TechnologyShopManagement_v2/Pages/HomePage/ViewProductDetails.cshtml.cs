using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;
using System.Text.Json;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    [BindProperties]
    public class ViewProductDetailsModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public const string SessionNumber = "numberCart";

        public ViewProductDetailsModel(Technology_ManagementContext db)
        {
            _db = db;

        }
        public IEnumerable<Cart> cartList { get; set; }
        public IEnumerable<Product> listProduct { get; set; }





        public Product product { get; set; }
        public async void OnGet(int proID)
        {
            string user = HttpContext.Session.GetString("isLogin");
            listProduct = null;
            product = null;
            List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();
            listP = getList();
            

            if (user != null)
            {
                Account acc = new Account();
                acc = _db.Accounts.FirstOrDefault(s => s.Username == user);
                cartList = from s in _db.Carts
                           where s.AccountId == acc.UserId
                           select s;
                HttpContext.Session.SetInt32(SessionNumber, cartList.Count());
            }
            else
            {
                listP = getList();
                HttpContext.Session.SetInt32(SessionNumber, listP.Count());
            }







            product = _db.Products.SingleOrDefault(s => s.Id == proID);

            listProduct = from prLst in _db.Products
                        .OrderBy(x => Guid.NewGuid())
                        .Where(x => x.Status > 0)
                        .Take(4)
                          select prLst;

        }
        public async Task<IActionResult> OnPost(int proId, int proQuantity)
        {

            //if product exist
            if (proId > 0 && proQuantity > 0)
            {
                //save to cookie
                appenCookie(proId, proQuantity);

            }

            //back to home page
            return RedirectToPage("Home");

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
