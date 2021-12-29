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
    public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(Context dbContext) : base(dbContext)
        {
        }
        
        public async Task<IReadOnlyList<Parceiro>> GetAll() 
        {
            return await GetAllAsync();
        }
        
        public async Task<Parceiro> Add(Parceiro parceiro) 
        {
            return await AddAsync(parceiro);
        }
        
        public async Task Update(Parceiro parceiro) 
        {
            await UpdateAsync(parceiro);
        }
        
        public async Task<Parceiro> GetById(Guid id) 
        {
            return await GetByIdAsync(id);
        }
        
        public async Task<bool> CheckApiKey(Guid apiKey) 
        {
            var result = await _dbContext.Parceiros.Where(x => x.ApiKey == apiKey).FirstOrDefaultAsync();

            return result != null;
        }
    }
}