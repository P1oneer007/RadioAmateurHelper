using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Components
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
        public ComponentModel Component { get; set; }

        public IActionResult OnGet(int id)
        {
            Component = _context.Components.FirstOrDefault(c => c.Id == id);
            if (Component == null)
                return RedirectToPage("/Components/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IFormFile Image, IFormFile SchematicSymbol, bool DeleteImage, bool DeleteSymbol)
        {
            if (!ModelState.IsValid)
                return Page();

            var component = _context.Components.FirstOrDefault(c => c.Id == Component.Id);
            if (component == null)
                return RedirectToPage("/Components/Index");

            component.Name = Component.Name;
            component.Type = Component.Type;
            component.Price = Component.Price;
            component.Stock = Component.Stock;
            component.Description = Component.Description;
            component.Characteristics = Component.Characteristics;

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", "components");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            // Удаление изображения по кнопке
            if (DeleteImage && !string.IsNullOrEmpty(component.ImageUrl))
            {
                var oldImagePath = Path.Combine(_env.WebRootPath, component.ImageUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldImagePath))
                    System.IO.File.Delete(oldImagePath);

                component.ImageUrl = null;
            }

            // Удаление символа по кнопке
            if (DeleteSymbol && !string.IsNullOrEmpty(component.SchematicSymbolUrl))
            {
                var oldSymbolPath = Path.Combine(_env.WebRootPath, component.SchematicSymbolUrl.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                if (System.IO.File.Exists(oldSymbolPath))
                    System.IO.File.Delete(oldSymbolPath);

                component.SchematicSymbolUrl = null;
            }

            // Загрузка нового изображения
            if (Image != null)
            {
                var imagePath = Path.Combine(uploadsFolder, Image.FileName);
                using var stream = System.IO.File.Create(imagePath);
                await Image.CopyToAsync(stream);
                component.ImageUrl = "/uploads/components/" + Image.FileName;
            }

            // Загрузка нового символа
            if (SchematicSymbol != null)
            {
                var schematicPath = Path.Combine(uploadsFolder, SchematicSymbol.FileName);
                using var stream = System.IO.File.Create(schematicPath);
                await SchematicSymbol.CopyToAsync(stream);
                component.SchematicSymbolUrl = "/uploads/components/" + SchematicSymbol.FileName;
            }

            _context.Components.Update(component);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Components/Index");
        }
    }
}
