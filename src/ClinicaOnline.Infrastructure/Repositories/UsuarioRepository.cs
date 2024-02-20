using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories;
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

        public async Task<IReadOnlyList<Usuario>> GetAll()
        {
            return await GetAllAsync();
        }

        public async Task<Usuario> GetUserByEmailAndPassword(Usuario user)
        {
            return await _dbContext.Usuarios
                .Where(x => x.Email == user.Email && x.Senha == user.Senha)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> Add(Usuario user)
        {
            return await AddAsync(user);
        }

        public async Task<bool> CheckEmailExists(string email)
        {
            var user = await _dbContext.Usuarios.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
            return user != null;
        }
    }
}