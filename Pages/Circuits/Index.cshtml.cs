using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore; 
using RadioAmateurHelper.Models;            
using RadioAmateurHelper.Data;
using Microsoft.AspNetCore.Mvc;

namespace RadioAmateurHelper.Pages.Circuits
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public List<CircuitModel> Circuits { get; set; }
        public string Category { get; set; }
        public string Search { get; set; }
        [BindProperty(SupportsGet = true)] public int Page { get; set; } = 1;
        public int TotalPages { get; set; }
        private const int PageSize = 5;

        public IndexModel(ApplicationDbContext context) => _context = context;

        public async Task OnGetAsync(string category, string search)
        {
            Category = category;
            Search = search;

            var query = _context.Circuits.AsQueryable();
            if (!string.IsNullOrWhiteSpace(category)) query = query.Where(c => c.Category == category);
            if (!string.IsNullOrWhiteSpace(search)) query = query.Where(c => c.Title.Contains(search));

            TotalPages = (int)Math.Ceiling(await query.CountAsync() / (double)PageSize);
            Circuits = await query.OrderByDescending(c => c.CreatedAt)
                                  .Skip((Page - 1) * PageSize)
                                  .Take(PageSize)
                                  .ToListAsync();
        }
    }
}
