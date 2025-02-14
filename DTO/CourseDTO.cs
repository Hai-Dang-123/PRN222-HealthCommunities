namespace HealthCommunitiesCheck2.DTO
{
    public class CourseDTO
    {
        public Guid CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsDelete { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsOnline { get; set; }
        public decimal Price { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
