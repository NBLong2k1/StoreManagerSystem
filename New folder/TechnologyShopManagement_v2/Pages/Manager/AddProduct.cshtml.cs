using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class AddProductModel : PageModel
    {
        public const string errorMessage = "errorMessage";
        public static string messError = "";

        private readonly Technology_ManagementContext _db;
        public AddProductModel(Technology_ManagementContext db)
        {
            _db = db;
        }
        public Account accountU { get; set; }
        public IEnumerable<Category> categories { get; set; }
        public IEnumerable<Product> products { get; set; }  

        public Product product { get; set; }
        public void OnGet()
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));


            categories =new List<Category>();
            
            categories = from cate in _db.Categories
                         select cate;
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
            try {
                product.Status = 1;
                    await _db.AddAsync(product);
                    await _db.SaveChangesAsync();
                    messError = "";
                    return Redirect("/Manager/listProduct");

            }
            catch (Exception ex)
            {
                Console.WriteLine("error in 377777777777777777" + ex);
            }
            messError = "Add Fail";
            return Redirect("/Manager/AddProduct");


        }
    }
}
