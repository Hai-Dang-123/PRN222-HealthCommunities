namespace HealthCommunitiesCheck2.Model
{
    public class News
    {
        public Guid NewsID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
    }

}
