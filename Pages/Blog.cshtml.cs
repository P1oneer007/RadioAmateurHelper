using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages
{
    public class BlogModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public BlogModel(ApplicationDbContext context) => _context = context;

        public List<BlogPost> Posts { get; set; }

        public void OnGet() =>
            Posts = _context.BlogPosts.OrderByDescending(p => p.CreatedAt).ToList();
    }
}
