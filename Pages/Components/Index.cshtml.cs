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

        public List<ComponentModel> Results { get; set; } = new List<ComponentModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public void OnGet()
        {
            var query = _context.Components.AsQueryable();

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(SearchTerm) || c.Type.Contains(SearchTerm));
            }

            Results = query.OrderBy(c => c.Name).ToList();
        }
    }
}
