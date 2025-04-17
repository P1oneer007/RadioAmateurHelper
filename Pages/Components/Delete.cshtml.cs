using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Components
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult OnPost()
        {
            var component = _context.Components.FirstOrDefault(c => c.Id == Component.Id);
            if (component != null)
            {
                _context.Components.Remove(component);
                _context.SaveChanges();
            }

            return RedirectToPage("/Components/Index");
        }
    }
}
