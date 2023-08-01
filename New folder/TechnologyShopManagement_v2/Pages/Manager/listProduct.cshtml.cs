using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLabs.Model;
using TechnologyShopManagement_v2.Models.Entities;
using static NuGet.Packaging.PackagingConstants;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    [BindProperties]
    public class listProductModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Product> product { get; set; }

        private readonly IConfiguration Configuration;
        public listProductModel(Technology_ManagementContext db, IConfiguration configuration)
        {
            Configuration = configuration;
            _db = db;
        }
        public Account accountU { get; set; }
        //public IEnumerable<Branch> branches { get; set; }
        //public IEnumerable<Category> categoryList { get; set; }
        //public IEnumerable<Product> productsList { get; set; }
        public async Task OnGetAsync(string sortOrder,
                 string currentFilter, string searchString, int? pageIndex)
        {
            string username = HttpContext.Session.GetString("isLogin").ToString();
            accountU = new Account();
            accountU = _db.Accounts.FirstOrDefault(s => s.Username.Equals(username));

            CurrentSort = sortOrder;
            if (searchString != null)
            {
                pageIndex = 1;

            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            IQueryable<Product> productIQ = _db.Products;


            if (!String.IsNullOrEmpty(searchString))
            {
                int number = 0;
                bool isNumber = int.TryParse(searchString, out number);
                productIQ = productIQ.Where(s => (s.ProductName.Contains(searchString) || s.Id==number||s.CategoryCode.Contains(searchString)));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            product = await PaginatedList<Product>.CreateAsync(productIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        }
    
    }
}
