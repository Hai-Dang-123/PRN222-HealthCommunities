using System.ComponentModel.DataAnnotations;

namespace HealthCommunitiesCheck2.Model
{
    public class UploadedFile
    {
        [Key]
        public Guid FileID { get; set; }

        [Required, MaxLength(255)]
        public string Title { get; set; }

        [Required]
        public string GoogleDriveFileID { get; set; } // ID trên Google Drive

        [Required]
        public string FileType { get; set; } // "video" hoặc "reading"

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}