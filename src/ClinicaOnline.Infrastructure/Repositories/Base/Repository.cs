using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities.Base;
using ClinicaOnline.Core.Repositories.Base;
using ClinicaOnline.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicaOnline.Infrastructure.Repositories.Base
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly Context _dbContext;

        public Repository(Context dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}