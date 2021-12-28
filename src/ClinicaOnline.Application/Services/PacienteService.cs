using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;

namespace ClinicaOnline.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private IPacienteRepository _pacienteRepository;
        public PacienteService(IPacienteRepository pacienteRepository)
        {
            _pacienteRepository = pacienteRepository;
        }

        public async Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId)
        {
            return await _pacienteRepository.GetPacientesByMedicoId(medicoId);
        }

        public async Task<IReadOnlyList<Paciente>> GetAll()
        {
            return await _pacienteRepository.GetAll();
        }
    }
}