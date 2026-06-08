using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Admin
{
    [Authorize]
    public class BlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BlogModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public List<BlogPost> Posts { get; set; } = new();

        public IActionResult OnGet()
        {
            if (!IsAdmin()) return Forbid();

            LoadPosts();
            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (!IsAdmin()) return Forbid();

            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post != null)
            {
                _context.BlogPosts.Remove(post);
                _context.SaveChanges();

                var folder = Path.Combine(_env.WebRootPath, "uploads", "blog", id.ToString());
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true);
            }

            return RedirectToPage();
        }

        private void LoadPosts() =>
            Posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();

        private bool IsAdmin() =>
            User.Identity?.IsAuthenticated == true &&
            string.Equals(User.Identity.Name, "Leka-07@bk.ru", StringComparison.OrdinalIgnoreCase);
    }
}
