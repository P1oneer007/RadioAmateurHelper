using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Calculators
{
    public class DividerModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DividerModel(ApplicationDbContext context) => _context = context;

        [BindProperty] public double Vin { get; set; }
        [BindProperty] public double R1 { get; set; }
        [BindProperty] public double R2 { get; set; }

        public double? Vout { get; set; }

        public void OnPost()
        {
            if (R1 > 0 && R2 > 0)
            {
                Vout = Vin * R2 / (R1 + R2);

                _context.CalculatorHistories.Add(new CalculatorHistory
                {
                    CalculatorName = "Divider",
                    InputValues = $"Vin={Vin}V, R1={R1}, R2={R2}",
                    Result = $"{Vout:F2} Â",
                    Timestamp = DateTime.Now
                });
                _context.SaveChanges();
            }
        }
    }
}
