using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages
{
    public class ExchangeModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        //  онтекст базы данных (Entity Framework)
        public ExchangeModel(ApplicationDbContext context) => _context = context;

        public List<ExchangeEntry> Entries { get; set; }
        // —писок всех объ€влений, отображаемых на странице

        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Message { get; set; }
        // —войства, к которым прив€заны пол€ формы

        public void OnGet() =>
            // ѕри GET-запросе Ч получаем список объ€влений из базы
            Entries = _context.ExchangeEntries
                .OrderByDescending(e => e.PostedAt)
                .ToList();

        public IActionResult OnPost()
        {
            // ќбработка отправки формы (POST-запрос)
            if (!string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(Message))
            {
                _context.ExchangeEntries.Add(new ExchangeEntry
                {
                    Name = Name,
                    Message = Message,
                    PostedAt = DateTime.Now
                });
                _context.SaveChanges(); // —охран€ем новое объ€вление
            }

            return RedirectToPage(); // ѕерезагрузка страницы (PRG-паттерн)
        }
    }
}
