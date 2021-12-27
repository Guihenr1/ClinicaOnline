using System.Collections.Generic;
using System.Threading.Tasks;
using ClinicaOnline.Application.Interfaces;
using ClinicaOnline.Application.Mapper;
using ClinicaOnline.Application.Models.Request;
using ClinicaOnline.Application.Models.Response;
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

        public async Task<MedicoResponse> Add(MedicoRequest medico)
        {
            var response = new MedicoResponse();

            if (await _medicoRepository.CheckCrmAndUfCrmExists(medico.Crm, medico.UfCrm))
            {
                response.AddError("CRM e Uf CRM já cadastrados");
                return response;
            }

            var medicoAdd = await _medicoRepository.Add(ObjectMapper.Mapper.Map<Medico>(medico));

            return ObjectMapper.Mapper.Map<MedicoResponse>(medicoAdd);
        }
    }
}