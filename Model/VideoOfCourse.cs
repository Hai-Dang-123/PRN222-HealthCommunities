using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class VideoOfCourse
    {
        [Key]
        public int VideoID { get; set; }

        [ForeignKey("Course")]
        public int CourseID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string VideoFilePath { get; set; }

        public TimeSpan Duration { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Course Course { get; set; }
    }

}
