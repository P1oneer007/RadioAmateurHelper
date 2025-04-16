using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RadioAmateurHelper.Models
{
    public class ReferenceModel
    {
        /*[Key]
         public int Id { get; set; }

         [Required]
         [StringLength(200)]
         public string Title { get; set; }

         [Required]
         public string Content { get; set; } // можно хранить Markdown или HTML

         // public DateTime CreatedAt { get; set; } = DateTime.Now;
         [Column(TypeName = "TEXT")]
         public DateTime CreatedAt { get; set; }*/
        public int Id { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; }

        public string Content { get; set; }

        // Для SQLite используем TEXT, EF сам сериализует DateTime как строку
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
