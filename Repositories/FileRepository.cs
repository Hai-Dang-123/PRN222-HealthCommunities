using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.Repositories
{
    public class FileRepository
    {
        private readonly ApplicationDbContext _context;

        public FileRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveFileAsync(string title, string fileId, string fileType)
        {
            var file = new UploadedFile
            {
                FileID = Guid.NewGuid(),
                Title = title,
                GoogleDriveFileID = fileId,
                FileType = fileType,
                CreatedAt = DateTime.UtcNow
            };

            _context.UploadedFiles.Add(file);
            await _context.SaveChangesAsync();
        }
    }
}
