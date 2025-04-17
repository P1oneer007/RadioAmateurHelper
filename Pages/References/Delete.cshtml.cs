using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.References
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ReferenceModel Entry { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Entry = await _context.References.FindAsync(id);
            if (Entry == null)
                return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var entry = await _context.References.FindAsync(Entry.Id);
            if (entry != null)
            {
                _context.References.Remove(entry);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
