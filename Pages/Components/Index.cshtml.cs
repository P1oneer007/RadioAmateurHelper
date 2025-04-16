using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Components
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) => _context = context;

        [BindProperty(SupportsGet = true)] public string SearchTerm { get; set; }
        public List<ComponentModel> Results { get; set; }

        public void OnGet()
        {
            Results = string.IsNullOrWhiteSpace(SearchTerm)
                ? _context.ComponentModels.ToList()
                : _context.ComponentModels
                    .Where(c => c.Name.Contains(SearchTerm))
                    .ToList();
        }
    }
}
