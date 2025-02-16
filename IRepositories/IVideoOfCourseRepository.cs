﻿using HealthCommunitiesCheck2.Model;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IVideoOfCourseRepository : IGenericRepository<VideoOfCourse>
    {
        Task<List<VideoOfCourse>> GetVideosByCourseId(Guid courseId);
    }
}
