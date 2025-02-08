using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Repositories;

namespace HealthCommunitiesCheck2.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork (ApplicationDbContext context)
        {
            _context = context;
            User = new UserRepository(_context);
            Contact = new ContactRepository(_context);
            Course = new CourseRepository(_context);
            Enrollment = new EnrollmentRepository(_context);    
            News = new NewsRepository(_context);
            ReadingOfCourse = new ReadingOfCourseRepository(_context);
            VideoOfCourse = new VideoOfCourseRepository(_context);  
            // ADD MORE
        }

        public IUserRepository User {  get; private set; }

        public IContactRepository Contact { get; private set; }

        public ICourseRepository Course { get; private set; }

        public IEnrollmentRepository Enrollment { get; private set; }

        public INewsRepository News { get; private set; }

        public IReadingOfCourseRepository ReadingOfCourse { get; private set; }

        public IVideoOfCourseRepository VideoOfCourse { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
