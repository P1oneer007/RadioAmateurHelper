using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Admin
{
    public class CircuitsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CircuitsModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(string Title, string Category, string Description, IFormFile Image)
        {
            var circuit = new CircuitModel
            {
                Title = Title,
                Category = Category,
                Description = Description,
                CreatedAt = DateTime.Now
            };

            if (Image != null)
            {
                var fileName = Path.GetFileName(Image.FileName);
                var path = Path.Combine(_env.WebRootPath, "uploads/circuits", fileName);
                using var stream = System.IO.File.Create(path);
                await Image.CopyToAsync(stream);
                circuit.ImageUrl = "/uploads/circuits/" + fileName;
            }

            _context.Circuits.Add(circuit);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Admin/Circuits");
        }
    }
}
