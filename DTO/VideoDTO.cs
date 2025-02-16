using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.DTO
{
    public class VideoDTO
    {
        public Guid VideoID { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CourseID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public string VideoFilePath { get; set; } // Link Google Drive
        public TimeSpan Duration { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
