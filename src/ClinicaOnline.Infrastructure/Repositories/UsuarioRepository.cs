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

        public async Task<List<Usuario>> GetAllAsync()
        {
            var result = await GetAll();
            return result.ToList();
        }

        public async Task<Usuario> GetUserByEmailAndPassword(Usuario user)
        {
            return await _dbContext.Usuarios
                .Where(x => x.Email == user.Email && x.Senha == user.Senha)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> AddAsync(Usuario user)
        {
            return await Add(user);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await _dbContext.Usuarios.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return user != null;
        }
    }
}