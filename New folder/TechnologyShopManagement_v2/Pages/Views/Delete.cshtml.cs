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
//    public class DeleteModel : PageModel
//    {
//        private readonly TechnologyShopManagement_v2.Models.Technology_ManagementContext _context;

//        public DeleteModel(TechnologyShopManagement_v2.Models.Technology_ManagementContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//      public Product Product { get; set; }

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null || _context.Products == null)
//            {
//                return NotFound();
//            }

//            var product = await _context.Products.FirstOrDefaultAsync(m => m.Id == id);

//            if (product == null)
//            {
//                return NotFound();
//            }
//            else 
//            {
//                Product = product;
//            }
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync(int? id)
//        {
//            if (id == null || _context.Products == null)
//            {
//                return NotFound();
//            }
//            var product = await _context.Products.FindAsync(id);

//            if (product != null)
//            {
//                Product = product;
//                _context.Products.Remove(Product);
//                await _context.SaveChangesAsync();
//            }

//            return RedirectToPage("./Index");
//        }
//    }
//}
