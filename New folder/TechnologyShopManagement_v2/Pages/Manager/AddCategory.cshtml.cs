using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class AddCategoryModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";
      
        private readonly Technology_ManagementContext _db;
        public AddCategoryModel (Technology_ManagementContext db)
        {
            _db = db;
        }

        public IEnumerable<Category> categories { get; set; }
        public Account accountU { get; set; }
        public Category category { get; set; }  
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));

            categories =new List<Category>();
            categories = from cate in _db.Categories
                         where cate.Code == cate.SubCatCode
                         select cate;

            if (messError.Length<=1)
            {
                HttpContext.Session.SetString(errorMessage, "");
            }
            else
            {
                HttpContext.Session.SetString(errorMessage, messError);
            }
            
        }
        public async Task<IActionResult> OnPost()
        {
            bool checkExist = false;
            categories = _db.Categories;
            try
            {
                foreach(var c in categories) {
                    if (c.Code.Equals(category.Code))
                    {
                        checkExist = true;

                    }
                }

                if (checkExist == false)
                {
                    await _db.AddAsync(category);
                    await _db.SaveChangesAsync();
                    messError = "";
                    return Redirect("/Manager/listCategory");
                }
                else
                {
                    messError = "Code maybe Exist";
                    Console.WriteLine("cannot add");
                    return Redirect("/Manager/AddCategory");
                    messError = "";
                }
              
              

            }
            catch (Exception ex) {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Add Fail";
            return Redirect("/Manager/AddCategory");


        }
    }
}
