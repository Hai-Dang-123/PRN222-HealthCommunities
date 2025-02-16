using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public enum EnrollmentStatus { Enrolled, Completed, Dropped }

    public class Enrollment
    {
        [Key]
        public Guid EnrollmentID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }

        [ForeignKey("Course")]
        public Guid CourseID { get; set; }

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        public EnrollmentStatus Status { get; set; }

        public virtual User User { get; set; }
        public virtual Course Course { get; set; }
    }

}
