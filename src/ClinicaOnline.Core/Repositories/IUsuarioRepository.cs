using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories.Base;

namespace ClinicaOnline.Core.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario> GetUserByEmailAndPassword(Usuario user);
        Task<List<Usuario>> GetAllAsync();
        Task<bool> CheckEmailExists(string email);
    }
}