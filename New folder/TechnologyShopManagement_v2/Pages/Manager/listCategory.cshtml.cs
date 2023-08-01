using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesLabs.Model;
using TechnologyShopManagement_v2.Models.Entities;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    public class listCategoryModel : PageModel
    {
        private readonly Technology_ManagementContext _db;
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Category> category { get; set; }
        public Account accountU { get; set; }
        private readonly IConfiguration Configuration;
        public listCategoryModel(Technology_ManagementContext db, IConfiguration configuration)
        {
            _db = db;
          
            Configuration = configuration;
        }

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

            IQueryable<Category> CategoryIQ = _db.Categories;


            if (!String.IsNullOrEmpty(searchString))
            {
                CategoryIQ = CategoryIQ.Where(s => (s.Name.Contains(searchString) || s.Code.Contains(searchString)));
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            category = await PaginatedList<Category>.CreateAsync(CategoryIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


        
    }
    }
}
