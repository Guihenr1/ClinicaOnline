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
        
        public async Task<Medico> GetById(Guid id) 
        {
            return await GetByIdAsync(id);
        }
        
        public async Task Update(Medico medico) 
        {
            await UpdateAsync(medico);
        }
        
        public async Task Delete(Medico medico) 
        {
            await DeleteAsync(medico);
        }

        public async Task<Medico> GetByCrmAndUfCrm(string crm, string ufCrm)
        {
            return await _dbContext.Medicos.FirstOrDefaultAsync(x => x.Crm.ToLower() == crm.ToLower()
                && x.UfCrm.ToLower() == ufCrm.ToLower());
        }
    }
}