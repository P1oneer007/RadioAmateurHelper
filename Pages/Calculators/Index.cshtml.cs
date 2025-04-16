using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadioAmateurHelper.Pages.Calculators
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public string SelectedCalculator { get; set; }

        public string RedirectUrl { get; set; }

        public void OnGet() { }

        public void OnPost()
        {
            if (!string.IsNullOrWhiteSpace(SelectedCalculator))
            {
                RedirectUrl = $"/Calculators/{SelectedCalculator}";
            }
        }
    }
}
