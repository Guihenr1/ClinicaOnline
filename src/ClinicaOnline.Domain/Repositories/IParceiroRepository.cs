using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories.Base;

namespace ClinicaOnline.Domain.Repositories
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