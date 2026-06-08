using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RadioAmateurHelper.Data;
using RadioAmateurHelper.Models;
using System.Security.Claims;

namespace RadioAmateurHelper.Pages.Blog
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;

        public CreateModel(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        [BindProperty]
        public BlogPost BlogPost { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync(IFormFile Image, IFormFile Video, IFormFile File)
        {
            if (!ModelState.IsValid)
                return Page();

            BlogPost.CreatedAt = DateTime.Now;
            BlogPost.AuthorUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            BlogPost.AuthorName = User.Identity?.Name;

            _context.BlogPosts.Add(BlogPost);
            await _context.SaveChangesAsync();

            // Сначала сохранили в БД, получили BlogPost.Id

            var postFolder = Path.Combine(_env.WebRootPath, "uploads", "blog", BlogPost.Id.ToString());
            if (!Directory.Exists(postFolder))
                Directory.CreateDirectory(postFolder);

            if (Image != null)
            {
                var imagePath = Path.Combine(postFolder, Image.FileName);
                using var stream = System.IO.File.Create(imagePath);
                await Image.CopyToAsync(stream);
                BlogPost.ImageUrl = $"/uploads/blog/{BlogPost.Id}/{Image.FileName}";
            }

            if (Video != null)
            {
                var videoPath = Path.Combine(postFolder, Video.FileName);
                using var stream = System.IO.File.Create(videoPath);
                await Video.CopyToAsync(stream);
                BlogPost.VideoUrl = $"/uploads/blog/{BlogPost.Id}/{Video.FileName}";
            }

            if (File != null)
            {
                var filePath = Path.Combine(postFolder, File.FileName);
                using var stream = System.IO.File.Create(filePath);
                await File.CopyToAsync(stream);
                BlogPost.FileUrl = $"/uploads/blog/{BlogPost.Id}/{File.FileName}";
            }

            _context.BlogPosts.Update(BlogPost);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Blog/Index");
        }

    }
}
