using System.ComponentModel.DataAnnotations;

namespace RadioAmateurHelper.Models
{
    public class FirmwareModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? FilePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
