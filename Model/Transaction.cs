using HealthCommunitiesCheck2.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public enum TransactionType { Deposit, Withdraw, Payment }
public enum TransactionStatus { Pending, Success, Failed }

public class Transaction
{
    [Key]
    public Guid TransactionID { get; set; }

    [ForeignKey("SenderUser")]
    public Guid SenderUserID { get; set; } // Student

    [ForeignKey("ReceiverUser")]
    public Guid? ReceiverUserID { get; set; } // Lecture (có thể null nếu giao dịch với hệ thống)

    [ForeignKey("Wallet")]
    public Guid WalletID { get; set; }

    [ForeignKey("ReceiverWallet")]
    public Guid? ReceiverWalletID { get; set; } // Ví nhận tiền (có thể là Lecture hoặc SystemWallet)

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    public TransactionType Type { get; set; }
    public TransactionStatus Status { get; set; } = TransactionStatus.Pending;

    public string VNPayTransactionID { get; set; } // Mã giao dịch VNPAY (nếu có)
    public string Description { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    public virtual User SenderUser { get; set; }
    public virtual User ReceiverUser { get; set; }
    public virtual Wallet Wallet { get; set; }
    public virtual Wallet ReceiverWallet { get; set; }

    public void ProcessTransaction()
    {
        if (Status == TransactionStatus.Success)
        {
            Wallet.UpdateBalance(Amount, Type);
            if (ReceiverWallet != null && Type == TransactionType.Payment)
            {
                ReceiverWallet.UpdateBalance(Amount, TransactionType.Deposit);
            }
        }
    }
}

