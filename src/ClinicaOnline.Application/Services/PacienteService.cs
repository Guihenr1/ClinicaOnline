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
using ClinicaOnline.Core.Utils;

namespace ClinicaOnline.Application.Services
{
    public class PacienteService : IPacienteService
    {
        private IPacienteRepository _pacienteRepository;
        private IMedicoService _medicoService;
        public PacienteService(IPacienteRepository pacienteRepository, IMedicoService medicoService)
        {
            _pacienteRepository = pacienteRepository;
            _medicoService = medicoService;
        }

        public async Task<IReadOnlyList<Paciente>> GetPacientesByMedicoId(Guid medicoId)
        {
            return await _pacienteRepository.GetPacientesByMedicoId(medicoId);
        }

        public async Task<IReadOnlyList<Paciente>> GetAll()
        {
            return await _pacienteRepository.GetAll();
        }

        public async Task<PacienteResponse> Add(PacienteRequest model)
        {
            var response = new PacienteResponse();

            if (await _pacienteRepository.GetByCpf(model.Cpf) != null){
                response.AddError("Cpf já cadastrado");
                return response;
            }

            if (!Cpf.ValidateCpf(model.Cpf)){
                response.AddError("Cpf inválido");
                return response;
            }        

            var mapped = ObjectMapper.Mapper.Map<Paciente>(model);
            mapped.Id = Guid.NewGuid();

            mapped.Medico = await _medicoService.GetBydId(model.MedicoId);
            if (mapped.Medico == null) {
                response.AddError("Médico não encontrado");
                return response;
            }

            var pacienteAdd = await _pacienteRepository.Add(mapped);

            return ObjectMapper.Mapper.Map<PacienteResponse>(pacienteAdd);
        }

        public async Task<PacienteResponse> Update(Guid id, PacienteRequest model)
        {
            var response = new PacienteResponse();

            var getByCpf = await _pacienteRepository.GetByCpf(model.Cpf);
            if (getByCpf != null && getByCpf.Id != id){
                response.AddError("Cpf já cadastrado");
                return response;
            }

            if (!Cpf.ValidateCpf(model.Cpf)){
                response.AddError("Cpf inválido");
                return response;
            }        

            var mapped = ObjectMapper.Mapper.Map<Paciente>(model);
            mapped.Id = id;

            mapped.Medico = await _medicoService.GetBydId(model.MedicoId);
            if (mapped.Medico == null) {
                response.AddError("Médico não encontrado");
                return response;
            }

            await _pacienteRepository.Update(mapped);

            return response;
        }

        public async Task<PacienteResponse> Delete(Guid id)
        {
            var response = new PacienteResponse();
            var paciente = await _pacienteRepository.GetById(id);

            if (paciente == null) {
                response.AddError("Paciente não encontrado");
                return response;
            }

            await _pacienteRepository.Delete(paciente);

            return response;
        }
    }
}