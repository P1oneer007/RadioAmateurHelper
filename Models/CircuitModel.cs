using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHelper.Models
{
    public class CircuitModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public string SchematicFilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string Category { get; set; }
    }
}
