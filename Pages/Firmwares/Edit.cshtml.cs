using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Firmwares
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public FirmwareModel Firmware { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Firmware = await _context.Firmwares.FindAsync(id);
            if (Firmware == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var existing = await _context.Firmwares.FindAsync(Firmware.Id);
            if (existing == null)
                return NotFound();

            if (UploadFile != null)
            {
                // удалить старый файл
                if (!string.IsNullOrEmpty(existing.FilePath))
                {
                    var oldPath = Path.Combine(_env.WebRootPath, existing.FilePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                        System.IO.File.Delete(oldPath);
                }

                var folder = Path.Combine("uploads", "firmwares", existing.Id.ToString());
                var path = Path.Combine(_env.WebRootPath, folder);
                Directory.CreateDirectory(path);

                var filePath = Path.Combine(folder, UploadFile.FileName);
                using (var stream = new FileStream(Path.Combine(_env.WebRootPath, filePath), FileMode.Create))
                {
                    await UploadFile.CopyToAsync(stream);
                }

                existing.FilePath = "/" + filePath.Replace("\\", "/");
            }

            existing.Name = Firmware.Name;
            existing.Description = Firmware.Description;

            await _context.SaveChangesAsync();

            return RedirectToPage("/Firmwares/Index");
        }
    }
}
