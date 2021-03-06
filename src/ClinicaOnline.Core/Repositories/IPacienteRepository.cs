using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories.Base;

namespace ClinicaOnline.Core.Repositories
{
    public interface IPacienteRepository : IRepository<Paciente>
    {
        Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId);
        Task<IReadOnlyList<Paciente>> GetAll();
        Task<Paciente> Add(Paciente paciente);
        Task<Paciente> GetByCpf(string cpf);
        Task Update(Paciente paciente);
        Task Delete(Paciente paciente);
        Task<Paciente> GetById(Guid id);
    }
}