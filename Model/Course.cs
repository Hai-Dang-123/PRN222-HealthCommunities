using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class Course
    {
        [Key]
        public Guid CourseID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDelete { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }

        public bool IsOnline { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public virtual ICollection<ReadingOfCourse> Readings { get; set; } = new HashSet<ReadingOfCourse>();
        public virtual ICollection<VideoOfCourse> Videos { get; set; } = new HashSet<VideoOfCourse>();
    }

}
