namespace HealthCommunitiesCheck2.Model
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public DateTime SubmittedAt { get; set; }
    }

}
