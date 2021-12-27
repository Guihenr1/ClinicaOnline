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
        
        public async Task<List<Parceiro>> GetAllAsync() 
        {
            var result = await GetAll();
            return result.ToList();
        }
    }
}