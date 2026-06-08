using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;
using System.Security.Claims;

namespace RadioAmateurHelper.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) => _context = context;

        public List<BlogPost> Posts { get; set; }
        public bool AllowUsersToDeleteOwnPosts { get; set; }
        public string? CurrentUserId { get; set; }
        public bool IsAdmin { get; set; }

        public void OnGet()
        {
            Posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
            AllowUsersToDeleteOwnPosts = _context.SiteSettings.FirstOrDefault()?.AllowUsersToDeleteOwnPosts == true;
            CurrentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAdmin = User.Identity?.IsAuthenticated == true &&
                string.Equals(User.Identity.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase);
        }
    }
}
