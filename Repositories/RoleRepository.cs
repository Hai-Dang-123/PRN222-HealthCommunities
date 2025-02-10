using HealthCommunitiesCheck2.Data;
using HealthCommunitiesCheck2.IRepositories;
using HealthCommunitiesCheck2.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace HealthCommunitiesCheck2.Repositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        private readonly ApplicationDbContext _context;

        public RoleRepository(ApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Role> GetByIdAsync(Guid roleId)
        {
            return await _context.Roles.FindAsync(roleId);
        }
        public async Task<List<Role>> GetAllAsync()
        {
            return await GetAll().ToListAsync();
        }


    }

}
