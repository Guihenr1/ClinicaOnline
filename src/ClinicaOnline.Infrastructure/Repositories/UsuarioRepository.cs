using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ClinicaOnline.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<List<Usuario>> GetAll()
        {
            var result = await GetAllAsync();
            return result.ToList();
        }

        public async Task<Usuario> GetUserByEmailAndPassword(Usuario user)
        {
            return await _dbContext.Usuarios
                .Where(x => x.Email == user.Email && x.Senha == user.Senha)
                .FirstOrDefaultAsync();
        }
    }
}