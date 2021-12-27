using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Core.Entities;
using ClinicaOnline.Core.Repositories;

namespace ClinicaOnline.Application.Services
{
    public class MedicoService : IMedicoService
    {
        private IMedicoRepository _medicoRepository;
        public MedicoService(IMedicoRepository medicoRepository)
        {
            _medicoRepository = medicoRepository;
        }

        public async Task<IReadOnlyList<Medico>> GetAll()
        {
            return await _medicoRepository.GetAll();
        }
    }
}