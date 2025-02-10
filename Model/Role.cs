using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class Role
    {
        [Key]
        public Guid RoleID { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }

}
