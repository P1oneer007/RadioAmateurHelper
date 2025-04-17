using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Firmwares
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public FirmwareModel Firmware { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Firmware = await _context.Firmwares.FindAsync(id);
            if (Firmware == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _context.Firmwares.FindAsync(Firmware.Id);
            if (existing == null)
                return NotFound();

            // Удалить файл
            if (!string.IsNullOrEmpty(existing.FilePath))
            {
                var path = Path.Combine(_env.WebRootPath, existing.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(path))
                    System.IO.File.Delete(path);
            }

            _context.Firmwares.Remove(existing);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Firmwares/Index");
        }
    }
}
