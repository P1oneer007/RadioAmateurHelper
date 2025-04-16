using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages
{
    public class ExchangeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public ExchangeModel(ApplicationDbContext context) => _context = context;

        public List<ExchangeEntry> Entries { get; set; }

        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Message { get; set; }

        public void OnGet() =>
            Entries = _context.ExchangeEntries.OrderByDescending(e => e.PostedAt).ToList();

        public IActionResult OnPost()
        {
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Message))
            {
                _context.ExchangeEntries.Add(new ExchangeEntry
                {
                    Name = Name,
                    Message = Message,
                    PostedAt = DateTime.Now
                });
                _context.SaveChanges();
            }

            return RedirectToPage();
        }
    }
}
