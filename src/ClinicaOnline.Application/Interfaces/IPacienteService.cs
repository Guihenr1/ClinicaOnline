using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;

namespace ClinicaOnline.Application.Interfaces
{
    public interface IPacienteService
    {
        Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId);
        Task<IReadOnlyList<Paciente>> GetAll();
    }
}