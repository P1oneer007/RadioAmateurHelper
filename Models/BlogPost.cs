using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHelper.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public string? ImageUrl { get; set; } // Фото
        public string? VideoUrl { get; set; } // Видео
        public string? FileUrl { get; set; }  // Любой файл
        public DateTime CreatedAt { get; set; } = DateTime.Now;        

    }
}
