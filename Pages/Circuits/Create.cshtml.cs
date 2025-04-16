using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;
using System.IO;
using System.Threading.Tasks;
using System;
using System.Diagnostics;

namespace RadioAmateurHelper.Pages.Circuits
{
    // [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public CircuitModel Circuit { get; set; }

        [BindProperty]
        public IFormFile UploadImage { get; set; }

        [BindProperty]
        public IFormFile UploadFile { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            Circuit.CreatedAt = DateTime.UtcNow;

            if (UploadImage != null)
            {
                var imgPath = Path.Combine("uploads/images", UploadImage.FileName);
                var fullImgPath = Path.Combine(_env.WebRootPath, imgPath);
                Debug.WriteLine("WebRootPath: " + _env.WebRootPath);

                using var fs = new FileStream(fullImgPath, FileMode.Create);
                await UploadImage.CopyToAsync(fs);
                Circuit.ImageUrl = "/" + imgPath.Replace("\\", "/");
            }

            if (UploadFile != null)
            {
                var filePath = Path.Combine("uploads/files", UploadFile.FileName);
                var fullFilePath = Path.Combine(_env.WebRootPath, filePath);
                using var fs = new FileStream(fullFilePath, FileMode.Create);
                await UploadFile.CopyToAsync(fs);
                Circuit.SchematicFilePath = "/" + filePath.Replace("\\", "/");
            }

            _context.Circuits.Add(Circuit);
            await _context.SaveChangesAsync();
            return RedirectToPage("Index");
        }
    }
}
