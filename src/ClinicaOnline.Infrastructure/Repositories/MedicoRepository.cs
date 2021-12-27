using System;
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
    public class MedicoRepository : Repository<Medico>, IMedicoRepository
    {
        public MedicoRepository(Context dbContext) : base(dbContext)
        {
        }
        
        public async Task<IReadOnlyList<Medico>> GetAll() 
        {
            return await GetAllAsync();
        }
        
        public async Task<Medico> Add(Medico medico) 
        {
            return await AddAsync(medico);
        }

        public async Task<bool> CheckCrmAndUfCrmExists(string crm, string ufCrm)
        {
            var user = await _dbContext.Medicos.Where(x => x.Crm.ToLower() == crm.ToLower()
                && x.UfCrm.ToLower() == ufCrm.ToLower()).FirstOrDefaultAsync();
            return user != null;
        }
    }
}