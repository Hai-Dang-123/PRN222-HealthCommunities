﻿using System.Linq.Expressions;

namespace HealthCommunitiesCheck2.IRepositories
{
    public interface IGenericRepository<T> where T : class
    {
        T GetById(string id);
        T GetByGuid(Guid id);
        Task<T> GetByIdAsync(Guid id);
        IQueryable<T> GetAll();
        IQueryable<T> FindAll(Expression<Func<T, bool>> expression);
        IEnumerable<T> FindAllAsync(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        T Add(T entity);
        Task<T> GetByIdsAsync(int id);
        Task<T> UpdateAsync(T entity);
        bool Delete(T entity);
        Task<List<T>> GetAllByListAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> GetAllByListAsync(Expression<Func<T, bool>> expression, object include);
        void UpdateRange(List<T> entity);
        void RemoveRange(List<T> entity);
        void AddRange(List<T> entity);
        Task DeleteAsync(Guid id);
        Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<List<T>> ToListAsync();

    }
}
