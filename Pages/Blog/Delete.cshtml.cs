using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RadioAmateurHelper.Data;
using System.Security.Claims;

namespace RadioAmateurHelper.Pages.Blog
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public DeleteModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public IActionResult OnGet(int id)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post != null && CanDeletePost(post.AuthorUserId))
            {
                _context.BlogPosts.Remove(post);
                _context.SaveChanges();

                var folder = Path.Combine(_env.WebRootPath, "uploads", "blog", id.ToString());
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true); // Удаляем папку с файлами
            }

            return RedirectToPage("/Blog/Index");
        }

        private bool CanDeletePost(string? authorUserId)
        {
            if (User.Identity?.IsAuthenticated != true)
                return false;

            if (string.Equals(User.Identity.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase))
                return true;

            var settings = _context.SiteSettings.FirstOrDefault();
            if (settings?.AllowUsersToDeleteOwnPosts != true)
                return false;

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return !string.IsNullOrEmpty(authorUserId) && authorUserId == currentUserId;
        }
    }
}
