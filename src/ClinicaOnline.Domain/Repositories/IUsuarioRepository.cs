using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories.Base;

namespace ClinicaOnline.Domain.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetUserByEmailAndPassword(Usuario user);
        Task<bool> CheckEmailExists(string email);
        Task<IReadOnlyList<Usuario>> GetAll();
        Task<Usuario> Add(Usuario user);
    }
}