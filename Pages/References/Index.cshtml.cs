using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.References
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) => _context = context;

        public List<ReferenceModel> Entries { get; set; }

        public void OnGet()
        {
            Entries = _context.References.OrderByDescending(r => r.CreatedAt).ToList();
        }
    }
}
