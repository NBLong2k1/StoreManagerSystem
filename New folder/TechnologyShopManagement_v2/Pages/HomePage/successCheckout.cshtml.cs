using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.HomePage
{
    public class successCheckoutModel : PageModel
    {
        public const string SessionNumber = "numberCart";
        private readonly Technology_ManagementContext _dbContext;
        public successCheckoutModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Cart> cartList { get; set; }
        public void OnGet()
        {
            string user = HttpContext.Session.GetString("isLogin");

            List<(int proId, int proQuantity)> listP = new List<(int proId, int proQuantity)>();

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
