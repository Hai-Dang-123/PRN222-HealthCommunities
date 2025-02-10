using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class Wallet
    {
        [Key]
        public Guid WalletID { get; set; }

        [ForeignKey("User")]
        public Guid UserID { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Balance { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public virtual User User { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; } = new HashSet<Transaction>();
        public void UpdateBalance(decimal amount, TransactionType type)
        {
            if (type == TransactionType.Deposit)
                Balance += amount;
            else if (type == TransactionType.Withdraw || type == TransactionType.Payment)
                Balance -= amount;
        }

    }

}
