using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class EditCategoryModel : PageModel
    {
        static string categoryCode = "";
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        private readonly Technology_ManagementContext _db;
        public EditCategoryModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public IEnumerable<Category> categories { get; set; }
        public Account accountU { get; set; }
        public Category category { get; set; }
        public void OnGet(string categoryCode)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));

            categories = new List<Category>();
            categories = from cate in _db.Categories
                         where cate.Code == cate.SubCatCode
                         select cate;
            category = new Category();
            category = (from cate in _db.Categories
                       where cate.Code.Equals(categoryCode)
                       select cate).Single();



            if (messError.Length <= 1)
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
           

            try
            {
                
               
                
                _db.Categories.Update(category);
                await _db.SaveChangesAsync();
                messError = "";
                return Redirect("/Manager/listCategory");

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Update Fail";
            categoryCode = category.Code;
            return Redirect("/Manager/EditCategory");


        }


    }
}
