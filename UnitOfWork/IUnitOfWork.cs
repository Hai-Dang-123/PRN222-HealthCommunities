using HealthCommunitiesCheck2.IRepositories;

namespace HealthCommunitiesCheck2.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User {  get; }
        // Add more
        IContactRepository Contact { get; }
        ICourseRepository Course { get; }
        IEnrollmentRepository Enrollment { get; }
        INewsRepository News { get; }
        IReadingOfCourseRepository ReadingOfCourse { get; }
        IVideoOfCourseRepository VideoOfCourse { get; }

    }
}
