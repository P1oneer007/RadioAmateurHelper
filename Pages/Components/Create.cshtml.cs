using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Components
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(IFormFile Image, IFormFile SchematicSymbol)
        {
            var component = new ComponentModel
            {
                Name = Request.Form["Name"],
                Type = Request.Form["Type"],
                Price = decimal.Parse(Request.Form["Price"]),
                Stock = int.Parse(Request.Form["Stock"]),
                Description = Request.Form["Description"],
                Characteristics = Request.Form["Characteristics"]
            };

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "components");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            if (Image != null)
            {
                var imagePath = Path.Combine(uploadsFolder, Image.FileName);
                using var stream = System.IO.File.Create(imagePath);
                await Image.CopyToAsync(stream);
                component.ImageUrl = "/uploads/components/" + Image.FileName;
            }

            if (SchematicSymbol != null)
            {
                var schematicPath = Path.Combine(uploadsFolder, SchematicSymbol.FileName);
                using var stream = System.IO.File.Create(schematicPath);
                await SchematicSymbol.CopyToAsync(stream);
                component.SchematicSymbolUrl = "/uploads/components/" + SchematicSymbol.FileName;
            }

            _context.Components.Add(component);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Components/Index");
        }
    }
}
