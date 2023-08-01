using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TechnologyShopManagement_v2.Pages.Manager
{
    public class LogOutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
             HttpContext.Session.Remove("isLogin");
          
            return RedirectToPage("/HomePage/Home");
        }
    }
}
