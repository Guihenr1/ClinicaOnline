using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories.Base;

namespace ClinicaOnline.Domain.Repositories
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