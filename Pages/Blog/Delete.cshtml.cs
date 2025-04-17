using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using RadioAmateurHelper.Data;

namespace RadioAmateurHelper.Pages.Blog
{
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
            if (post != null)
            {
                _context.BlogPosts.Remove(post);
                _context.SaveChanges();

                var folder = Path.Combine(_env.WebRootPath, "uploads", "blog", id.ToString());
                if (Directory.Exists(folder))
                    Directory.Delete(folder, true); // Удаляем папку с файлами
            }

            return RedirectToPage("/Blog/Index");
        }
    }
}
