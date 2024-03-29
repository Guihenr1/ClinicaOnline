using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClinicaOnline.Domain.Entities;
using ClinicaOnline.Domain.Repositories;
using ClinicaOnline.Infrastructure.Data;
using ClinicaOnline.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ClinicaOnline.Infrastructure.Repositories
{
    public class PacienteRepository : Repository<Paciente>, IPacienteRepository
    {
        public PacienteRepository(Context dbContext) : base(dbContext)
        {
        }

        public async Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId)
        {
            return await _dbContext.Pacientes
                .Where(x => x.Medico.Id == medicoId).ToListAsync();
        }

        public async Task<IReadOnlyList<Paciente>> GetAll()
        {
            return await _dbContext.Pacientes.Include(x => x.Medico).ToListAsync();
        }

        public async Task<Paciente> Add(Paciente paciente)
        {
            return await AddAsync(paciente);
        }

        public async Task<Paciente> GetByCpf(string cpf)
        {
            return await _dbContext.Pacientes
                .Where(x => x.Cpf == cpf).FirstOrDefaultAsync();
        }
        
        public async Task Update(Paciente paciente) 
        {
            await UpdateAsync(paciente);
        }

        public async Task Delete(Paciente paciente)
        {
            await DeleteAsync(paciente);
        }

        public async Task<Paciente> GetById(Guid id)
        {
            return await GetByIdAsync(id);
        }
    }
}