using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Blog
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context) => _context = context;

        public List<BlogPost> Posts { get; set; }

        public void OnGet()
        {
            Posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
        }
    }
}
