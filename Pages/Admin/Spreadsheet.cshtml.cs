using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Admin
{
    [Authorize]
    public class SpreadsheetModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SpreadsheetModel(ApplicationDbContext context) => _context = context;

        public string DataJson { get; set; }
        public List<ComponentModel> Components { get; set; } = new();
        public List<ExchangeEntry> ExchangeEntries { get; set; } = new();
        public List<ServiceRequestModel> ServiceRequests { get; set; } = new();
        public DateTime? UpdatedAt { get; set; }

        public IActionResult OnGet()
        {
            if (!IsAdmin()) return Forbid();

            var sheet = GetOrCreateSheet();
            DataJson = sheet.DataJson;
            UpdatedAt = sheet.UpdatedAt;
            Components = _context.Components.OrderBy(c => c.Name).ToList();
            ExchangeEntries = _context.ExchangeEntries.OrderByDescending(e => e.PostedAt).ToList();
            ServiceRequests = _context.ServiceRequests.OrderByDescending(r => r.CreatedAt).ToList();
            return Page();
        }

        public IActionResult OnPostSave([FromForm] string dataJson)
        {
            if (!IsAdmin()) return Forbid();

            if (string.IsNullOrWhiteSpace(dataJson))
                return BadRequest("Пустые данные");

            try
            {
                JsonDocument.Parse(dataJson);
            }
            catch
            {
                return BadRequest("Некорректный JSON");
            }

            var sheet = GetOrCreateSheet();
            sheet.DataJson = dataJson;
            sheet.UpdatedAt = DateTime.UtcNow;
            _context.SaveChanges();

            return new JsonResult(new { success = true, updatedAt = sheet.UpdatedAt });
        }

        private AdminSpreadsheet GetOrCreateSheet()
        {
            var sheet = _context.AdminSpreadsheets.FirstOrDefault();
            if (sheet != null) return sheet;

            sheet = new AdminSpreadsheet();
            _context.AdminSpreadsheets.Add(sheet);
            _context.SaveChanges();
            return sheet;
        }

        private bool IsAdmin() =>
            User.Identity?.IsAuthenticated == true && User.Identity.Name == "Leka-07@bk.ru";
    }
}
