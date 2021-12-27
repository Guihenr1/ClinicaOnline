using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories.Base;

namespace ClinicaOnline.Infrastructure.Repositories
{
    public class ParceiroRepository : Repository<Parceiro>, IParceiroRepository
    {
        public ParceiroRepository(Context dbContext) : base(dbContext)
        {
        }
        
        public async Task<IReadOnlyList<Parceiro>> GetAll() 
        {
            var result = await GetAllAsync();
            return result.ToList();
        }
        
        public async Task<Parceiro> Add(Parceiro parceiro) 
        {
            return await AddAsync(parceiro);
        }
        
        public async void Update(Parceiro parceiro) 
        {
            await UpdateAsync(parceiro);
        }
    }
}