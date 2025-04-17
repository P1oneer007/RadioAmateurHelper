using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Firmwares
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public FirmwareModel Firmware { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (UploadFile != null)
            {
                var uploadsFolder = Path.Combine(_environment.WebRootPath, "Uploads", "Firmwares");
                Directory.CreateDirectory(uploadsFolder); // создаст папку если нет

                var fileName = Path.GetFileName(UploadFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await UploadFile.CopyToAsync(stream);
                }

                Firmware.FilePath = "/Uploads/Firmwares/" + fileName;
            }

            _context.Firmwares.Add(Firmware);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
