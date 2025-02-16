using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }

        [Required, MaxLength(100)]
        public string FullName { get; set; }

        [Required, EmailAddress, MaxLength(255)]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string Salt { get; set; }

        [ForeignKey("Role")]
        public Guid RoleID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual Role Role { get; set; }
        public virtual ICollection<Course> Courses { get; set; } = new HashSet<Course>();
        public virtual ICollection<Enrollment> Enrollments { get; set; } = new HashSet<Enrollment>();
        public virtual ICollection<Wallet> Wallets { get; set; } = new HashSet<Wallet>();
        public virtual ICollection<Transaction> SenderTransactions { get; set; } = new HashSet<Transaction>();
        public virtual ICollection<Transaction> ReceiverTransactions { get; set; } = new HashSet<Transaction>();

    }

}
