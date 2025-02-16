using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.DTO
{
    public class ReadingDTO
    {
        public Guid ReadingID { get; set; } = Guid.NewGuid();

        [Required]
        public Guid CourseID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public string FilePath { get; set; } // Link Google Drive
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

}
