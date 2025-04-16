using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Circuits
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DeleteModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public CircuitModel Circuit { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Circuit = await _context.Circuits.FindAsync(id);
            if (Circuit == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var circuit = await _context.Circuits.FindAsync(Circuit.Id);
            if (circuit != null)
            {
                _context.Circuits.Remove(circuit);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage("Index");
        }
    }
}
