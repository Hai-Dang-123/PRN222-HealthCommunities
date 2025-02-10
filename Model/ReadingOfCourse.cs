using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class ReadingOfCourse
    {
        [Key]
        public Guid ReadingID { get; set; }

        [ForeignKey("Course")]
        public Guid CourseID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Course Course { get; set; }
    }

}
