//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.EntityFrameworkCore;
//using TechnologyShopManagement_v2.Models;

//namespace TechnologyShopManagement_v2.Pages.Views
//{
//    public class IndexModel : PageModel
//    {
//        private readonly TechnologyShopManagement_v2.Models.Technology_ManagementContext _context;

//        public IndexModel(TechnologyShopManagement_v2.Models.Technology_ManagementContext context)
//        {
//            _context = context;
//        }

//        public IList<Product> Product { get;set; } = default!;

//        public async Task OnGetAsync()
//        {
//            if (_context.Products != null)
//            {
//                Product = await _context.Products
//                .Include(p => p.BranchCodeNavigation)
//                .Include(p => p.CategoryCodeNavigation).ToListAsync();
//            }
//        }
//    }
//}
