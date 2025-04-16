using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadioAmateurHelper.Pages.Calculators
{
    public class OhmLawModel : PageModel
    {
        [BindProperty] public double Voltage { get; set; }
        [BindProperty] public double Resistance { get; set; }
        public double? Current { get; set; }

        public void OnPost()
        {
            if (Resistance != 0)
                Current = Voltage / Resistance;
        }
    }
}
