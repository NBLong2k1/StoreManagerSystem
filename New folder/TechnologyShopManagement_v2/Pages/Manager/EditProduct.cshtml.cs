using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class EditProductModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        static int id = 0;
        private readonly Technology_ManagementContext _dbContext;
        public EditProductModel(Technology_ManagementContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<Category> categoryList { get; set; }
        public Account accountU { get; set; }
        public Product product { get; set; }
        public void OnGet(int proId)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _dbContext.Accounts.FirstOrDefault(s => s.Username.Equals(username));


            if (proId != 0)
            {
                id = proId;
            }
 product = null;
            categoryList = null;
         
            categoryList = from cate in _dbContext.Categories
                           select cate;
            product = _dbContext.Products.Find(id);

            if (messError.Length <= 1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }

        }


        public async Task<IActionResult> OnPost(string productStatus)
        {
            int status = 0;

            try
            {
                if (productStatus == null)
                {
                    status = 0;
                }
                else
                {
                    status = 1;
                }
                Console.WriteLine("status is " + status);
                product.Status= status;
                _dbContext.Products.Update(product);
                await _dbContext.SaveChangesAsync();
                messError = "";
                return Redirect("/Manager/listProduct");

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Update Fail";
            id = product.Id;
            return Redirect("/Manager/editProduct");


        }
    }
}
