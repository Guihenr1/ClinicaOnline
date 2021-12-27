using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities.Base;

namespace ClinicaOnline.Core.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}