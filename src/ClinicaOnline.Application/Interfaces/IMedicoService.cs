using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<IReadOnlyList<Medico>> GetAll();
        Task<MedicoResponse> Add(MedicoRequest medico);
        Task Update(Guid id, MedicoRequest model);
        Task Delete(Guid medicoId);
        Task<Medico> GetBydId(Guid id);
        Task<IReadOnlyList<Medico>> GetAllForPartners(string ufCrm);
    }
}