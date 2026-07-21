using System.ComponentModel.DataAnnotations;

namespace PortfolioMH.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty;

        [MaxLength(500)]
        public string Technologies { get; set; } = string.Empty;

        [MaxLength(300)]
        public string? ImageUrl { get; set; }

        [MaxLength(300)]
        public string? LiveUrl { get; set; }

        [MaxLength(300)]
        public string? GitHubUrl { get; set; }

        public bool IsVisible { get; set; } = true;

        public bool IsFeatured { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
