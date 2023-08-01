//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
////using TechnologyShopManagement_v2.Models;

//namespace TechnologyShopManagement_v2.Pages.Views
//{
//    public class CreateModel : PageModel
//    {
//        //private readonly TechnologyShopManagement_v2.Models.Technology_ManagementContext _context;

//        //public CreateModel(TechnologyShopManagement_v2.Models.Technology_ManagementContext context)
//        //{
//        //    _context = context;
//        //}

//        public IActionResult OnGet()
//        {
//        ViewData["BranchCode"] = new SelectList(_context.Branches, "Code", "Code");
//        ViewData["CategoryCode"] = new SelectList(_context.Categories, "Code", "Code");
//            return Page();
//        }

//       // [BindProperty]
//        //public Product Product { get; set; }
        

//        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
//        public async Task<IActionResult> OnPostAsync()
//        {
//          if (!ModelState.IsValid)
//            {
//                return Page();
//            }

//          //  _context.Products.Add(Product);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("./Index");
//        }
//    }
//}
