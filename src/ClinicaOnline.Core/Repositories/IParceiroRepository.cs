using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories.Base;

namespace ClinicaOnline.Core.Repositories
{
    public interface IParceiroRepository : IRepository<Parceiro>
    {
        Task<Parceiro> Add(Parceiro parceiro);
        Task<IReadOnlyList<Parceiro>> GetAll();
        Task Update(Parceiro parceiro);
        Task<Parceiro> GetById(Guid id);
        Task<bool> CheckApiKey(Guid apiKey);
    }
}