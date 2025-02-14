using System;

namespace HealthCommunitiesCheck2.DTO
{
    public class EnrollmentDTO
    {
        public Guid EnrollmentID { get; set; }
        public Guid UserID { get; set; }
        public Guid CourseID { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string Status { get; set; } // Chuyển Enum thành string

        public string CourseTitle { get; set; } // Để hiển thị thông tin khóa học
        public string UserName { get; set; } // Để hiển thị thông tin người dùng
    }
}
