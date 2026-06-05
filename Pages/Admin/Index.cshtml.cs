using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadioAmateurHelper.Pages.Admin
{
    [Authorize]
    public class IndexModel : PageModel
    {
        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated != true ||
                !string.Equals(User.Identity.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase))
            {
                return Forbid();
            }

            return Page();
        }
    }
}
