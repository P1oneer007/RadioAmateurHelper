using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;

namespace RadioAmateurHelper.Pages.Blog
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public EditModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public IActionResult OnGet(int id)
        {
            BlogPost = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (BlogPost == null)
                return RedirectToPage("/Blog/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, IFormFile Image, IFormFile Video, IFormFile File)
        {
            var post = _context.BlogPosts.FirstOrDefault(p => p.Id == id);
            if (post == null)
                return RedirectToPage("/Blog/Index");

            post.Title = Request.Form["Title"];
            post.Content = Request.Form["Content"];

            var postFolder = Path.Combine(_env.WebRootPath, "uploads", "blog", id.ToString());
            if (!Directory.Exists(postFolder))
                Directory.CreateDirectory(postFolder);

            if (Image != null)
            {
                var imagePath = Path.Combine(postFolder, Image.FileName);
                using var stream = System.IO.File.Create(imagePath);
                await Image.CopyToAsync(stream);
                post.ImageUrl = $"/uploads/blog/{id}/{Image.FileName}";
            }

            if (Video != null)
            {
                var videoPath = Path.Combine(postFolder, Video.FileName);
                using var stream = System.IO.File.Create(videoPath);
                await Video.CopyToAsync(stream);
                post.VideoUrl = $"/uploads/blog/{id}/{Video.FileName}";
            }

            if (File != null)
            {
                var filePath = Path.Combine(postFolder, File.FileName);
                using var stream = System.IO.File.Create(filePath);
                await File.CopyToAsync(stream);
                post.FileUrl = $"/uploads/blog/{id}/{File.FileName}";
            }

            _context.BlogPosts.Update(post);
            await _context.SaveChangesAsync();
            return RedirectToPage("/Blog/Post", new { id = post.Id });
        }
    }
}
