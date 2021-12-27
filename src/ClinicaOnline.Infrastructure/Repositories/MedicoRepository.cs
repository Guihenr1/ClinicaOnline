using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories.Base;

namespace ClinicaOnline.Infrastructure.Repositories
{
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(Context dbContext) : base(dbContext)
        {
        }
        
        public async Task<IReadOnlyList<Medico>> GetAll() 
        {
            return await GetAllAsync();
        }
    }
}