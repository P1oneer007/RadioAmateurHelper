using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;

namespace RadioAmateurHelper.Pages.Admin
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SettingsModel(ApplicationDbContext context) => _context = context;

        [BindProperty]
        public bool AllowUsersToDeleteOwnPosts { get; set; }

        public string? StatusMessage { get; set; }

        public IActionResult OnGet()
        {
            if (!IsAdmin()) return Forbid();

            AllowUsersToDeleteOwnPosts = GetSettings().AllowUsersToDeleteOwnPosts;
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!IsAdmin()) return Forbid();

            var settings = GetSettings();
            settings.AllowUsersToDeleteOwnPosts = AllowUsersToDeleteOwnPosts;
            _context.SaveChanges();

            StatusMessage = "Настройки сохранены.";
            return Page();
        }

        private Models.SiteSetting GetSettings()
        {
            var settings = _context.SiteSettings.FirstOrDefault();
            if (settings != null) return settings;

            settings = new Models.SiteSetting();
            _context.SiteSettings.Add(settings);
            _context.SaveChanges();
            return settings;
        }

        private bool IsAdmin() =>
            User.Identity?.IsAuthenticated == true &&
            string.Equals(User.Identity.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase);
    }
}
