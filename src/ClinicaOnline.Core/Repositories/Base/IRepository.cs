using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities.Base;

namespace ClinicaOnline.Core.Repositories.Base
{
    public interface IRepository<T> where T : EntityBase
    {
        Task<IReadOnlyList<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}