using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RadioAmateurHelper.Pages.Calculators
{
    public class AntennasModel : PageModel
    {
        [BindProperty] public double Frequency { get; set; }
        public double? Wavelength { get; set; }

        public void OnPost()
        {
            if (Frequency > 0)
                Wavelength = 300.0 / Frequency;
        }
    }
}
