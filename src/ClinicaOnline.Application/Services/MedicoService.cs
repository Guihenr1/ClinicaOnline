using System;
using System.Collections.Generic;
using System.Linq;
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
        private Lazy<IPacienteService> _pacienteService;
        public MedicoService(IMedicoRepository medicoRepository, Lazy<IPacienteService> pacienteService)
        {
            _medicoRepository = medicoRepository;
            _pacienteService = pacienteService;
        }

        public async Task<IReadOnlyList<Medico>> GetAll()
        {
            return await _medicoRepository.GetAll();
        }

        public async Task<MedicoResponse> Update(Guid id, MedicoRequest model)
        {
            var response = new MedicoResponse();

            var checkMedicoExist = await _medicoRepository.GetById(id);
            if (checkMedicoExist == null)
            {
                response.AddError("Médico não encontrado");
                return response;
            }

            var checkCrmAndUfCrm = await _medicoRepository.GetByCrmAndUfCrm(model.Crm, model.UfCrm);
            if (checkCrmAndUfCrm != null && checkCrmAndUfCrm.Id != id)
            {
                response.AddError("CRM e Uf CRM já cadastrados para outro médico");
                return response;
            }

            var medico = ObjectMapper.Mapper.Map<Medico>(model);
            medico.Id = id;
            await _medicoRepository.Update(medico);

            return response;
        }

        public async Task<MedicoResponse> Add(MedicoRequest medico)
        {
            var response = new MedicoResponse();

            var checkCrmAndUfCrm = await _medicoRepository.GetByCrmAndUfCrm(medico.Crm, medico.UfCrm);
            if (checkCrmAndUfCrm != null)
            {
                response.AddError("CRM e Uf CRM já cadastrados");
                return response;
            }

            var medicoAdd = await _medicoRepository.Add(ObjectMapper.Mapper.Map<Medico>(medico));

            return ObjectMapper.Mapper.Map<MedicoResponse>(medicoAdd);
        }

        public async Task<MedicoResponse> Delete(Guid medicoId)
        {
            var response = new MedicoResponse();

            var pacientes = await _pacienteService.Value.GetPacientesByMedicoId(medicoId);
            if (pacientes.Any())
            {
                response.AddError("Não é possível excluir médicos com pacientes associados");
                return response;
            }

            var medico = await _medicoRepository.GetById(medicoId);
            if (medico == null)
            {
                response.AddError("Médico não encontrado");
                return response;
            }
            
            await _medicoRepository.Delete(medico);

            return response;
        }

        public async Task<Medico> GetBydId(Guid id)
        {
            return await _medicoRepository.GetById(id);
        }

        public async Task<IReadOnlyList<Medico>> GetAllForPartners(string ufCrm)
        {
            var medicos = await _medicoRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(ufCrm))
                medicos = medicos.Where(x => x.UfCrm.ToLower() == ufCrm.ToLower()).ToList();

            return medicos;
        }
    }
}