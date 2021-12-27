using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories.Base;

namespace ClinicaOnline.Core.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetUserByEmailAndPassword(Usuario user);
        Task<bool> CheckEmailExists(string email);
        Task<IReadOnlyList<Usuario>> GetAll();
        Task<Usuario> Add(Usuario user);
    }
}