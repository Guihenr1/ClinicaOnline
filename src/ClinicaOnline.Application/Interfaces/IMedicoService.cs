using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IMedicoService
    {
        Task<IReadOnlyList<Medico>> GetAll();
        Task<MedicoResponse> Add(MedicoRequest medico);
        Task<MedicoResponse> Update(Guid id, MedicoRequest model);
    }
}