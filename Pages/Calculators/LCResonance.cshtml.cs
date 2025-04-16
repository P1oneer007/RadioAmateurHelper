using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Calculators
{
    public class LCResonanceModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LCResonanceModel(ApplicationDbContext context) => _context = context;

        [BindProperty] public double L { get; set; } // ?H
        [BindProperty] public double C { get; set; } // nF
        public double? Frequency { get; set; }

        public void OnPost()
        {
            if (L > 0 && C > 0)
            {
                double lHenries = L * 1e-6;
                double cFarads = C * 1e-9;
                Frequency = 1 / (2 * Math.PI * Math.Sqrt(lHenries * cFarads)) / 1e6; // â ÌÃö

                _context.CalculatorHistories.Add(new CalculatorHistory
                {
                    CalculatorName = "LCResonance",
                    InputValues = $"L={L}?H, C={C}nF",
                    Result = $"{Frequency:F3} ÌÃö",
                    Timestamp = DateTime.Now
                });
                _context.SaveChanges();
            }
        }
    }
}
