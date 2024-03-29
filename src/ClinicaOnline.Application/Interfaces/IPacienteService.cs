using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
using ClinicaOnline.Domain.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId);
        Task<IReadOnlyList<Paciente>> GetAll();
        Task<PacienteResponse> Add(PacienteRequest model);
        Task Update(Guid id, PacienteRequest model);
        Task Delete(Guid id);
    }
}