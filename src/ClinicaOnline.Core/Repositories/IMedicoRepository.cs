using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories.Base;

namespace ClinicaOnline.Core.Repositories
{
    public interface IMedicoRepository : IRepository<Medico>
    {
        Task<IReadOnlyList<Medico>> GetAll();
        Task<Medico> Add(Medico medico);
        Task<bool> CheckCrmAndUfCrmExists(string crm, string ufCrm);
    }
}