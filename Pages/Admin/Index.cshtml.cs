using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadioAmateurHelper.Pages.Admin
{
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true && User.Identity.Name == "Leka-07@bk.ru")
                return RedirectToPage("/Admin/Spreadsheet");

            return Page();
        }
    }
}
