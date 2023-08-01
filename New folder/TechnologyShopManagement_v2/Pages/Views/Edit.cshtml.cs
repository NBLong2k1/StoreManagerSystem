//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using TechnologyShopManagement_v2.Models;

//namespace TechnologyShopManagement_v2.Pages.Views
//{
//    public class EditModel : PageModel
//    {
//        private readonly TechnologyShopManagement_v2.Models.Technology_ManagementContext _context;

//        public EditModel(TechnologyShopManagement_v2.Models.Technology_ManagementContext context)
//        {
//            _context = context;
//        }

//        [BindProperty]
//        public Product Product { get; set; } = default!;

//        public async Task<IActionResult> OnGetAsync(int? id)
//        {
//            if (id == null || _context.Products == null)
//            {
//                return NotFound();
//            }

//            var product =  await _context.Products.FirstOrDefaultAsync(m => m.Id == id);
//            if (product == null)
//            {
//                return NotFound();
//            }
//            Product = product;
//           ViewData["BranchCode"] = new SelectList(_context.Branches, "Code", "Code");
//           ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Code");
//            return Page();
//        }

//        // To protect from overposting attacks, enable the specific properties you want to bind to.
//        // For more details, see https://aka.ms/RazorPagesCRUD.
//        public async Task<IActionResult> OnPostAsync()
//        {
//            if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//            _context.Attach(Product).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!ProductExists(Product.Id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return RedirectToPage("./Index");
//        }

//        private bool ProductExists(int id)
//        {
//          return _context.Products.Any(e => e.Id == id);
//        }
//    }
//}
