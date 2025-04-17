using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Blog
{
    public class PostModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PostModel(ApplicationDbContext context) => _context = context;

        public BlogPost Post { get; set; }

        public IActionResult OnGet(int id)
        {
            Post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (Post == null)
                return RedirectToPage("/Blog/Index");

            return Page();
        }
    }
}
