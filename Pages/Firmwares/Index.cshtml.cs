using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Firmwares
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) => _context = context;

        public List<FirmwareModel> Firmwares { get; set; }

        public void OnGet()
        {
            Firmwares = _context.Firmwares.OrderByDescending(f => f.UploadedAt).ToList();
        }
    }
}
